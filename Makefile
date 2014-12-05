KUBEROOT = ~/Code/kubernetes
KUBECTL = $(KUBEROOT)/cluster/kubectl.sh
CURDIR = $(shell pwd)

# Not sure if I'm using make correctly since I need .NOTPARALLEL
# For the all step, I want docket-vnextapi to finish before starting deploy-local.
# This seems to only work with .NOTPARALLEL, else it starts deploying while still building.
.NOTPARALLEL:
all:
	$(error Please see README for usage)

format:
	find ./ -type f -exec dos2unix --newline {} \;


## Kube helpers
vagrant-up:
	cd $(KUBEROOT) && vagrant up
kube-up:
	cd $(KUBEROOT) && ./cluster/kube-up.sh
kube-down:
	cd $(KUBEROOT) && ./cluster/kube-down.sh
kube-up-services:
	$(KUBECTL) create -f kubernetes/myregistryService.dev.json
#	$(KUBECTL) create -f kubernetes/staticService.dev.json
#	$(KUBECTL) create -f kubernetes/vnextapiService.dev.json
	$(KUBECTL) create -f kubernetes/goapiService.dev.json
kube-up-controllers:
	$(KUBECTL) create -f kubernetes/myregistryPod.dev.json
#	$(KUBECTL) create -f kubernetes/staticController.dev.json
#	$(KUBECTL) create -f kubernetes/vnextapiController.dev.json
	$(KUBECTL) create -f kubernetes/goapiController.dev.json
kube-down-controllers:
	$(KUBECTL) delete pod myregistry
#	$(KUBECTL) delete replicationController staticController
#	$(KUBECTL) delete replicationController vnextapiController
	$(KUBECTL) delete replicationController goapiController
kube-down-services:
	$(KUBECTL) delete service myregistry
#	$(KUBECTL) delete service static
#	$(KUBECTL) delete service vnextapi
	$(KUBECTL) delete service goapi


## Used to push docker images to a local repo or a GCS repo, etc
docker-repo-local:
	docker run -e SETTINGS_FLAVOR=dev -v /tmp/registry:/tmp/registry -p 5000:5000 registry

docker-repo-gcs:
	docker run -e GCS_BUCKET=polykube-docker-registry -e GCP_OAUTH2_REFRESH_TOKEN=1/6PvQsme6855MyOf8rg54vPE48TD0CS-HW_XXunypQmkMEudVrK5jSpoR30zcRFq6 -p 5000:5000 google/docker-registry

docker-push: docker-push-vnextapi docker-push-goapi docker-push-static

docker-push-static:
	docker tag polykube/static localhost:5000/polykube/static
	docker push localhost:5000/polykube/static

docker-push-goapi:
	docker tag polykube/goapi localhost:5000/polykube/goapi
	docker push localhost:5000/polykube/goapi

docker-push-vnextapi:
	docker tag polykube/vnextapi localhost:5000/polykube/vnextapi
	docker push localhost:5000/polykube/vnextapi



docker: docker-vnextapi docker-static docker-goapi
docker-static:
	rm -f Dockerfile 
	ln -s Dockerfile-static Dockerfile
	docker build -t polykube/static .
	rm Dockerfile
docker-goapi:
	rm -f Dockerfile
	ln -s Dockerfile-goapi Dockerfile
	docker build -t polykube/goapi .
	rm Dockerfile
docker-vnextapi:
	rm -f Dockerfile
	ln -s Dockerfile-vnextapi Dockerfile
	docker build -t polykube/vnextapi .
	rm Dockerfile

run-static:
	docker run -p 20000:80  polykube/static
run-goapi:
	docker run -p 20010:80  polykube/goapi
run-vnextapi:
	docker run -p 20020:8000 polykube/vnextapi

dev-static:
	docker run -it -p 20000:80 \
		-v $(CURDIR)/services/static:/root/polykube/static \
		-w /root/polykube/static \
		polykube/static /bin/bash
dev-goapi:
	docker run -it -p 20010:80 \
		-v $(CURDIR)/services/goapi:/gopath/src/goapi \
		-w /gopath/src/goapi  \
		polykube/goapi-dev /bin/bash
docker-goapi-dev:
	rm -f Dockerfile
	ln -s Dockerfile-goapi-dev Dockerfile
	docker build -t polykube/goapi-dev .
dev-vnextapi:
	docker run -it -p 20020:8000 \
		-v $(CURDIR)/services/vnextapi:/root/polykube/vnextapi \
		-w /root/polykube/vnextapi/ \
		polykube/vnextapi /bin/bash

KUBEROOT = ~/Code/kubernetes
KUBECFG = $(KUBEROOT)/cluster/kubecfg.sh
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
kube-up-services:
	$(KUBECFG) -c misc/kubernetes/staticService.dev.json create services
	$(KUBECFG) -c misc/kubernetes/vnextapiService.dev.json create services
	$(KUBECFG) -c misc/kubernetes/goapiService.dev.json create services
kube-up-controllers:
	$(KUBECFG) -c misc/kubernetes/staticController.dev.json create replicationControllers
	$(KUBECFG) -c misc/kubernetes/vnextapiController.dev.json create replicationControllers
	$(KUBECFG) -c misc/kubernetes/goapiController.dev.json create replicationControllers
kube-down-controllers:
	$(KUBECFG) rm staticController
	$(KUBECFG) rm vnextapiController
	$(KUBECFG) rm goapiController
kube-down-services:
	$(KUBECFG) delete services/static
	$(KUBECFG) delete services/vnextapi
	$(KUBECFG) delete services/goapi


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

KUBEROOT = ~/Code/kubernetes
KUBECFG = $(KUBEROOT)/cluster/kubecfg.sh
CURDIR = $(shell pwd)

# Not sure if I'm using make correctly since I need .NOTPARALLEL
# For the all step, I want docket-vnextapi to finish before starting deploy-local.
# This seems to only work with .NOTPARALLEL, else it starts deploying while still building.
.NOTPARALLEL:
all:
	$(error Please see README for usage)



## Kube helpers
kube-up: kube-up-vnextapi kube-up-goapi kube-up-static
kube-down: kube-down-vnextapi kube-down-goapi kube-down-static

kube-up-static:
	$(KUBECFG) -c misc/kubernetes/staticController.dev.json create replicationControllers
	$(KUBECFG) -c misc/kubernetes/staticService.dev.json create services
kube-up-vnextapi:
	$(KUBECFG) -c misc/kubernetes/vnextapiController.dev.json create replicationControllers
	$(KUBECFG) -c misc/kubernetes/vnextapiService.dev.json create services
kube-up-goapi:
	$(KUBECFG) -c misc/kubernetes/goapiController.dev.json create replicationControllers
	$(KUBECFG) -c misc/kubernetes/goapiService.dev.json create services

kube-down-static:
	$(KUBECFG) stop staticController
	$(KUBECFG) rm staticController
	$(KUBECFG) delete services/static
kube-down-vnextapi:
	$(KUBECFG) stop vnextapiController
	$(KUBECFG) rm vnextapiController
	$(KUBECFG) delete services/vnextapi
kube-down-goapi:
	$(KUBECFG) stop goapiController
	$(KUBECFG) rm goapiController
	$(KUBECFG) delete services/goapi



## Used to push docker images to a local repo or a GCS repo, etc
docker-repo-local:
	docker run -e SETTINGS_FLAVOR=dev -v /tmp/registry:/tmp/registry -p 5000:5000 registry

docker-repo-gcs:
	docker run -e GCS_BUCKET=polykube-docker-registry -e GCP_OAUTH2_REFRESH_TOKEN=1/6PvQsme6855MyOf8rg54vPE48TD0CS-HW_XXunypQmkMEudVrK5jSpoR30zcRFq6 -p 5000:5000 google/docker-registry

docker-push-local: docker-push-local-vnextapi docker-push-local-goapi docker-push-local-static

docker-push-local-static:
	docker tag polykube/static localhost:5000/polykube/static
	docker push localhost:5000/polykube/static

docker-push-local-goapi:
	docker tag polykube/goapi localhost:5000/polykube/goapi
	docker push localhost:5000/polykube/goapi

docker-push-local-vnextapi:
	docker tag polykube/vnextapi localhost:5000/polykube/vnextapi
	docker push localhost:5000/polykube/vnextapi



## Build/run "production" images
docker: docker-aspvnextbase docker-vnextapi docker-static docker-goapi

docker-aspvnextbase:
	rm -f Dockerfile
	ln -s Dockerfile-aspvnextbase Dockerfile
	docker build -t polykube/aspvnextbase .
	rm Dockerfile

docker-static:
	rm -f Dockerfile 
	ln -s Dockerfile-static Dockerfile
	docker build -t polykube/static .
	rm Dockerfile
run-static:
	docker run -p 20000:80  polykube/static

docker-goapi:
	rm -f Dockerfile
	ln -s Dockerfile-goapi Dockerfile
	docker build -t polykube/goapi .
	rm Dockerfile
run-goapi:
	docker run -p 20010:80  polykube/goapi

docker-vnextapi:
	rm -f Dockerfile
	ln -s Dockerfile-vnextapi Dockerfile
	docker build -t polykube/vnextapi .
	rm Dockerfile
run-vnextapi:
	docker run -p 20020:8000 polykube/vnextapi


## Build/run interactive dev containers
# (goapi needs its own dev container)
# (static is already a most minimal image, even for a dev env)
#    (this will change as other build steps are added for html/css/js)
# (vnextapi doesn't pack into a minimal binary yet, so dev==prod container)
run-static-dev:
	docker run -it -p 20000:80 \
		-v $(CURDIR)/src/static:/root/polykube/static \
		-w /root/polykube/static \
		polykube/static /bin/bash

docker-goapi-dev:
	rm -f Dockerfile
	ln -s Dockerfile-goapi-dev Dockerfile
	docker build -t polykube/goapi-dev .
run-goapi-dev:
	docker run -it -p 20010:80 \
		-v $(CURDIR)/src/goapi:/gopath/src/goapi \
		-w /gopath/src/goapi  \
		polykube/goapi-dev /bin/bash

run-vnextapi-dev:
	docker run -it -p 20020:80 \
		-v $(CURDIR)/src/vnextapi:/root/polykube/vnextapi \
		-w /root/polykube/vnextapi/ \
		polykube/vnextapi /bin/bash

KUBECFG = ~/Code/kubernetes/cluster/kubecfg.sh
CURDIR = $(shell pwd)

# Not sure if I'm using make correctly since I need .NOTPARALLEL
# For the all step, I want docket-vnextapi to finish before starting deploy-local.
# This seems to only work with .NOTPARALLEL, else it starts deploying while still building.
.NOTPARALLEL:

all: docker-vnextapi deploy-local

## Kube helpers
kube-up:
	$(KUBECFG) -c misc/kubernetes/vnextapiController.dev.json create replicationControllers
	$(KUBECFG) -c misc/kubernetes/vnextapiService.dev.json create services

kube-down:
	$(KUBECFG) stop vnextapiController
	$(KUBECFG) rm vnextapiController
	$(KUBECFG) delete services/vnextapi

## Local docker stuff
local-docker-repo:
	docker run -e SETTINGS_FLAVOR=dev -p 5000:5000 registry

deploy-local:
	docker tag polykube/vnextapi localhost:5000/polykube/vnextapi
	docker push localhost:5000/polykube/vnextapi

## Build docker images for our stuff
docker-vnextapi:
	# TODO: remove this grossness when this patch is taken:
	# https://github.com/docker/docker/issues/7284
	rm -f Dockerfile
	cp Dockerfile-vnextapi Dockerfile
	docker build --no-cache -t polykube/vnextapi .
	rm Dockerfile

## Run dev container
run:
	docker run                         \
		-p 8000:8000                     \
		polykube/vnextapi

dev:
	docker run                         \
		-p 8000:8000                     \
		-v $(CURDIR):/root/polykube-dev  \
		-it                              \
		polykube/vnextapi                \
		/bin/bash
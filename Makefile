KUBEROOT = ~/Code/kubernetes
KUBECFG = $(KUBEROOT)/cluster/kubecfg.sh
CURDIR = $(shell pwd)

# Not sure if I'm using make correctly since I need .NOTPARALLEL
# For the all step, I want docket-vnextapi to finish before starting deploy-local.
# This seems to only work with .NOTPARALLEL, else it starts deploying while still building.
.NOTPARALLEL:

all:
	exit # See README



## Kube helpers
kube-local:
	$(KUBEROOT)/hack/local-up-cluster.sh

kube-up: kube-up-vnextapi kube-up-goapi kube-up-static

kube-up-vnextapi:
	$(KUBECFG) -c misc/kubernetes/vnextapiController.dev.json create replicationControllers
	$(KUBECFG) -c misc/kubernetes/vnextapiService.dev.json create services
kube-up-goapi:
	$(KUBECFG) -c misc/kubernetes/goapiController.dev.json create replicationControllers
	$(KUBECFG) -c misc/kubernetes/goapiService.dev.json create services
kube-up-static:
	$(KUBECFG) -c misc/kubernetes/staticController.dev.json create replicationControllers
	$(KUBECFG) -c misc/kubernetes/staticService.dev.json create services
kube-up-registrator:
	# well, this is not the way to use registrator but I Want to be able to check this in and show it
	# it doesn't work, just spawns a bunch of pauses
	$(KUBECFG) -c misc/kubernetes/registratorController.dev.json create replicationControllers

kube-down:
	$(KUBECFG) stop vnextapiController; \
	$(KUBECFG) rm vnextapiController; \
	$(KUBECFG) delete services/vnextapi; \
	$(KUBECFG) stop goapiController; \
	$(KUBECFG) rm goapiController; \
	$(KUBECFG) delete services/goapi; \
	$(KUBECFG) stop staticController; \
	$(KUBECFG) rm staticController; \
	$(KUBECFG) delete services/static


## Local docker stuff
local-docker-repo:
	docker run -e SETTINGS_FLAVOR=dev -v /tmp/registry:/tmp/registry -p 5000:5000 registry

deploy-local: deploy-local-vnextapi deploy-local-goapi deploy-local-static

deploy-local-vnextapi:
	docker tag polykube/vnextapi localhost:5000/polykube/vnextapi
	docker push localhost:5000/polykube/vnextapi

deploy-local-goapi:
	docker tag polykube/goapi localhost:5000/polykube/goapi
	docker push localhost:5000/polykube/goapi

deploy-local-static:
	docker tag polykube/static localhost:5000/polykube/static
	docker push localhost:5000/polykube/static



## Build docker images for our stuff
docker: docker-vnextapi docker-static docker-goapi

docker-registrator:
	docker run                                                \
		-v /var/run:/mnt/host/var/run                           \
		-e DOCKER_HOST=unix:///mnt/host/var/run/docker.sock     \
		progrium/registrator                                    \
		etcd://10.0.0.2:4001/test

docker-registrator2:
	docker run                                                \
		-v /var/run:/mnt/host/var/run                           \
		-e DOCKER_HOST=unix:///mnt/host/var/run/docker.sock     \
		--entrypoint="registrator"                              \
		progrium/registrator                                    \
		etcd://10.0.0.2:4001/test

docker-vnextapi:
	# TODO: remove this grossness when this patch is taken: https://github.com/docker/docker/issues/7284
	rm -f Dockerfile
	cp Dockerfile-vnextapi Dockerfile
	docker build -t polykube/vnextapi .
	rm Dockerfile

docker-static:
	# TODO: remove this grossness when this patch is taken: https://github.com/docker/docker/issues/7284
	rm -f Dockerfile
	cp Dockerfile-static Dockerfile
	docker build -t polykube/static .
	rm Dockerfile

docker-goapi:
	# TODO: remove this grossness when this patch is taken: https://github.com/docker/docker/issues/7284
	rm -f Dockerfile
	cp Dockerfile-goapi Dockerfile
	docker build -t polykube/goapi .
	rm Dockerfile

## Open interactive dev containers
dev-vnextapi:
	docker run                                     \
		-p 20020:8000                                \
		-v $(CURDIR):/root/polykube-dev              \
		-it                                          \
		-w /root/polykube-dev/src/Polykube.vNextApi  \
		polykube/vnextapi                            \
		/bin/bash

dev-static:
	docker run                                            \
		-p 20000:80                                         \
		-v $(CURDIR)/src/static:/root/polykube-static-dev   \
		-it                                                 \
		polykube/static                                     \
		/bin/bash -c "rm /root/polykube-static-active && ln -s /root/polykube-static-dev /root/polykube-static-active && /bin/bash" \

dev-goapi:
	docker run                         \
		-p 20010:80                      \
		-v $(CURDIR):/root/polykube-dev  \
		-it                              \
		-w /root/polykube-dev/src/goapi  \
		polykube/goapi                   \
		/bin/bash


## Run non-interactive containers
run-vnextapi:
	docker run -p 20020:80 polykube/vnextapi

run-static:
	docker run -p 20000:80  polykube/static

run-goapi:
	docker run -p 20010:80  polykube/goapi
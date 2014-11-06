# polykube

Polykube is a Kubernetes-deployable web service that consists of three microservices:

1. `vnextapi`, which is a Microsoft ASP.NET Web API vNext service. This runs on Mono and uses Owin/Nowin for the server implementation.
2. `goapi` is a simple proof-of-concept service written in Go (golang).
3. `static` is a small docker container exposing nginx and some static html/js/css content.

## Motivations

1. Another Kubernetes example

2. Learn docker and friends, Kubernetes, etc

3. Want to learn the best possible way to design services

## Notes to self

0. Determine if Go Api is properly staticly built. There's a warning about getaddrinfo as it is now.

1. Investigate if `kpm pack` adds any benefit (will it build? produce a versioned binary to container-ize separately from the source container?)

2. Revisit minimalistic docker images when Dockerfile2 lands or this patch: https://github.com/docker/docker/pull/8021

3. Still not sure what the easiest way to do docker registry is

## Planned Features

1. Service Discovery
2. Sharding support (and addressing)
3. Tested deployment to Azure (sporadically fails to verify master, sometimes works)
3. Tested deployment to GCE (fails to verfiy master, has worked once)
3. Tested deployment to Vagrant (fails to verify master, has never worked)


## Assumptions

The commands in this README assume that you have Kubernetes cloned in `~/Code/kubernetes` and this code cloned in `~/Code/polykube`.


## `Makefile`

### `make docker`
This will build all of the production service container images. `make docker-{static,goapi,vnextapi}` or invidual builds.

### `make docker-dev`
This will build all of the production service container images. `make docker-{static,goapi,vnextapi}-dev` or invidual builds.

### `make run-{static,goapi,vnextapi}`
This will launch the production container locally.

### `make run-{static,goapi,vnextapi}-dev`
This will launch the development container locally with volumes mapped to source on the host.


### `make local-docker-repo`
Start a local docker repo (used to serve images for Kubernetes in local configuration)

### `make deploy-local`
Push all of the docker images to the local docker repo started with `make local-docker-repo`. This is only required for local kubernetes deployment.


### `make kube-up`
Bring up all of the kubernetes replicationControllers and services.

### `make kube-down`
Bring down all of the kubernetes replicationControllers and services. (This doesn't kill all docker containers, not sure if I'm doing something wrong...)


## How to Deploy

1. Bring up the docker registry
2. Push the docker images to the docker registry
3. Bring up the other helping services
4. Turn on the replication controllers for all of them

## Notes

### Ports

- Docker port is the port that is used when running `make dev-{servicename}`
- Kube ctrlr is the port exposed by the container under kubernetes
- Kube srvc is the port exposed by service under kubernetes (this should be used to access, not Kube ctrlr)
- Internal is the port exposed by the service inside the container

Docker port | Kube ctrlr | Kube srvc | Internal | Service
------------|------------|-----------|-----------|--------
      20020 |      30020 |     10020 |     8000 | vnextapi
      20000 |      30000 |     10000 |       80 | static
      20010 |      30010 |     10010 |       80 | goapi

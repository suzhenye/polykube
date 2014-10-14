# polykube

Polykube is a web service that consists of three microservices.

1. `vnextapi`, which is a Microsoft ASP.NET Web API vNext service. This runs on Mono and uses Owin/Nowin for the server implementation.
2. `goapi` is a simple proof-of-concept service written in Go (golang).
3. `static` is a small docker container exposing nginx and some static html/js/css content.

This is deployable using Kubernetes. Tested with a local cluster. Going to test with Azure soon.


## Motivations

* Example of a kubernetes project that is, at least somewhat, non-trivial
* Exercise some sharding/discovery ideas

## Features - Already Done

1. ASP.NET vNext (development, K Runtime, packaging, etc)
2. Simple golang service (just to prove out service discovery later)
3. Docker: using it for deployment, and repeatable builds hopefully (vnextapi is deployed like a dynamic app)
4. Local Kubernetes Deployments (aka, "local" on Docker; not "vagrant" with Docker on Vagrant VMs)


## Features - Added Recently

0. Fixed NuGet.Config (CASING IS VERY IMPORTANT FOR THIS FILENAME) so that it builds again against myget.org properly.
1. Multiphase `Dockerfile`s: GoApi service


## Features - Planned

0. Determine if Go Api is properly staticly built. There's a warning about getaddrinfo as it is now.

1. Investigate if `kpm pack` adds any benefit whatsoever

2. Fix vnextapi (broke with KRE-alpha4, so bumped pkgs up to alpha4 and will fix soon)

## Features - Longer Term

1. Service Discovery
2. Sharding support (and addressing)
3. Tested deployment to Azure (sporadically fails to verify master, sometimes works)
3. Tested deployment to GCE (fails to verfiy master, has worked once)
3. Tested deployment to Vagrant (fails to verify master, has never worked)


## Dependencies

1. Docker (Since boot2docker runs a virtual machine with Docker inside, you'll need to do two layers of forwarding to enable volume mounting for the development containers.)

2. Docker must be patched to support nested builds: https://github.com/docker/docker/pull/8021


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
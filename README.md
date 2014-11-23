# polykube

Polykube is a Kubernetes-deployable web service that consists of three microservices:

1. `vnextapi`, which is an [ASP.NET 5](http://www.asp.net/vnext/overview/aspnet-vnext/aspnet-5-overview), [MVC 6 Web API](http://www.asp.net/vnext/overview/aspnet-vnext/create-a-web-api-with-mvc-6) service (hosted by [Kestrel](https://githu
b.com/aspnet/KestrelHttpServer)).
2. `goapi` is a simple proof-of-concept service written in [Go](http://golang.org).
3. `static` is [nginx](http://nginx.org) exposing some static html/js/css content.


## Motivations

1. I like deployment orchestration/infrastructure technology.

2. Learn docker, Kubernetes, aspnet5, etc.


## Notes to self

0. Determine if Go Api is properly staticly built. There's a warning about getaddrinfo as it is now.

1. Investigate if `kpm pack` adds any benefit (will it build? produce a versioned binary to container-ize separately from the source container?)

2. Revisit minimalistic docker images when Dockerfile2 lands or this patch: https://github.com/docker/docker/pull/8021

3. Document docker-registry strategy for local testing and for prepping for a real cloud deploy

4. Investigate static hard-coded routes for ASP.NET 5. I don't know if I like the attribute based routing. It's nice to have an easy mental model. Request comes in, and there's the class which contains the list of routes. Versus searching through all classes accessible and seeing if they have a route attribute.

## Quick features to add

1. Use kubectl

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

### `make run-{static,goapi,vnextapi}`
This will launch the production container locally.

### `make dev-{static,goapi,vnextapi}`
This will launch the development container locally with volumes mapped to source on the host.


### `make docker-repo-local`
Starts a docker repo listening on localhost:5000 that is connected to local storage (used for local development).

### `make docker-repo-gcs`
Starts a docker repo listening on localhost:5000 that is connected to GCS storage. This is also what's configured in the Kubernetes config files. (Which I probably shouldn't have checked in)


### `make kube-up-services`
Bring up kubernetes services (static, vnextapi, goapi). `make kube-down-services` is the inverse.

### `make kube-up-controllers`
Bring up kubernetes replication controllers (static, vnextapi, goapi). `make kube-down-controllers` is the inverse.


## How to Deploy

1. Bring up the docker registry
2. Push the docker images to the docker registry
3. Create kube services.
4. Turn up kube replication controllers

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

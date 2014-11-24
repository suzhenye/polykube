# polykube

Polykube is a Kubernetes-deployable web service that consists of three microservices:

1. `vnextapi`, which is an [ASP.NET 5](http://www.asp.net/vnext/overview/aspnet-vnext/aspnet-5-overview), [MVC 6 Web API](http://www.asp.net/vnext/overview/aspnet-vnext/create-a-web-api-with-mvc-6) service (hosted by [Kestrel](https://github.com/aspnet/KestrelHttpServer)).
2. `goapi` is a simple proof-of-concept service written in [Go](http://golang.org).
3. `static` is [nginx](http://nginx.org) exposing some static html/js/css content.

It currently can be deployed to, at least, Vagrant and Google Cloud.


## Planned Features

1. Service Discovery
2. Sharding support (and addressing)
3. Tested deployment to Azure (wasn't working in October)
3. Tested deployment to GCE (works)


## Assumptions

The commands in this README assume that you have Kubernetes cloned in `~/Code/kubernetes` and this code cloned in `~/Code/polykube`.


## Quick start (example)

This will clone the repo and launch an ASP.NET container linked to the source in the tree.
You can edit the source in your host machine and then execute `make run` inside the launched docker container to build and run the vnextapi service.

```
git clone https://github.com/colemickens/polykube
cd polykube;
make docker-vnextapi; # builds the vnextapi container
make dev-vnextapi;    # launches the dev container linked to host source code
container> make run   # runs `kpm restore` and starts the app on kestrel
```


## Full `Makefile` Information

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

### Revisit Later

0. Determine if Go Api is properly staticly built. There's a warning about getaddrinfo as it is now.

1. Investigate if `kpm pack` adds any benefit (will it build? produce a versioned binary to container-ize separately from the source container?)

2. Revisit minimalistic docker images when Dockerfile2 lands or this patch: https://github.com/docker/docker/pull/8021

4. Investigate static hard-coded routes for ASP.NET 5.
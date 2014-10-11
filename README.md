# polykube

Polykube is a web service that consists of three microservices.

1. `vnextapi`, which is a Microsoft ASP.NET Web API vNext service. This runs on Mono and uses Owin/Nowin for the server implementation.
2. `goapi` is a simple proof-of-concept service written in Go (golang).
3. `static` is a small docker container exposing nginx and some static html/js/css content.

This is deployable using Kubernetes. Tested with a local cluster. Going to test with Azure soon.

## Motivations

* Example of a kubernetes project that is, at least somewhat, non-trivial
* Exercise some sharding/discovery ideas
* Demonstrate how Docker can allow projects to create repeatable builds and development environments. A
  single command should drop you into a working build environment, and a single command should be able to
  reproduceably build a service container for each component.

## Implemented

1. ASP.NET vNext (development, K Runtime, packaging, etc)
2. Simple golang service (just to prove out service discovery later)
3. Docker: using it for deployment, and repeatable builds hopefully (vnextapi is deployed like a dynamic app)
4. Kubernetes deployments


## Implementing sooner than soon

1. Multiphase `Dockerfile`s for building in a container and outputting a minimal runnable container
2. Restructure the container logic for "dev" environments that are linked to their source/dev dirs in the host environment

## Implementing soon

1. Service Discovery
2. Sharding support (and addressing)
3. Deployment to Azure
4. Deployment to local Vagrant cluster
5. Deployment to GCE
6. Build actual packages for project, rather than packaging source into Docker container

Deployment to Azure, Vagrant and GCE have all had a number of issues. I've only deployed a kubernetes cluster successfully, once, despite trying half a dozen times in each Azure, GCE and Vagrant.


## Dependencies

1. Docker (If you use boot2docker you will have to do some workarounds for the dev containers to be able to mount virtual volumes. Since boot2docker runs a virtual machine with Docker inside, you'll need to do two layers of forwarding.)


## Assumptions

The commands in this README assume that you have Kubernetes cloned in `~/Code/kubernetes` and this code cloned in `~/Code/polykube`.


## Quick Start

### In interactive, dev-friendly containers
See below under instructions for `make dev-{servicename}`. Each service must be started individually and run in their own window in this mode.

### In containers
See below under instructions for `make run-{servicename}`. Each service must be started individually and run in their own window in this mode.

### In kubernetes (locally)
Start a local kubernetes instance. See below if you don't know how, or the Kubernetes docs. Then, see below under instructions for `make kube-up`

### In kubernetes (azure)
Whenever I can try to figure it out and write it up...




## `Makefile`

### `make docker`
This will build the docker images for the various services.
(Also: `make docker-vnextapi` or `make docker-static` or `make docker-goapi`)

### `make local-docker-repo`
Start a local docker repo (used to serve images for Kubernetes in local configuration)

### `make deploy-local`
Push all of the docker images to the local docker repo started with `make local-docker-repo`. This is only required for local kubernetes deployment.

### `make run-{servicename}`
These commands will run the service containers with the ports as described below (the Docker column).

### `make dev-{servicename}`
They modify `docker run` command with the standard service containers (same ports as with `make run-{servicename}`) to map in the source for the service and then execute commands to ensure that the dev code is being served from the container. This allows you to edit the source code on your host machine, and then rebuild and run it immediately in the container.

```
cole@chimera>> make dev-vnextapi

root@bd91580b2abf:~/polykube-dev/src/Polykube.vNextApi# ls
# ExampleController.cs  Startup.cs  config.json  k_daemon.sh  project.json  start_k_daemon.sh

root@bd91580b2abf:~/polykube-dev/src/Polykube.vNextApi# k web
# press [enter], `k web` will exit
# edit the csharp source code in the source tree

root@bd91580b2abf:~/polykube-dev/src/Polykube.vNextApi# k web
# observe the changes!
```

```
cole@chimera>> make dev-goapi

# this isn't finished yet
```

```
cole@chimera>> make dev-static

[ root@f21a88b6c0b0:~ ]$ ls /root/polykube-static-active/www
# 404.html  css/  index.html  js/

[ root@f21a88b6c0b0:~ ]$ nginx
# press [ctrl]+c, nginx will exit
# edit the static files in the source tree

[ root@f21a88b6c0b0:~ ]$ nginx
# observe the changes!
```

### `make kube-up`
Bring up all of the kubernetes replicationControllers and services.

### `make kube-down`
Bring down all of the kubernetes replicationControllers and services. (This doesn't kill all docker containers, not sure if I'm doing something wrong...)

## Notes

### Starting a local kubernetes cluster

```
git clone https://github.com/GoogleCloudPlatform/kubernetes.git ~/Code/kubernetes
cd ~/Code/kubernetes
./hack/local-up-cluster.sh
```

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

### Bugs

- `kpm restore` fails to find nuget.config, even when it's next to global.json...
- `k_daemon.sh` existing

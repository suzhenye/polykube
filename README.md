# polykube

As of now, this is a service, named "polykube" that is comprised of microservices. There are currently three: `vnextapi`, `goapi`, `static`. `vnext` is an ASP.NET vNext application. `goapi` is a golang application (net/http). `static` is just nginx with a simple http/js client that accesses `vnextapi` and `goapi` via CORS-enabled HTTP.

This is deployable using Kubernetes. Tested with a local cluster. Going to test with Azure soon.

## Implemented

1. ASP.NET vNext (development, K Runtime, packaging, etc)
2. Simple golang service (just to prove out service discovery later)
3. Docker: using it for deployment, and repeatable builds hopefully (vnextapi is deployed like a dynamic app)
4. Kubernetes deployments


## Implementing soon

0. Deployment to Azure
1. Service discovery via key value stores - probably etcd
2. Add registrator (may not be possible...)
3. Consume registrator from vnextapi project
4. Consume registrator from new golang project (+ docker config for it, kube cfg or it, etc)
5. Have a static js frontend served by JS that talks to my two backends.
  - It will show the various backend machines available that they can find from discovery



## Dependencies

1. Docker (so, Linux, or boot2docker)

2. These `cd`s expect you to have `kubernetes` and `polykube` checked out in ~/Code/. Change them accordingly if you need to. (Note, the Makefile in `polykube` references `kubernetes` at ~/Code/kubernetes, so you have to change it there as well.)



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
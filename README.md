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
4. Local Kubernetes Deployments (aka, "local" on Docker; not "vagrant" with Docker on Vagrant VMs)

## Recently added

0. Fixed NuGet.Config (CASING IS VERY IMPORTANT FOR THIS FILENAME) so that it builds again against myget.org properly.
1. Multiphase `Dockerfile`s: GoApi service

## Soon

0. Fix GoApi service so that it's properly static, and test it, etc.

1. Multiphase `Dockerfile`s: VNext service: Need to output a mono-3.10 DEB package, give it to the next build to install for more minimal image

2. Stop using the final service containers as a psuedo-dev environment. Instead, setup actual Dockerfiles that are only responsible for dropping into a proper build environment. This will also simplify some of the trickery occuring in the prod service images now that are a result of enabling the dev containers.

3. Evaluate packaging my service bits into a DEB or TAR or something as well. Right now it's just shuffling around binaries, which might be fine.

## Planned

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
This will build the docker images for the various services.
(Also: `make docker-vnextapi` or `make docker-static` or `make docker-goapi`)

### `make local-docker-repo`
Start a local docker repo (used to serve images for Kubernetes in local configuration)

### `make deploy-local`
Push all of the docker images to the local docker repo started with `make local-docker-repo`. This is only required for local kubernetes deployment.

### `make run-{servicename}`
These commands will run the service containers with the ports as described below (the Docker column).

### `make dev-{servicename}`

(This section currently does not match the real state of the code...)

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

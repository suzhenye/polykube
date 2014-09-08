# AgoraSolution

This is a playground for me to experiment with the following things:

1. ASP.NET vNext (development, K Runtime, packaging, etc)
2. Kubernetes
3. Possibly Mesos+Chronos

Note: Thanks to [prozachj](https://github.com/prozachj) for [the Docker image](https://github.com/ProZachJ/docker-mono-aspnetvnext).

## Brainstorming

1. Build a Dockerfile for building the solution and dropping the docker container outputs.
2. That way anyone can build who can run a docker container.
3. Write a kubernetes profile for deploying my docker container(s).
4. 

## Todo

1. Configure FxCop/StyleCop if they work with Linux.

## Running

Build the api docker image:

```
git clone github.com/colemickens/Agora Agora
cd Agora/src/Agora.Api
docker build -t agora_api .
docker run agora_api
```

Start kubernetes cluser locally (skip this if you're not on Linux, improvise with another Kubernetes cluster):

```

```

Then deploy to the kubernetes cluster:

```
cluster/kubecfg.sh -c pod.json create pods
cluster/kubecfg.sh -c service.json create services
```

## Development

Start the docker container:
```
export SDVNEXTPATH=~/Code/vnext/AgoraSolution
docker \
  run \
  -i \
  -v $SDVNEXTPATH:/root/AgoraSolution \
  -p 80:5000 \
  -t prozachj/docker-mono-aspnetvnext \
  /bin/bash
```

At this point you'll be dropped in the docker container with access to the AgoraSolution folder. Finally:

```
cd /root/AgoraSolution/
kpm restore
cd Agora.Api/
k web
```
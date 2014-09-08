# AgoraSolution

This is a playground for me to experiment with the following things:

1. ASP.NET vNext (development, K Runtime, packaging, etc)
2. Docker support
2. Kubernetes API in .NET

Note: Thanks to [prozachj](https://github.com/prozachj) for [the Docker image](https://github.com/ProZachJ/docker-mono-aspnetvnext).

## Additional ideas

1. Build a Dockerfile for building the solution and dropping the docker container outputs.
2. Chronos (by implication Mesos?)

## Running

Run a local docker regsitry in docker
```
docker run -e SETTINGS_FLAVOR=dev -p 5000:5000 registry
```

Build the agora-api docker image
```
git clone github.com/colemickens/Agora ~/Code/Agora
cd ~/Code/Agora/src/Agora.Api
docker build -t agora_api .
```

Push it to the local docker repo
```
docker tag agora-api localhost:5000/agora-api
docker push localhost:5000/agora-api
```

Start kubernetes cluser locally (or some other way):
```
cd ~/Code/kubernetes
hack/local-up-cluster.sh
alias kubecfg=~/Code/kubernetes/cluster/kubecfg.sh
```

Then deploy to the kubernetes cluster (warning, this assumes you have set your environment variables for your cluster type, or have override the default!):
```
kubecfg -c misc/kubernetes/frontendController.dev.json create replicationControllers
kubecfg -c misc/kubernetes/frontendService.dev.json create services
```

## Development

Start the docker container:
```
export SDVNEXTPATH=~/Code/vnext/Agora
docker \
  run \
  -i \
  -v $SDVNEXTPATH:/root/Agora \
  -p 80:5000 \
  -t prozachj/docker-mono-aspnetvnext \
  /bin/bash
```

At this point you'll be dropped in the docker container with access to the Agora folder. Finally:
```
cd /root/Agora/
kpm restore
cd src/Agora.Api/
./k_daemon.sh web
```

Then you can iterate by editting files in the host and restarting `k web` in the container. Yay, native editting with containized build and running.
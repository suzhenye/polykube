# polykube

Multiple languages, multiple service discovery patterns.

This is a playground for me to experiment with the following things:

1. ASP.NET vNext (development, K Runtime, packaging, etc)
2. Docker support
3. Kubernetes API in .NET
4. Other apps in other languages playing nice with my choice of service discovery, etc

Note: Thanks to [prozachj](https://github.com/prozachj) for [the ASP.NET vNext Docker image](https://github.com/ProZachJ/docker-mono-aspnetvnext).

## Additional ideas

1. Build a Dockerfile for building the solution and dropping the docker container outputs.
2. Chronos (by implication Mesos?)

## Running

This relies on the location of kubernetes (`~/Code/kubernetes`).
This will run a single instance of `vnextapi` in a container. You can see it at http://localhost:8000.

```
make
make run
```

## Running on Kubernetes

Start a cluster somehow. If you don't know how, you can start one locally:
```
cd ~/Code/kubernetes
./hack/local-up-cluster.sh
```

Now:
```
make kube-up
```

The service is accessible on one of your minions at port 10000. Unfortunately, Kubernetes currently requires manual inspection for wiring up DNS. (note, if you're running on localhost, it's just http://localhost:10000/)

## Bugs

- `kpm restore` fails to find nuget.config, even when it's next to global.json...
- `k_daemon.sh` existing
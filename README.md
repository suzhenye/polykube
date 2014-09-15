# polykube

Multiple languages, multiple service discovery patterns.

This is a playground for me to experiment with random things.

## Implemented

1. ASP.NET vNext (development, K Runtime, packaging, etc)
2. Docker: using it for deployment, and repeatable builds hopefully (vnextapi is deployed like a dynamic app)
3. Service discovery via key value stores - probably etcd

## Plan to implement

1. Add registrator
2. Consume registrator from vnextapi project
3. Consume registrator from new golang project (+ docker config for it, kube cfg for it, etc)
4. Have a static js frontend served by JS that talks to my two backends.
  - It will show the various backend machines available that they can find from discovery

## Future ideas

1. Chronos (by implication Mesos?)

## Running

### Dependencies

1. Docker (so, Linux, or boot2docker)

These `cd`s expect you to have `kubernetes` and `polykube` checked out in ~/Code/. Change them accordingly if you need to. (Note, the Makefile in `polykube` references `kubernetes` at ~/Code/kubernetes, so you have to change it there as well.)

This will run a single instance of `vnextapi` in a container. You can see it at http://localhost:8000.
```
cd ~/Code/polykube
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
cd ~/Code/polykube
make kube-up
```

The service is accessible on one of your minions at port 10000. Unfortunately, Kubernetes currently requires manual inspection for wiring up DNS. (note, if you're running on localhost, it's just http://localhost:10000/)

## Bugs

- `kpm restore` fails to find nuget.config, even when it's next to global.json...
- `k_daemon.sh` existing

## Notes

- Thanks to [prozachj](https://github.com/prozachj) for [the ASP.NET vNext Docker image](https://github.com/ProZachJ/docker-mono-aspnetvnext).
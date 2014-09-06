# AgoraSolution

## About

This will use some sort of container technology:
- Start an etcd service
- Start a microservice or two and register themselves to etcd
- Shared configuration for environments across the services
- Maybe play with Chronos
- Orchestrate it with Kubernetes or Mesos or something

This currently piggybacks on the work of prozachj. Thanks prozachj!

## Running

Prep the docker container and this source code:
```
export SDVNEXTPATH=~/Code/vnext/AgoraSolution

mkdir -p $SDVNEXTPATH
git clone github.com/colemickens/AgoraSolution $SDVNEXTPATH

docker pull prozachj/docker-mono-aspnetvnext
```

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
cd /root/AgoraSolution/Agora.Api
kpm restore
k web
```

## Next steps

1. Get a simple route working with a REST-y style Web API
2. Simple service discovery API with a .NET service and [something else] service registering themselves
3. Figure out deployment on someone's Docker platform (Deis? Flynn? Kubernetes? Kubernetes+Mesos?)
4. Figure out what others use for pressing Docker images? Docker build tools. I know I've read about some.
5. Heavily document day-to-day dev instructions, building, packaging and deployment

## Current Problems

### Problem 1

I can't run `kpm pack`. Note the length of the path at the end:

```
root@56e60c4f55ad:~/AgoraSolution/Agora.Api# kpm pack
verbose:False out: project:
Copying to output path /root/AgoraSolution/Agora.Api/bin/output
Packing nupkg dependency Kestrel 1.0.0-alpha3-10117
  Target /root/AgoraSolution/Agora.Api/bin/output/approot/packages/Kestrel/1.0.0-alpha3-10117
Packing nupkg dependency Microsoft.AspNet.Mvc 6.0.0-alpha3-10424
  Target /root/AgoraSolution/Agora.Api/bin/output/approot/packages/Microsoft.AspNet.Mvc/6.0.0-alpha3-10424
Packing nupkg dependency Microsoft.AspNet.Hosting 1.0.0-alpha3-10147
  Target /root/AgoraSolution/Agora.Api/bin/output/approot/packages/Microsoft.AspNet.Hosting/1.0.0-alpha3-10147
Packing nupkg dependency Microsoft.AspNet.Owin 1.0.0-alpha3-10154
  Target /root/AgoraSolution/Agora.Api/bin/output/approot/packages/Microsoft.AspNet.Owin/1.0.0-alpha3-10154
Packing nupkg dependency Microsoft.AspNet.Server.Kestrel 1.0.0-alpha3-10117
  Target /root/AgoraSolution/Agora.Api/bin/output/approot/packages/Microsoft.AspNet.Server.Kestrel/1.0.0-alpha3-10117
Packing nupkg dependency Microsoft.AspNet.FileSystems 1.0.0-alpha3-10132
  Target /root/AgoraSolution/Agora.Api/bin/output/approot/packages/Microsoft.AspNet.FileSystems/1.0.0-alpha3-10132
Packing nupkg dependency Microsoft.AspNet.Http 1.0.0-alpha3-10154
  Target /root/AgoraSolution/Agora.Api/bin/output/approot/packages/Microsoft.AspNet.Http/1.0.0-alpha3-10154
Packing nupkg dependency Microsoft.AspNet.Mvc.Common 6.0.0-alpha3-10424
  Target /root/AgoraSolution/Agora.Api/bin/output/approot/packages/Microsoft.AspNet.Mvc.Common/6.0.0-alpha3-10424
Packing nupkg dependency Microsoft.AspNet.Mvc.Core 6.0.0-alpha3-10424
  Target /root/AgoraSolution/Agora.Api/bin/output/approot/packages/Microsoft.AspNet.Mvc.Core/6.0.0-alpha3-10424
Packing nupkg dependency Microsoft.AspNet.Mvc.ModelBinding 6.0.0-alpha3-10424
  Target /root/AgoraSolution/Agora.Api/bin/output/approot/packages/Microsoft.AspNet.Mvc.ModelBinding/6.0.0-alpha3-10424
Packing nupkg dependency Microsoft.AspNet.Mvc.Razor 6.0.0-alpha3-10424
  Target /root/AgoraSolution/Agora.Api/bin/output/approot/packages/Microsoft.AspNet.Mvc.Razor/6.0.0-alpha3-10424
Packing nupkg dependency Microsoft.AspNet.Mvc.Razor.Host 6.0.0-alpha3-10424
  Target /root/AgoraSolution/Agora.Api/bin/output/approot/packages/Microsoft.AspNet.Mvc.Razor.Host/6.0.0-alpha3-10424
Packing nupkg dependency Microsoft.AspNet.Razor 4.0.0-alpha3-10137
  Target /root/AgoraSolution/Agora.Api/bin/output/approot/packages/Microsoft.AspNet.Razor/4.0.0-alpha3-10137
Packing nupkg dependency Microsoft.AspNet.RequestContainer 1.0.0-alpha3-10147
  Target /root/AgoraSolution/Agora.Api/bin/output/approot/packages/Microsoft.AspNet.RequestContainer/1.0.0-alpha3-10147
Packing nupkg dependency Microsoft.AspNet.Routing 1.0.0-alpha3-10145
  Target /root/AgoraSolution/Agora.Api/bin/output/approot/packages/Microsoft.AspNet.Routing/1.0.0-alpha3-10145
Packing nupkg dependency Microsoft.Framework.ConfigurationModel 1.0.0-alpha3-10139
  Target /root/AgoraSolution/Agora.Api/bin/output/approot/packages/Microsoft.Framework.ConfigurationModel/1.0.0-alpha3-10139
Packing nupkg dependency Microsoft.Framework.DependencyInjection 1.0.0-alpha3-10135
  Target /root/AgoraSolution/Agora.Api/bin/output/approot/packages/Microsoft.Framework.DependencyInjection/1.0.0-alpha3-10135
Packing nupkg dependency Microsoft.AspNet.FeatureModel 1.0.0-alpha3-10154
  Target /root/AgoraSolution/Agora.Api/bin/output/approot/packages/Microsoft.AspNet.FeatureModel/1.0.0-alpha3-10154
Packing nupkg dependency Microsoft.AspNet.PipelineCore 1.0.0-alpha3-10154
  Target /root/AgoraSolution/Agora.Api/bin/output/approot/packages/Microsoft.AspNet.PipelineCore/1.0.0-alpha3-10154
Packing nupkg dependency Microsoft.AspNet.Security.DataProtection 1.0.0-alpha3-10132
  Target /root/AgoraSolution/Agora.Api/bin/output/approot/packages/Microsoft.AspNet.Security.DataProtection/1.0.0-alpha3-10132
Packing nupkg dependency Microsoft.Framework.Logging 1.0.0-alpha3-10133
  Target /root/AgoraSolution/Agora.Api/bin/output/approot/packages/Microsoft.Framework.Logging/1.0.0-alpha3-10133
Packing nupkg dependency Microsoft.Framework.Runtime.Interfaces 1.0.0-alpha3-10165
  Target /root/AgoraSolution/Agora.Api/bin/output/approot/packages/Microsoft.Framework.Runtime.Interfaces/1.0.0-alpha3-10165
Packing nupkg dependency Microsoft.AspNet.HttpFeature 1.0.0-alpha3-10154
  Target /root/AgoraSolution/Agora.Api/bin/output/approot/packages/Microsoft.AspNet.HttpFeature/1.0.0-alpha3-10154
Packing nupkg dependency Nowin 0.11.0.0
  Target /root/AgoraSolution/Agora.Api/bin/output/approot/packages/Nowin/0.11.0.0
Packing nupkg dependency Microsoft.AspNet.Security 1.0.0-alpha3-10250
  Target /root/AgoraSolution/Agora.Api/bin/output/approot/packages/Microsoft.AspNet.Security/1.0.0-alpha3-10250
Packing nupkg dependency Microsoft.Framework.OptionsModel 1.0.0-alpha3-10122
  Target /root/AgoraSolution/Agora.Api/bin/output/approot/packages/Microsoft.Framework.OptionsModel/1.0.0-alpha3-10122
Packing nupkg dependency Newtonsoft.Json 5.0.8
  Target /root/AgoraSolution/Agora.Api/bin/output/approot/packages/Newtonsoft.Json/5.0.8
Packing nupkg dependency Microsoft.DataAnnotations 1.0.0-alpha3-10131
  Target /root/AgoraSolution/Agora.Api/bin/output/approot/packages/Microsoft.DataAnnotations/1.0.0-alpha3-10131
Packing nupkg dependency K.Roslyn 1.0.0-alpha3-10037
  Target /root/AgoraSolution/Agora.Api/bin/output/approot/packages/K.Roslyn/1.0.0-alpha3-10037
Packing nupkg dependency System.IO.FileSystem 4.0.10.0
  Target /root/AgoraSolution/Agora.Api/bin/output/approot/packages/System.IO.FileSystem/4.0.10.0
Packing nupkg dependency System.Threading.Thread 4.0.0.0
  Target /root/AgoraSolution/Agora.Api/bin/output/approot/packages/System.Threading.Thread/4.0.0.0
Packing nupkg dependency Microsoft.CodeAnalysis.Common 0.7.4060502-beta
  Target /root/AgoraSolution/Agora.Api/bin/output/approot/packages/Microsoft.CodeAnalysis.Common/0.7.4060502-beta
Packing nupkg dependency Microsoft.CodeAnalysis.CSharp 0.7.4060502-beta
  Target /root/AgoraSolution/Agora.Api/bin/output/approot/packages/Microsoft.CodeAnalysis.CSharp/0.7.4060502-beta
Packing nupkg dependency Microsoft.Bcl.Immutable 1.1.20-beta
  Target /root/AgoraSolution/Agora.Api/bin/output/approot/packages/Microsoft.Bcl.Immutable/1.1.20-beta
Packing nupkg dependency Microsoft.Bcl.Metadata 1.0.11-alpha
  Target /root/AgoraSolution/Agora.Api/bin/output/approot/packages/Microsoft.Bcl.Metadata/1.0.11-alpha
Packing project dependency Agora.Api
  Source /root/AgoraSolution/Agora.Api
  Target /root/AgoraSolution/Agora.Api/bin/output/approot/src/Agora.Api
Path is too long. Path: /root/AgoraSolution/Agora.Api/bin/output/approot/src/Agora.Api/bin/output/approot/src/Agora.Api/bin/output/approot/src/Agora.Api/bin/output/approot/src/Agora.Api/bin/output/approot/src/Agora.Api/bin/output/approot/src/Agora.Api/bin/output/approot/src/Agora.Api/bin/output/approot/src/Agora.Api/bin/output/approot/src/Agora.Api/bin/output/approot/src/Agora.Api/bin/output/approot/src/Agora.Api/bin/output/approot/src/Agora.Api/bin/output/approot/src/Agora.Api/bin/output/approot/src/Agora.Api/bin/output/approot/src/Agora.Api/bin/output/approot/src/Agora.Api/bin/output/approot/src/Agora.Api/bin/output/approot/src/Agora.Api/bin/output/approot/src/Agora.Api/bin/output/approot/src/Agora.Api/bin/output/approot/src/Agora.Api/bin/output/approot/src/Agora.Api/bin/output/approot/src/Agora.Api/bin/output/approot/src/Agora.Api/bin/output/approot/src/Agora.Api/bin/output/approot/src/Agora.Api/bin/output/approot/src/Agora.Api/bin/output/approot/src/Agora.Api/bin/output/approot/src/Agora.Api/bin/output/approot/src/Agora.Api/bin/output/approot/src/Agora.Api/bin/output/approot/src/Agora.Api/bin/output/approot/src/Agora.Api/bin/output/approot/src/Agora.Api/bin/output/approot/src/Agora.Api/bin/output/approot/src/Agora.Api/bin/output/approot/src/Agora.Api/bin/output/approot/src/Agora.Api/bin/output/approot/src/Agora.Api/bin/output/approot/src/Agora.Api/bin/output/approot/src/Agora.Api/bin/output/approot/src/Agora.Api/bin/output/approot/src/Agora.Api/bin/output/approot/src/Agora.Api/bin/output/approot/src/Agora.Api/bin/output/approot/src/Agora.Api/bin/output/approot/src/Agora.Api/bin/output/approot/src/Agora.Api/bin/output/approot/src/Agora.Api/bin/output/approot/src/Agora.Api/bin/output/approot/src/Agora.Api/bin/output/approot/src/Agora.Api/bin/output/approot/src/Agora.Api/bin/output/approot/src/Agora.Api/bin/output/approot/src/Agora.Api/bin/output/approot/src/Agora.Api/bin/output/approot/src/Agora.Api/bin/output/approot/src/Agora.Api/bin/output/approot/src/Agora.Api/bin/output/approot/src/Agora.Api/bin/output/approot/src/Agora.Api/bin/output/approot/src/Agora.Api/bin/output/approot/src/Agora.Api/bin/output/approot/src/Agora.Api/bin/output/approot/src/Agora.Api/bin/output/approot/src/Agora.Api/bin/output/approot/src/Agora.Api/bin/output/approot/src/Agora.Api/bin/output/approot/src/Agora.Api/bin/output/approot/src/Agora.Api/bin/output/approot/src/Agora.Api/bin/output/approot/src/Agora.Api/bin/output/approot/src/Agora.Api/bin/output/approot/src/Agora.Api/bin/output/approot/src/Agora.Api/bin/output/approot/src/Agora.Api/bin/output/approot/src/Agora.Api/bin/output/approot/src/Agora.Api/bin/output/approot/src/Agora.Api/bin/output/approot/src/Agora.Api/bin/output/approot/src/Agora.Api/bin/output/approot/src/Agora.Api/bin/output/approot/src/Agora.Api/bin/output/approot/src/Agora.Api/bin/output/approot/src/Agora.Api/bin/output/approot/src/Agora.Api/bin/output/approot/src/Agora.Api/bin/output/approot/src/Agora.Api/bin/output/approot/src/Agora.Api/bin/output/approot/src/Agora.Api/bin/output/approot/src/Agora.Api/bin/output/approot/src/Agora.Api/bin/output/approot/src/Agora.Api/bin/output/approot/src/Agora.Api/bin/output/approot/src/Agora.Api/bin/output/approot/src/Agora.Api/bin/output/approot/src/Agora.Api/bin/output/approot/src/Agora.Api/bin/output/approot/src/Agora.Api/bin/output/approot/src/Agora.Api/bin/output/approot/src/Agora.Api/bin/output/approot/src/Agora.Api/bin/output/approot/src/Agora.Api/bin/output/approot/src/Agora.Api/bin/output/approot/src/Agora.Api/bin/output/approot/src/Agora.Api/bin/output/approot/src/Agora.Api/bin/output/approot/src/Agora.Api/bin/output/approot/src/Agora.Api/bin/output/approot/src/Agora.Api/bin/output/approot/src/Agora.Api/bin/output/approot/src/Agora.Api/bin/output/approot/src/Agora.Api/bin/output/approot/src/Agora.Api/bin/output/approot/src/Agora.Api/bin/output/approot/src/Agora.Api/bin/output/approot/src/Agora.Api/bin/output/approot/src/Agora.Api/bin/output/approot/src/Agora.Api/bin/output/approot/src/Agora.Api/bin/output/approot/src/Agora.Api/bin/output/approot/src/Agora.Api/bin/output/approot/src/Agora.Api
```
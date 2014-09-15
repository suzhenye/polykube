FROM            prozachj/docker-mono-aspnetvnext
MAINTAINER      Cole Mickens <cole.mickens@gmail.com>

ADD             . /root/polykube/

RUN             bash -c "source /root/.kre/kvm/kvm.sh && cd /root/polykube/src && kpm restore"

WORKDIR         /root/polykube/src/Polykube.vNextApi/
CMD             /root/polykube/src/Polykube.vNextApi/start_k_daemon.sh

EXPOSE 8000

# TODO(colemick):
# 1. Use a different base image, or ask prozachj to remove the sample aspnet since it's cruft
# 2. Revist when kpm pack is a reality and we will emit docker images as a result of the build
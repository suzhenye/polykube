FROM            prozachj/docker-mono-aspnetvnext
MAINTAINER      Cole Mickens <cole.mickens@gmail.com>

RUN             git clone https://github.com/colemickens/Agora ~/Agora

ENTRYPOINT      /bin/bash -c "source ~/.kre/kvm/kvm.sh \
                    && cd ~/Agora/ \
                    && kpm restore \
                    && cd src/Agora.Api/ \
                    && ./k_daemon.sh web"

# TODO(colemick):
# 1. Use a different base image, or ask prozachj to remove the sample aspnet since it's cruft
# 2. Revist when kpm pack is a reality and we will emit docker images as a result of the build

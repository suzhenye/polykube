all: build
	
build:
  # I still don't think kpm looks in the right places for NuGet.Config
  #  (and is overly picky about casing on Linux)
  #  (TODO: file bug on aspvnext team)
  # This should work:
  # bash -c "source /root/.kre/kvm/kvm.sh;                     kpm --verbose restore"
  # Workaround:
	bash -c "source /root/.kre/kvm/kvm.sh; cd Polykube.vNextApi; kpm --verbose restore"

run: build
	bash -c "source /root/.kre/kvm/kvm.sh; cd Polykube.vNextApi; ./k_daemon.sh kestrel"
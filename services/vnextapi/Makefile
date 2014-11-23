all: build
	
build:
	bash -c "source /root/.kre/kvm/kvm.sh; kpm --verbose restore"

run: build
	bash -c "source /root/.kre/kvm/kvm.sh; cd src/Polykube.vNextApi; ./k_daemon.sh kestrel"

test:
	bash -c "source /root/.kre/kvm/kvm.sh; cd test/Polykube.vNextApiTest; k test"
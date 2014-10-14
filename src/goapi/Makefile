all: build

build:
	go get --ldflags '-extldflags "-static"' .

run: build
	../../bin/goapi
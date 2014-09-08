#!/bin/bash
# invoke with arguments to pass to k (e.g. ./k_daemon web)

trap 'my_exit; exit' SIGINT SIGQUIT

fifo=$PWD/fifo.tmp
timeout=15

# make a fifo and attach to &3
mkfifo $fifo
exec 3<> $fifo
rm -f $fifo

my_exit()
{
    echo -e "\nAsking KRuntime to exit..."

    # Write anything to fifo
    echo "quit" >&3

    result=$(wait_for_kruntime $timeout)

    if [ $result -ne 0 ]; then
        echo "WARN: KRuntime did not stop after waiting $timeout seconds"
    fi
}

wait_for_kruntime()
{
    local seconds=$1
    local count=0

    if [ -z "$seconds" ]; then
        seconds=-1
    fi;

    while ps -p $pid > /dev/null; do
        sleep 1;

        if [ $seconds -lt 0 ]; then
            continue;
        fi

        let count=count+1;
        if [ $count -gt $seconds ]; then
            echo 1
            return
        fi;
    done

    echo 0
}

# start KRuntime using fifo as stdin
k $@ <&3 &

# capture child pid
pid=$!

# detach child pid so signals don't propagate
disown

echo KRuntime pid=$pid
result=$(wait_for_kruntime)
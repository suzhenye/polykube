#!/bin/bash

SCRIPTDIR=`dirname $0`
cd $SCRIPTDIR

source /root/.kre/kvm/kvm.sh
kpm restore
./k_daemon.sh web

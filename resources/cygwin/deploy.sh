#!/bin/sh

function usage() {
    echo "Usage: deploy.sh <hostname>..."
    echo "e.g.:"
    echo "./deploy.sh app1 app2 backup files"
    exit 1
}

[ "$1" ] || usage

for host in "$@"
do
    ssh_host=Administrator@$host
    build=Debug

    scp linkme-conf.sh $ssh_host:
    ssh $ssh_host ./linkme-conf.sh --no-service

    if [ "$host" = "files" ]; then
        mkdir -p files
        cp ../../Solution/DataMining/MapReduce/bin/$build/LinkMe.DataMining.MapReduce.dll files
        cp ../../Solution/DataMining/MapReduce/app.config files
        for app in TupleDumper `cd ../../Solution/DataMining; echo app_*`; do
            cp ../../Solution/DataMining/$app/bin/$build/$app.exe files
        done
        rsync -auvz files/ $ssh_host:linkme/
    fi
done

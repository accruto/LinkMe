#!/bin/sh

case "$1" in
    --no-service)
        no_service=1
        ;;
esac

function install_service() {
    name=$1

    [ "$no_service" ] || cygrunsrv --query $name >/dev/null 2>&1 || cygrunsrv --install "$@"
    cygrunsrv --start $name
}

function setup_autossh_rsync() {
    port=$1
    host=$2
    rport=$3
    localuser=$4
    remoteuser=$5

    [ "$localuser" ] && localuser="--user $localuser"
    [ "$remoteuser" ] && remoteuser="${remoteuser}@"

    # For port XXXX, the monitoring port will be 1XXXX.
    install_service autossh-rsync-$port \
        --path /usr/bin/autossh \
        --args "-M 1$port -L 127.0.0.1:$port:localhost:$rport ${remoteuser}$host" \
        --env AUTOSSH_NTSERVICE=yes \
        --disp "Cygwin autossh ($port:$host:rsync)" \
        --desc "Autossh tunnel localhost:$port -> $host:rsync" \
        $localuser
}

function setup_rsyncd() {
    port=$1

    cat <<EOF > /etc/rsyncd.conf
hosts allow 127.0.0.1
read only = true

[var]
    path = /var

[linkme]
    path = /cygdrive/c/LinkMe

[system]
    path = /cygdrive/c/WINDOWS/system32

EOF
    install_service rsyncd \
        --path /usr/bin/rsync \
        --args "--daemon --no-detach --port=$port" \
        --disp "Cygwin rsync" \
        --desc "Efficient file transfer service" \
        --user Administrator
}

case "$1" in

# Gzip logs not modified in the last five days.
archive)
    dir=$2
    find -L $dir -type f -a -mtime +5 -a ! -name '*.gz' | xargs gzip -9v
    exit
    ;;

# Mirror logs between servers using rsync.
mirror)
    host=$2
    shift 2
    lockdir=/tmp/rsync.$host
    if mkdir $lockdir
    then
        trap "rm -rf $lockdir; exit" INT TERM EXIT
        rsync -avzL --no-perms "$@"
    fi
    exit
    ;;

# Delete logs not modified in the last 60 days.
purge)
    dir=$2
    find -L $dir -type f -a -mtime +60 | xargs rm -f
    exit
    ;;

setup_*)
    "$@"
    exit
    ;;

esac

case "$HOSTNAME" in

# Why, oh why, does app2 have an uppercase hostname?
app1|APP2)
    echo "Preparing app server cron jobs."

    setup_rsyncd 874

    crontab - <<EOF
3 3 * * * \$HOME/linkme-conf.sh archive /cygdrive/c/LinkMe/log
3 3 * * * \$HOME/linkme-conf.sh archive /cygdrive/c/WINDOWS/system32/LogFiles

# Comment out until observing rsync and gzip to run consistently and
# correctly for a while.
#8 3 * * * \$HOME/linkme-conf.sh purge /var/log
EOF
    ;;

backup)
    echo "Preparing 'backup' cron jobs."

    mkdir -p /var/backup/app1/var/log/linkme
    mkdir -p /var/backup/app1/var/log/system
    mkdir -p /var/backup/app2/var/log/linkme
    mkdir -p /var/backup/app2/var/log/system

    setup_rsyncd
    setup_autossh_rsync 8731 app1 874
    setup_autossh_rsync 8732 app2 874

    crontab - <<EOF
*/5 * * * * \$HOME/linkme-conf.sh mirror app1 rsync://localhost:8731/linkme/log/ /var/backup/app1/linkme/
*/5 * * * * \$HOME/linkme-conf.sh mirror app1 rsync://localhost:8731/system/LogFiles/ /var/backup/app1/system/
*/5 * * * * \$HOME/linkme-conf.sh mirror app2 rsync://localhost:8732/linkme/log/ /var/backup/app2/linkme/
*/5 * * * * \$HOME/linkme-conf.sh mirror app2 rsync://localhost:8732/system/LogFiles/ /var/backup/app2/system/

3 3 * * * \$HOME/linkme-conf.sh archive /var/backup
EOF
    ;;

files)
    echo "Preparing 'files' cron jobs."

    setup_autossh_rsync 8733 backup 873

    crontab - <<EOF
# Run every five minutes, a minute after backup's rsync job.
1-59/5 * * * * \$HOME/linkme-conf.sh mirror backup rsync://localhost:8731/var/backup/ /cygdrive/d/backup/

3 3 * * * \$HOME/linkme-conf.sh archive /var/backup
EOF
    ;;

esac

crontab -l

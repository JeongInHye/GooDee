#!/bin/sh
###############################################################
# Service > systemd > dotnet.service
# Sample Project > /root
# ip Change > 192.168.3."You linux ip"
# MariaDB > user(root)/password(test) > table(test.board)
###############################################################
gitPath=https://github.com/JeongInHye/Web.git
rootDir=/var/lib/jenkins/workspace/Test
projectDir=/Web
publishDir=/Web/bin/Debug/netcoreapp2.1/publish
serviceDir=/publish

if [ -d $rootDir$projectDir ]; then
	rm -rf $rootDir$projectDir
fi

# 1. GitHub Repository Download
git clone $gitPath
cd $rootDir$projectDir

# 2. Project Build
dotnet build
# 4. Project Publish
dotnet publish

# 3. Service Shutdown
systemctl stop dotnet.service


ln -s $rootDir$publishDir $rootDir$serviceDir

# 5. Service Run
systemctl start dotnet.service

exit 0

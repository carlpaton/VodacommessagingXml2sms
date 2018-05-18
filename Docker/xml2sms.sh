#!/bin/bash
#sudo docker pull microsoft/dotnet:2.0-sdk
sudo docker container kill xml2sms
sudo docker rm xml2sms

sudo docker rmi dotnet_image_xml2sms
sudo docker build -t dotnet_image_xml2sms .
sudo docker run -d -p 81:80 --name xml2sms dotnet_image_xml2sms

sudo docker start xml2sms
sudo docker ps --all
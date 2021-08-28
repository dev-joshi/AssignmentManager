@echo off
REM Commands to deploy Assignment Manager app

REM set working directory to bat file parent directory
cd /d %~dp0

REM Build Angular Home app
docker build --rm -t prachi-am-home:latest -f UI\HomeApp\DockerFile UI\

REM Build Angular Publisher app
docker build --rm -t prachi-am-publisher:latest -f UI\PublisherApp\DockerFile UI\

REM Build Angular Subscriber app
docker build --rm -t prachi-am-subscriber:latest -f UI\SubscriberApp\DockerFile UI\

REM Build nginx server for proxying angular apps
docker build --rm -t prachi-am-nginx:latest -f UI\nginx\DockerFile UI\

REM Spawn containers using docker compose file
docker-compose -f docker-compose.yml up
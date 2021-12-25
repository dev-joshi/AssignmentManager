@echo off
REM Commands to deploy Assignment Manager app

REM "C:\Program Files\Docker\Docker\DockerCli.exe" -SwitchDaemon

REM set working directory to bat file parent directory
cd /d %~dp0

REM setting Docker buildkit to not hide errors
SET DOCKER_BUILDKIT=0

REM Delete old database to start fresh
call :RunCommand "if exist DB\pgdata\ ( rmdir /s /q DB\pgdata )"

REM Build rest API docker container
call :RunCommand "docker build --rm -t prachi-am-auth:latest -f Auth\DockerFile ."

REM Build rest API docker container
call :RunCommand "docker build --rm -t prachi-am-api:latest -f API\DockerFile ."

REM Build nginx server container for proxying angular apps
call :RunCommand "docker build --rm -t prachi-am-nginx:latest -f UI\nginx\DockerFile UI\"

REM Build Angular Home app container
call :RunCommand "docker build --rm -t prachi-am-home:latest -f UI\HomeApp\DockerFile UI\"

REM Build Angular Publisher app container
call :RunCommand "docker build --rm -t prachi-am-publisher:latest -f UI\PublisherApp\DockerFile UI\"

REM Build Angular Subscriber app container
call :RunCommand "docker build --rm -t prachi-am-subscriber:latest -f UI\SubscriberApp\DockerFile UI\"

REM check and print config of docker compose file
call :RunCommand "docker-compose -f docker-compose.yml config"

REM Spawn containers using docker compose file
call :RunCommand "docker-compose -f docker-compose.yml up"

exit /b %ERRORLEVEL%

REM Runs a command with two attempts and stops batch in case of failure
:RunCommand
set "command=%~1"
set attempt=%~2

IF NOT DEFINED attempt set attempt=0

set /a attempt=%attempt%+1

echo.
echo.
echo running : "%command%" , attempt : %attempt%
%command%

IF %ERRORLEVEL% NEQ 0 (
	IF %attempt% LSS 2 (
		call :RunCommand "%command%", %attempt%
	) ELSE (
		echo.
        echo /!\/!\/!\/!\/!\/!\/!\/!\/!\/!\/!\/!\/!\/!\/!\/!\
        echo Error occured in deployment process... exiting !
        echo /!\/!\/!\/!\/!\/!\/!\/!\/!\/!\/!\/!\/!\/!\/!\/!\
        call :halt
	)
)

exit /b %ERRORLEVEL%

REM Sets the errorlevel and stops the batch immediately
:halt
call :__ErrorExit 2> nul
goto :eof

REM Creates a syntax error, stops immediately
:__ErrorExit
()
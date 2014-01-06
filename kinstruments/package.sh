#!/bin/bash

rm -rf dist

mkdir dist
cd dist

mkdir plugins
mkdir parts

cp -a ../www plugins/.

cp ../JsonFx* plugins/.
cp ../WebServer.dll plugins/.
cp ../xr-include.dll plugins/.
cp ../*struments*.dll plugins/.

cp -a ../../../Parts/* parts/.

mkdir Kinstruments
mv parts plugins Kinstruments

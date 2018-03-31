#!/usr/bin/env bash

NAME=$1
ROOT=${PWD}

if [ -z "$1" ]
then
    echo "Name required";
    exit;
fi

if [ -n "$2" ]
then
    ROOT="$2"
fi

# Delete old bundle
rm -rf ${ROOT}/build/${NAME}.app

# Create new bundle folders
mkdir -p ${ROOT}/build/${NAME}.app/Contents/MacOS ${ROOT}/build/${NAME}.app/Contents/Resources

# Copy assets
cp -r ${ROOT}/assets/ ${ROOT}/build/${NAME}.app/Contents/Resources/
cp ${ROOT}/Info.plist ${ROOT}/build/${NAME}.app/
cp ${ROOT}/cmake-build-debug/bin/${NAME} ${ROOT}/build/${NAME}.app/Contents/MacOS/

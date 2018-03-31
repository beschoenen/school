var express = require("express");
var mongoose = require('mongoose');

var User = mongoose.model('User'),
    Entry = mongoose.model('Entry'),
    Image = mongoose.model('Image'),
    Device = mongoose.model('Device'),
    AuthCode = mongoose.model('AuthCode');

///////////////////////////
// Custom Express responses

express.response.ok = function(data, error) {
    if(error) {
        return this.error(error);
    }

    if(!data) {
        return this.notFound("Object not found");
    }

    return this.json({ success: true, data: data.toJSON });
};

express.response.empty = function(error) {
    if(error) {
        return this.error(error);
    }

    return this.json({ success: true })
};

express.response.create = function(data, error) {
    return this.status(201).ok(data, error);
};

express.response.update = function(error) {
    return this.empty(error);
};

express.response.delete = function(error) {
    return this.empty(error);
};

express.response.unauthorized = function(error) {
    return this.status(401).error(error);
};

express.response.forbidden = function(error) {
    return this.status(403).error(error);
};

express.response.notFound = function(error) {
    return this.status(404).error(error);
};

express.response.error = function(error) {
    return this.json({ success: false, error: error });
};

///////////////////
// Array prototypes

Array.prototype.remove = function(value) {
    var index = this.findIndex(function(item) {
        return item === value
    });

    this.splice(index, 1);
};

Array.prototype.__defineGetter__("toJSON", function() {
    var newArray = [];
    this.forEach(function(item) {
        newArray.push(item.toJSON);
    });

    return newArray;
});

/////////////////
// Helper methods

module.exports = {
    makeDeviceCode: function() {
        var text = "";
        var possible = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        for(var i = 0; i < 10; i++) {
            text += possible.charAt(Math.floor(Math.random() * possible.length));
        }

        return text;
    }
};
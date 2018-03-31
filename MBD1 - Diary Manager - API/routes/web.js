var router = require('express').Router();
var mongoose = require('mongoose');
var request = require('request');

var helpers = require('../helpers');

var User = mongoose.model('User'),
    Entry = mongoose.model('Entry'),
    Image = mongoose.model('Image'),
    Device = mongoose.model('Device'),
    AuthCode = mongoose.model('AuthCode');

module.exports = function(app) {

    /***************************
     * Middleware
     ***************************/

    var webMiddleware = function(req, res, next) {
        Device.findOne({ code: req.cookies.Device }).populate('user').exec(function(error, result) {
            if(result === null) {
                res.redirect('/');
            }

            req.user = result.user;
            next();
        });
    };

    app.use('/overview', webMiddleware);
    app.use('/devices', webMiddleware);
    app.use('/admin', webMiddleware);
    app.use('/logout', webMiddleware);

    /***************************
     * Routes
     ***************************/

    router.get('/', function(req, res) {
        res.render('home', {
            message: req.query.message
        });
    });

    router.post('/', function(req, res) {
        var webcode = req.body.webcode;

        AuthCode.findOne({ code: webcode, isWeb: true }, function(error, doc) {
            if(doc === null) {
                var msg = "Not a valid code!";
                return res.redirect('/?message='+msg);
            }

            AuthCode.findByIdAndRemove(doc._id, function(error, del) {
                if(error || doc.validUntil < new Date()) {
                    var msg = "Not a valid code!";
                    return res.redirect('/?message='+msg);
                }

                User.findById({ _id: doc.user }, function(error, usr) {
                    var deviceCode = helpers.makeDeviceCode();

                    var dev = { code: deviceCode, name: "Browser", user: usr };

                    var nDevice = new Device(dev);

                    nDevice.save(function(error, device) {
                        if(error) {
                            return res.error(error);
                        }

                        usr.devices.push(device);
                        usr.save(function() {
                            res.cookie('Device', deviceCode);
                            return res.redirect('/overview');
                        });
                    });
                });
            });
        });
    });

    router.get('/overview', function(req, res) {
        Entry.find({ user: req.user._id }, function(error, docs) {
            res.render('overview', {
                entries: docs,
                name: req.user.fullName,
                isAdmin: req.user.isAdmin
            });
        });
    });

    router.get('/devices', function(req, res) {
        Device.find({ user: req.user._id }, function(error, docs) {
            res.render('devices', {
                devices: docs,
                name: req.user.fullName,
                loggeddevice: req.cookies.Device,
                isAdmin: req.user.isAdmin
            });
        });
    });

    router.get('/admin', function(req, res) {
        if(!req.user.isAdmin){
            res.redirect('/overview');
        }

        User.find({}, function(error, users){
            res.render('admin', {
                users: users,
                name: req.user.fullName,
                isAdmin: req.user.isAdmin
            });
        });
    });

    router.get('/logout', function(req, res) {
        if(req.user !== null) {
            res.clearCookie('Device');
        }

        return res.redirect('/');
    });

    return router;

};
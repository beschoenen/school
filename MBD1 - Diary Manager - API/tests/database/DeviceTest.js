var expect = require('chai').expect;
var should = require('chai').should();
var mongoose = require('mongoose');

var Device = mongoose.model('Device');
var User = mongoose.model('User');

describe("Model: Device", function() {
    it("Should create Device 1", function(done) {
        User.findOne({}, function(error, usr) {
            Device.create({
                code: "One",
                name: "TestDevice1",
                user: usr._id
            }, function(error, createdDevice) {
                if(!error) {
                    //Test if entry has been made
                    createdDevice.name.should.equal('TestDevice1');

                    //Ref the entry in the user collection
                    var entryObjectId = createdDevice._id;
                    var eUser = usr;
                    eUser.devices.push(entryObjectId);
                    eUser.save();

                    expect(eUser.devices).to.include.members([entryObjectId]);

                    done();
                }
            });
        });
    });

    it("Should create Device 2", function(done) {
        User.findOne({ firstName: "User" }, function(error, usr) {
            Device.create({
                code: "Two",
                name: "TestDevice2",
                user: usr._id
            }, function(error, createdDevice) {
                if(!error) {
                    //Test if entry has been made
                    createdDevice.name.should.equal('TestDevice2');

                    //Ref the entry in the user collection
                    var entryObjectId = createdDevice._id;
                    var eUser = usr;
                    eUser.devices.push(entryObjectId);
                    eUser.save();

                    expect(eUser.devices).to.include.members([entryObjectId]);

                    done();
                }
            });
        });
    });

    it("Should create Device 3", function(done) {
        User.findOne({}, function(error, usr) {
            Device.create({
                code: "Three",
                name: "TestDevice3",
                user: usr._id
            }, function(error, createdDevice) {
                if(!error) {
                    //Test if entry has been made
                    createdDevice.name.should.equal('TestDevice3');

                    //Ref the entry in the user collection
                    var entryObjectId = createdDevice._id;
                    var eUser = usr;
                    eUser.devices.push(entryObjectId);
                    eUser.save();

                    expect(eUser.devices).to.include.members([entryObjectId]);

                    done();
                }
            });
        });
    });

});
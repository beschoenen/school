var expect = require('chai').expect;
var should = require('chai').should();
var mongoose = require('mongoose');

var User = mongoose.model('User');
var Entry = mongoose.model('Entry');

describe("Model: Entry", function() {
    it("Should create diary entry 1", function(done) {
        User.findOne({}, function(error, usr) {
            Entry.create({
                user: usr._id,
                datetimeCreated: new Date(),
                title: "TestTitle",
                text: "Hello World!"
            }, function(error, createdEntry) {
                if(!error) {
                    //Test if entry has been made
                    createdEntry.title.should.equal('TestTitle');
                    createdEntry.text.should.equal('Hello World!');

                    //Ref the entry in the user collection
                    var entryObjectId = createdEntry._id;
                    var eUser = usr;
                    eUser.entries.push(entryObjectId);
                    eUser.save();

                    expect(eUser.entries).to.include.members([entryObjectId]);

                    done();
                }
            });
        });
    });

    it("Should create diary entry 2", function(done) {
        User.findOne({ firstName: "User" }, function(error, usr) {
            Entry.create({
                user: usr._id,
                datetimeCreated: new Date(),
                title: "Test Title 2",
                text: "Hello World! 2"
            }, function(error, createdEntry) {
                if(!error) {
                    //Test if entry has been made
                    createdEntry.title.should.equal('Test Title 2');
                    createdEntry.text.should.equal('Hello World! 2');

                    //Ref the entry in the user collection
                    var entryObjectId = createdEntry._id;
                    var eUser = usr;
                    eUser.entries.push(entryObjectId);
                    eUser.save();

                    expect(eUser.entries).to.include.members([entryObjectId]);

                    done();
                }
            });
        });
    });

    it("Should create diary entry 3", function(done) {
        User.findOne({}, function(error, usr) {
            Entry.create({
                user: usr._id,
                datetimeCreated: new Date(),
                title: "Test Title 3",
                text: "Hello World! 3"
            }, function(error, createdEntry) {
                if(!error) {
                    //Test if entry has been made
                    createdEntry.title.should.equal('Test Title 3');
                    createdEntry.text.should.equal('Hello World! 3');

                    //Ref the entry in the user collection
                    var entryObjectId = createdEntry._id;
                    var eUser = usr;
                    eUser.entries.push(entryObjectId);
                    eUser.save();

                    expect(eUser.entries).to.include.members([entryObjectId]);

                    done();
                }
            });
        });
    });
});
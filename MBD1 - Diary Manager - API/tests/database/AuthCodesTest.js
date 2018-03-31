var expect = require('chai').expect;
var should = require('chai').should();
var mongoose = require('mongoose');

var AuthCode = mongoose.model('AuthCode');
var User = mongoose.model('User');

describe("Model: AuthCode", function() {
    it("Should create authCode 1", function(done) {
        User.findOne({}, function(error, usr) {
            AuthCode.create({
                code: "One",
                validUntil: new Date(),
                user: usr._id
            }, function(error, createdAuthCode) {
                if(!error) {
                    //Test if entry has been made
                    createdAuthCode.code.should.equal('One');

                    //Ref the entry in the user collection
                    var entryObjectId = createdAuthCode._id;
                    var eUser = usr;
                    eUser.authCodes.push(entryObjectId);
                    eUser.save();

                    expect(eUser.authCodes).to.include.members([entryObjectId]);

                    done();
                }
            });
        });
    });

    it("Should create authCode 2", function(done) {
        User.findOne({}, function(error, usr) {
            AuthCode.create({
                code: "Two",
                validUntil: new Date(),
                user: usr._id
            }, function(error, createdAuthCode) {
                if(!error) {
                    //Test if entry has been made
                    createdAuthCode.code.should.equal('Two');

                    //Ref the entry in the user collection
                    var entryObjectId = createdAuthCode._id;
                    var eUser = usr;
                    eUser.authCodes.push(entryObjectId);
                    eUser.save();

                    expect(eUser.authCodes).to.include.members([entryObjectId]);

                    done();
                }
            });
        });
    });
});
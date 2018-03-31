var should = require('chai').should();
var mongoose = require('mongoose');

var User = mongoose.model('User');

describe("Model: User", function() {
    it('Should create user 1', function(done) {
        User.create({ firstName: "Test", lastName: "User" }, function(error, createdUser) {
            // verify that the returned user is what we expect
            if(!error) {
                createdUser.firstName.should.equal('Test');
                createdUser.lastName.should.equal('User');

                done();
            }
        });
    });

    it('Should create user 2 (admin)', function(done) {
        User.create({ firstName: "User", lastName: "Test", isAdmin: true }, function(error, createdUser) {
            // verify that the returned user is what we expect
            if(!error) {
                createdUser.firstName.should.equal('User');
                createdUser.lastName.should.equal('Test');
                createdUser.isAdmin.should.equal(true);

                done();
            }
        });
    });
});
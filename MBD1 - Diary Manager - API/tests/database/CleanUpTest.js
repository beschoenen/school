var mongoose = require('mongoose');

var AuthCode = mongoose.model('AuthCode');
var Device = mongoose.model('Device');
var User = mongoose.model('User');
var Entry = mongoose.model('Entry');

describe("Cleanup", function() {
    it("Should delete all AuthCodes", function(done) {
        AuthCode.remove(done);
    });
    it("Should delete all devices", function(done) {
        Device.remove(done);
    });
    it("Should delete all diary entries", function(done) {
        Entry.remove(done);
    });
    it("Should delete all users", function(done) {
        User.remove(done);
    });
});

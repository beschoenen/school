var request = require('request');
var assert = require('assert');
var http = require('http');
var mongoose = require('mongoose');

var AuthCode = mongoose.model('AuthCode');

function DoRequest(method, url, body, callback) {
    var options = {
        method: method,
        url: url,
        headers: {
            'X-DEVICE-TOKEN': 'Two',
            'Content-type': 'application/json'
        },
        body: body
    };

    request(options, function(error, response, body) {
        callback(JSON.parse(body));
    });
}

describe("AuthCode no access requests", function() {
    it('POST - an AuthCode', function(done) {
        request.post('http://localhost:3000/api/codes', function(error, response, body) {
            assert.equal(401, response.statusCode);

            var obj = JSON.parse(body);
            obj.should.be.a('object');
            obj.should.have.property('success').eql(false);

            done();
        });
    });
});

describe("AuthCode Good requests", function() {
    it('POST - an AuthCode', function(done) {
        var url = 'http://localhost:3000/api/codes';
        var newCode = {
            "code": "TestCode",
            "validUntil" : new Date(new Date().getTime() + 2*60000),
            "isWeb": false
        };

        DoRequest('POST', url, JSON.stringify(newCode), function(obj) {
            obj.should.have.property('success').eql(true);

            done();
        });
    });
});

describe("AuthCode Bad requests", function() {
    it('POST - an AuthCode', function(done) {
        var url = 'http://localhost:3000/api/codes';

        DoRequest('POST', url, null, function(obj) {
            obj.should.have.property('success').eql(false);

            done();
        });
    });
});
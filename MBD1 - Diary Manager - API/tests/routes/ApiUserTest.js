var request = require('request');
var assert = require('assert');
var http = require('http');
var mongoose = require('mongoose');

var User = mongoose.model('User');

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

describe("User no access requests", function() {
    it('GET - all users', function(done) {
        request.get('http://localhost:3000/api/users', function(error, response, body) {
            assert.equal(401, response.statusCode);

            var obj = JSON.parse(body);
            obj.should.be.a('object');
            obj.should.have.property('success').eql(false);

            done();
        });
    });

    it('POST - a user', function(done) {
        request.post('http://localhost:3000/api/users', function(error, response, body) {
            assert.equal(200, response.statusCode);

            var obj = JSON.parse(body);
            obj.should.be.a('object');
            obj.should.have.property('success').eql(false);

            done();
        });
    });

    it('GET - user with id 1', function(done) {
        request.get('http://localhost:3000/api/users/1', function(error, response, body) {
            assert.equal(401, response.statusCode);

            var obj = JSON.parse(body);
            obj.should.be.a('object');
            obj.should.have.property('success').eql(false);

            done();
        });
    });

    it('PUT - user with id 1', function(done) {
        request.put('http://localhost:3000/api/users/1', function(error, response, body) {
            assert.equal(401, response.statusCode);

            var obj = JSON.parse(body);
            obj.should.be.a('object');
            obj.should.have.property('success').eql(false);

            done();
        });
    });

    it('Delete - user with id 1', function(done) {
        request.delete('http://localhost:3000/api/users/1', function(error, response, body) {
            assert.equal(401, response.statusCode);

            var obj = JSON.parse(body);
            obj.should.be.a('object');
            obj.should.have.property('success').eql(false);

            done();
        });
    });
});

describe("User Good requests", function() {
    it('GET - all users', function(done) {
        var url = 'http://localhost:3000/api/users';

        DoRequest('GET', url, null, function(obj) {
            obj.should.have.property('success').eql(true);
            obj.should.have.property('data');
            obj.data.should.be.a('array');

            done();
        });
    });

    it('POST - a user', function(done) {
        var url = 'http://localhost:3000/api/users';
        var newUser = {
            "user": {
                "firstName": "UnitTest",
                "lastName": "UnitUser"
            },
            "device": {
                "code": "UnitTest",
                "name": "UnitTestCode"
            }
        };

        DoRequest('POST', url, JSON.stringify(newUser), function(obj) {
            obj.should.have.property('success').eql(true);
            obj.should.have.property('data');

            done();
        });
    });

    it('GET - admin user', function(done) {
        User.findOne({ firstName: "User" }, function(error, usr) {
            var url = 'http://localhost:3000/api/users/' + usr._id;

            DoRequest('GET', url, null, function(obj) {
                obj.should.have.property('success').eql(true);
                obj.should.have.property('data');

                done();
            });
        });
    });

    it('PUT - user', function(done) {
        User.findOne({ firstName: "Test" }, function(error, usr) {
            var url = 'http://localhost:3000/api/users/' + usr._id;

            usr.firstName = "TestPUT";

            DoRequest('PUT', url, JSON.stringify(usr), function(obj) {
                obj.should.have.property('success').eql(true);

                done();
            });
        });
    });

    it('Delete - a user', function(done) {
        User.findOne({ firstName: "UnitTest" }, function(error, usr) {
            var url = 'http://localhost:3000/api/users/' + usr._id;

            DoRequest('DELETE', url, null, function(obj) {
                obj.should.have.property('success').eql(true);

                done();
            });
        });
    });
});

describe("User Bad requests", function() {
    it('GET - all users', function(done) {
        var url = 'http://localhost:3000/api/users';

        DoRequest('GET', url, null, function(obj) {
            obj.should.have.property('success').eql(true);
            obj.should.have.property('data');
            obj.data.should.be.a('array');

            done();
        });
    });

    it('POST - a user', function(done) {
        var url = 'http://localhost:3000/api/users';

        DoRequest('POST', url, null, function(obj) {
            obj.should.have.property('success').eql(false);

            done();
        });
    });

    it('GET - admin user', function(done) {
        var url = 'http://localhost:3000/api/users/1';

        DoRequest('GET', url, null, function(obj) {
            obj.should.have.property('success').eql(false);

            done();
        });
    });

    it('PUT - user', function(done) {
        var url = 'http://localhost:3000/api/users/1';

        DoRequest('PUT', url, null, function(obj) {
            obj.should.have.property('success').eql(false);

            done();
        });
    });

    it('Delete - a user', function(done) {
        var url = 'http://localhost:3000/api/users/1';

        DoRequest('DELETE', url, null, function(obj) {
            obj.should.have.property('success').eql(false);

            done();
        });
    });
});
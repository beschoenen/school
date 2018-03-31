var request = require('request');
var assert = require('assert');
var http = require('http');
var mongoose = require('mongoose');

var Device = mongoose.model('Device'),
    AuthCode = mongoose.model('AuthCode');

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

describe("Device no access requests", function() {
    it('GET - all devices', function(done) {
        request.get('http://localhost:3000/api/devices', function(error, response, body) {
            assert.equal(401, response.statusCode);

            var obj = JSON.parse(body);
            obj.should.be.a('object');
            obj.should.have.property('success').eql(false);

            done();
        });
    });

    it('POST - a device', function(done) {
        request.post('http://localhost:3000/api/devices', function(error, response, body) {
            assert.equal(404, response.statusCode);

            var obj = JSON.parse(body);
            obj.should.be.a('object');
            obj.should.have.property('success').eql(false);

            done();
        });
    });

    it('GET - device with id 1', function(done) {
        request.get('http://localhost:3000/api/devices/1', function(error, response, body) {
            assert.equal(401, response.statusCode);

            var obj = JSON.parse(body);
            obj.should.be.a('object');
            obj.should.have.property('success').eql(false);

            done();
        });
    });

    it('Delete - device with id 1', function(done) {
        request.delete('http://localhost:3000/api/devices/1', function(error, response, body) {
            assert.equal(401, response.statusCode);

            var obj = JSON.parse(body);
            obj.should.be.a('object');
            obj.should.have.property('success').eql(false);

            done();
        });
    });
});

describe("Device Good requests", function() {
    it('GET - all devices', function(done) {
        var url = 'http://localhost:3000/api/devices';

        DoRequest('GET', url, null, function(obj) {
            obj.should.have.property('success').eql(true);
            obj.should.have.property('data');
            obj.data.should.be.a('array');

            done();
        });
    });

    it('POST - a device', function(done) {
        var url = 'http://localhost:3000/api/devices';
        var newDevice = {
            "code": "TestCode",
            "device": {
                "code": "UnitTestDeviceCode",
                "name":"UnitTestDevice"
            }
        };

        DoRequest('POST', url, JSON.stringify(newDevice), function(obj) {
            obj.should.have.property('success').eql(true);

            done();
        });
    });

    it('GET - device with id 1', function(done) {
        Device.findOne({ name: "UnitTestDevice" }, function(error, ent) {
            var url = 'http://localhost:3000/api/devices/' + ent._id;

            DoRequest('GET', url, null, function(obj) {
                obj.should.have.property('success').eql(true);
                obj.should.have.property('data');

                done();
            });
        });
    });

    it('Delete - device with id 1', function(done) {
        Device.findOne({ name: "UnitTestDevice" }, function(error, ent) {
            var url = 'http://localhost:3000/api/devices/' + ent._id;

            DoRequest('DELETE', url, null, function(obj) {
                obj.should.have.property('success').eql(true);

                done();
            });
        });
    });
});

describe("Device Bad requests", function() {
    it('GET - all devices', function(done) {
        var url = 'http://localhost:3000/api/devices';

        DoRequest('GET', url, null, function(obj) {
            obj.should.have.property('success').eql(true);
            obj.should.have.property('data');
            obj.data.should.be.a('array');

            done();
        });
    });

    it('POST - a device', function(done) {
        var url = 'http://localhost:3000/api/devices';

        DoRequest('POST', url, null, function(obj) {
            obj.should.have.property('success').eql(false);

            done();
        });
    });

    it('GET - device with id 1', function(done) {
        var url = 'http://localhost:3000/api/devices/1';

        DoRequest('GET', url, null, function(obj) {
            obj.should.have.property('success').eql(false);

            done();
        });
    });

    it('Delete - device with id 1', function(done) {
        var url = 'http://localhost:3000/api/devices/1';

        DoRequest('DELETE', url, null, function(obj) {
            obj.should.have.property('success').eql(false);

            done();
        });
    });
});
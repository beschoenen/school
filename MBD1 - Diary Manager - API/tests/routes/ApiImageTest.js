var request = require('request');
var assert = require('assert');
var http = require('http');
var mongoose = require('mongoose');

var Image = mongoose.model('Image'),
    Entry = mongoose.model('Entry');

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

describe("Images no access requests", function() {
    it('GET - a image with id 1', function(done) {
        request.get('http://localhost:3000/api/images/1', function(error, response, body) {
            assert.equal(401, response.statusCode);

            var obj = JSON.parse(body);
            obj.should.be.a('object');
            obj.should.have.property('success').eql(false);

            done();
        });
    });

    it('GET - a thumb of an image', function(done) {
        request.get('http://localhost:3000/api/images/1/thumb', function(error, response, body) {
            assert.equal(401, response.statusCode);

            var obj = JSON.parse(body);
            obj.should.be.a('object');
            obj.should.have.property('success').eql(false);

            done();
        });
    });

    it('PUT - image name with id 1', function(done) {
        request.put('http://localhost:3000/api/images/1', function(error, response, body) {
            assert.equal(401, response.statusCode);

            var obj = JSON.parse(body);
            obj.should.be.a('object');
            obj.should.have.property('success').eql(false);

            done();
        });
    });

    it('Delete - iamge with id 1', function(done) {
        request.delete('http://localhost:3000/api/images/1', function(error, response, body) {
            assert.equal(401, response.statusCode);

            var obj = JSON.parse(body);
            obj.should.be.a('object');
            obj.should.have.property('success').eql(false);

            done();
        });
    });
});

describe("Images Bad requests", function() {
    it('GET - a image', function(done) {
        var url = 'http://localhost:3000/api/images/1';

        DoRequest('GET', url, null, function(obj) {
            obj.should.have.property('success').eql(false);

            done();
        });
    });

    it('GET - a thumb of an image', function(done) {
        var url = 'http://localhost:3000/api/images/1/thumb';

        DoRequest('GET', url, null, function(obj) {
            obj.should.have.property('success').eql(false);

            done();
        });
    });

    it('PUT - image name with id 1', function(done) {
        var url = 'http://localhost:3000/api/images/1';

        DoRequest('PUT', url, null, function(obj) {
            obj.should.have.property('success').eql(false);

            done();
        });
    });

    it('Delete - iamge with id 1', function(done) {
        var url = 'http://localhost:3000/api/images/1';

        DoRequest('DELETE', url, null, function(obj) {
            obj.should.have.property('success').eql(false);

            done();
        });
    });
});
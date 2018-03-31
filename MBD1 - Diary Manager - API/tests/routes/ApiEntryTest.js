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

describe("Entry no access requests", function() {
    it('GET - all entries', function(done) {
        request.get('http://localhost:3000/api/entries', function(error, response, body) {
            assert.equal(401, response.statusCode);

            var obj = JSON.parse(body);
            obj.should.be.a('object');
            obj.should.have.property('success').eql(false);

            done();
        });
    });

    it('POST - a entry', function(done) {
        request.post('http://localhost:3000/api/entries', function(error, response, body) {
            assert.equal(401, response.statusCode);

            var obj = JSON.parse(body);
            obj.should.be.a('object');
            obj.should.have.property('success').eql(false);

            done();
        });
    });

    it('GET - entry with id 1', function(done) {
        request.get('http://localhost:3000/api/entries/1', function(error, response, body) {
            assert.equal(401, response.statusCode);

            var obj = JSON.parse(body);
            obj.should.be.a('object');
            obj.should.have.property('success').eql(false);

            done();
        });
    });

    it('PUT - entry with id 1', function(done) {
        request.put('http://localhost:3000/api/entries/1', function(error, response, body) {
            assert.equal(401, response.statusCode);

            var obj = JSON.parse(body);
            obj.should.be.a('object');
            obj.should.have.property('success').eql(false);

            done();
        });
    });

    it('Delete - entry with id 1', function(done) {
        request.delete('http://localhost:3000/api/entries/1', function(error, response, body) {
            assert.equal(401, response.statusCode);

            var obj = JSON.parse(body);
            obj.should.be.a('object');
            obj.should.have.property('success').eql(false);

            done();
        });
    });

    it('GET - all images of entry with id 1', function(done) {
        request.get('http://localhost:3000/api/entries/1/images', function(error, response, body) {
            assert.equal(401, response.statusCode);

            var obj = JSON.parse(body);
            obj.should.be.a('object');
            obj.should.have.property('success').eql(false);

            done();
        });
    });

    it('POST - an image for an entry with id 1', function(done) {
        request.put('http://localhost:3000/api/entries/1/images', function(error, response, body) {
            assert.equal(401, response.statusCode);

            var obj = JSON.parse(body);
            obj.should.be.a('object');
            obj.should.have.property('success').eql(false);

            done();
        });
    });
});

describe("Entry Good requests", function() {
    it('GET - all entries', function(done) {
        var url = 'http://localhost:3000/api/entries';

        DoRequest('GET', url, null, function(obj) {
            obj.should.have.property('success').eql(true);
            obj.should.have.property('data');
            obj.data.should.be.a('array');

            done();
        });
    });

    it('POST - a entry', function(done) {
        var url = 'http://localhost:3000/api/entries';
        var newEntry = {
            "title": "UnitTestTitle",
            "text" : "This is an UnitTest Entry",
            "datetimeCreated": new Date()
        };

        DoRequest('POST', url, JSON.stringify(newEntry), function(obj) {
            obj.should.have.property('success').eql(true);
            obj.should.have.property('data');

            done();
        });
    });

    it('GET - entry', function(done) {
        Entry.findOne({ title: "UnitTestTitle" }, function(error, ent) {
            var url = 'http://localhost:3000/api/entries/' + ent._id;

            DoRequest('GET', url, null, function(obj) {
                obj.should.have.property('success').eql(true);
                obj.should.have.property('data');

                done();
            });
        });
    });

    it('PUT - entry', function(done) {
        Entry.findOne({ title: "UnitTestTitle" }, function(error, ent) {
            var url = 'http://localhost:3000/api/entries/' + ent._id;

            ent.text = "Changed";

            DoRequest('PUT', url, JSON.stringify(ent), function(obj) {
                obj.should.have.property('success').eql(true);

                done();
            });
        });
    });

    it('Delete - entry', function(done) {
        Entry.findOne({ title: "UnitTestTitle" }, function(error, ent) {
            var url = 'http://localhost:3000/api/entries/' + ent._id;

            DoRequest('DELETE', url, null, function(obj) {
                obj.should.have.property('success').eql(true);

                done();
            });
        });
    });

    it('GET - all images of entry', function(done) {
        Entry.findOne({ title: "Test Title 2" }, function(error, ent) {
            var url = 'http://localhost:3000/api/entries/'+ent._id + "/images";

            DoRequest('GET', url, null, function(obj) {
                obj.should.have.property('success').eql(true);
                obj.should.have.property('data');
                obj.data.should.be.a('array');

                done();
            });
        });
    });
});

describe("Entry Bad requests", function() {
    it('GET - all entries', function(done) {
        var url = 'http://localhost:3000/api/entries';

        DoRequest('GET', url, null, function(obj) {
            obj.should.have.property('success').eql(true);
            obj.should.have.property('data');
            obj.data.should.be.a('array');

            done();
        });
    });

    it('POST - a entry', function(done) {
        var url = 'http://localhost:3000/api/entries';

        DoRequest('POST', url, null, function(obj) {
            obj.should.have.property('success').eql(false);

            done();
        });
    });

    it('GET - entry with id 1', function(done) {
        var url = 'http://localhost:3000/api/entries/1';

        DoRequest('GET', url, null, function(obj) {
            obj.should.have.property('success').eql(false);

            done();
        });
    });

    it('PUT - entry with id 1', function(done) {
        var url = 'http://localhost:3000/api/entries/1';

        DoRequest('PUT', url, null, function(obj) {
            obj.should.have.property('success').eql(false);

            done();
        });
    });

    it('Delete - entry with id 1', function(done) {
        var url = 'http://localhost:3000/api/entries/1';

        DoRequest('DELETE', url, null, function(obj) {
            obj.should.have.property('success').eql(false);

            done();
        });
    });

    it('GET - all images of entry', function(done) {
        var url = 'http://localhost:3000/api/entries/1/images';

        DoRequest('GET', url, null, function(obj) {
            obj.should.have.property('success').eql(false);

            done();
        });
    });
});
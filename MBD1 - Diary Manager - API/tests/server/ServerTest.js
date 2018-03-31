var assert = require('assert');
var http = require('http');

describe("Server", function() {
    it('Check for homepage', function(done) {
        http.get('http://localhost:3000', function(res) {
            assert.equal(200, res.statusCode);
            done();
        });
    });
});
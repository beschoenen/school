var app = require("../index.js");

describe("Database testing", function () {
    require('./database/UserTest.js');
    require('./database/EntryTest.js');
    require('./database/DeviceTest.js');
    require('./database/AuthCodesTest.js');
});

describe("Server testing", function () {
    require('./server/ServerTest.js');
});

describe("API Routes testing: User", function () {
    require('./routes/ApiUserTest.js');
});

describe("API Routes testing: Entry", function () {
    require('./routes/ApiEntryTest.js');
});

describe("API Routes testing: AuthCode", function () {
    require('./routes/ApiAuthCodeTest.js');
});

describe("API Routes testing: Device", function () {
    require('./routes/ApiDeviceTest.js');
});

describe("API Routes testing: Image", function () {
    require('./routes/ApiImageTest.js');
});

describe("CleanUp test", function () {
    require('./database/CleanUpTest.js');
});
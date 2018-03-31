var express = require('express');
var bodyParser = require('body-parser');
var cors = require('cors');
var mongoose = require('mongoose');
var cookieParser = require('cookie-parser');
var swaggerJSDoc = require('swagger-jsdoc');
var path = require('path');
var config = require('config');

var app = express();

// initialize swagger-jsdoc
var swaggerSpec = swaggerJSDoc({
    swaggerDefinition: {
        info: {
            title: 'Diary Manager API documentation',
            version: '1.0.0',
            description: 'Diary Manager API documentation'
        },
        consumes: [
            'application/json'
        ],
        host: 'krichter.nl:3000',
        basePath: '/api/',
        securityDefinitions: {
            deviceToken: {
                type: 'apiKey',
                name: 'X-Device-Token',
                in: 'header'
            }
        }
    },
    apis: [
        './routes/api/*.js',
        './models/*.js'
    ],
    spec: '/swagger.json'
});

// serve swagger
app.get('/swagger.json', function(req, res) {
    res.setHeader('Content-Type', 'application/json');
    res.send(swaggerSpec);
});

// Database connection
var dbconnect = config.get('dbConfig');
mongoose.connect(dbconnect);

// View engine
app.set('view engine', 'pug');
app.set('views', './views');

// BodyParser
app.use(bodyParser.json());
app.use(bodyParser.urlencoded({ extended: true })); // for parsing application/x-www-form-urlencoded

// Static folder
app.use('/assets', express.static('assets'));
app.use(express.static(path.join(__dirname, 'assets')));

app.use(cookieParser());
app.use(cors());

// Models
require('./models/_index');

// Routes
require('./routes/_index')(app);

// Start
app.listen(3000);
console.log('Listening...');
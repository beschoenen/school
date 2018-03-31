var mongoose = require('mongoose');

var User = mongoose.model('User'),
    Device = mongoose.model('Device'),
    Entry = mongoose.model('Entry'),
    Image = mongoose.model('Image'),
    AuthCode = mongoose.model('AuthCode');

/**
 * @swagger
 * parameters:
 *   DeviceHeader:
 *     name: X-Device-Token
 *     description: "Device Token for authentication"
 *     type: string
 *     in: header
 *     required: true
 *   SearchQuery:
 *     name: search
 *     description: "Query array to filter a list: name,Kevin"
 *     type: string
 *     in: query
 *     required: false
 *   PageQuery:
 *     name: page
 *     description: "The page to display"
 *     type: int
 *     in: query
 *     required: false
 *     default: 1
 *   LimitQuery:
 *     name: limit
 *     description: "The number of items to display"
 *     type: int
 *     in: query
 *     required: false
 *     default: 20
 *
 * definitions:
 *   ErrorResponse:
 *     properties:
 *       success:
 *         type: boolean
 *       error:
 *         type: object
 *   EmptyResponse:
 *     properties:
 *       success:
 *         type: boolean
 */
module.exports = function(app) {
    /**
     * Get the requesting user.
     */
    app.use('/api', function(req, res, next) {
        // Ignore the register route
        if(['/users', '/devices'].indexOf(req.url) > -1 && req.method === 'POST') {
            next();
            return;
        }

        Device.findOne({ code: req.header('X-DEVICE-TOKEN') }).populate('user').exec(function(error, result) {
            if(!result) {
                return res.unauthorized('Invalid device token');
            }

            req.requestingUser = result.user;
            return next();
        });
    });

    /**
     * Set the user that's requested.
     */
    app.use('/api/users/:id', function(req, res, next) {
        if(req.params.id === "me") {
            req.user = req.requestingUser;
            return next();
        }

        if(!req.requestingUser.isAdmin) {
            return res.forbidden('You are not an admin!');
        }

        User.findOne({ _id: req.params.id }, function(error, result) {
            if(!result) {
                return res.notFound('User not found');
            }

            req.user = result;
            return next();
        });
    });

    /**
     * Auto load the requested entry.
     */
    app.use('/api/entries/:id', function(req, res, next) {
        if(req.requestingUser.entries.indexOf(req.params.id) === -1) {
            return res.notFound('Entry not found');
        }

        Entry.findById(req.params.id, function(error, result) {
            if(!result) {
                return res.notFound('Entry not found');
            }

            req.entry = result;
            return next();
        });
    });

    /**
     * Auto load the requested device.
     */
    app.use('/api/devices/:id', function(req, res, next) {
        if(req.requestingUser.devices.indexOf(req.params.id) === -1) {
            return res.notFound('Device not found');
        }

        Device.findById(req.params.id, function(error, result) {
            if(!result) {
                return res.notFound('Device not found');
            }

            req.device = result;
            return next();
        });
    });

    /**
     * Auto load the requested image.
     */
    app.use('/api/images/:id', function(req, res, next) {
        Image.findById(req.params.id, function(error, result) {
            if(!result) {
                return res.notFound('Image not found');
            }

            req.image = result;
            return next();
        });
    });

    /**
     * Handle search and pagination query parameters.
     */
    app.use('/api', function(req, res, next) {
        req.pagination = {
            limit: parseInt(req.query.limit || 20),
            page: parseInt(req.query.page || 1)
        };
        req.pagination.skip = (req.pagination.page * req.pagination.limit) - req.pagination.limit;

        req.search = {};

        var search = req.query.search || [];
        if(typeof req.query.search === "string") {
            search = [req.query.search];
        }

        search.forEach(function(item) {
            item = item.split(',');
            req.search[item[0]] = { '$regex': item[1] };
        });

        return next();
    });

    app.use('/api/users', require('./users'));
    app.use('/api/codes', require('./codes'));
    app.use('/api/devices', require('./devices'));
    app.use('/api/entries', require('./entries'));
    app.use('/api/images', require('./images'));
};
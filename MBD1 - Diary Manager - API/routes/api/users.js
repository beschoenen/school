var router = require('express').Router();
var mongoose = require('mongoose');

var User = mongoose.model('User'),
    Device = mongoose.model('Device'),
    Entry = mongoose.model('Entry');

/**
 * @swagger
 * users:
 *   get:
 *     tags:
 *       - User
 *     description: Get all users
 *     produces:
 *       - application/json
 *     security:
 *       - deviceToken
 *     parameters:
 *       - $ref: '#/parameters/DeviceHeader'
 *       - $ref: '#/parameters/SearchQuery'
 *       - $ref: '#/parameters/PageQuery'
 *       - $ref: '#/parameters/LimitQuery'
 *     responses:
 *       200:
 *         description: JSON Success Response
 *         schema:
 *           properties:
 *             success:
 *               type: boolean
 *             data:
 *               type: array
 *               items:
 *                 $ref: '#/definitions/User'
 *       401:
 *         description: JSON Unauthorized Response
 *         schema:
 *           $ref: '#/definitions/ErrorResponse'
 *       403:
 *         description: JSON Forbidden Response
 *         schema:
 *           $ref: '#/definitions/ErrorResponse'
 *       404:
 *         description: JSON Error Response
 *         schema:
 *           $ref: '#/definitions/ErrorResponse'
 */
router.get('/', function(req, res) {
    if(!req.requestingUser.isAdmin) {
        return res.forbidden('You are not an admin!');
    }

    User.find(req.search)
    .limit(req.pagination.limit).skip(req.pagination.skip)
    .exec(function(error, result) {
        return res.ok(result, error);
    });
});

/**
 * @swagger
 * users:
 *   post:
 *     tags:
 *       - User
 *     description: Create a user (register)
 *     produces:
 *       - application/json
 *     parameters:
 *       - name: body
 *         in: body
 *         type: object
 *         required: true
 *         schema:
 *           properties:
 *             user:
 *               type: object
 *               properties:
 *                 firstName:
 *                   type: string
 *                 lastName:
 *                   type: string
 *             device:
 *               type: object
 *               properties:
 *                 code:
 *                   type: string
 *                 name:
 *                   type: string
 *     responses:
 *       201:
 *         description: JSON, A single user
 *         schema:
 *           properties:
 *             success:
 *               type: boolean
 *             data:
 *               $ref: '#/definitions/Device'
 */
router.post('/', function(req, res) {
    var nUser = new User(req.body.user);
    var nDevice = new Device(req.body.device);

    nUser.devices.push(nDevice);
    nUser.save(function(error, result) {
        if(error) {
            return res.error(error);
        }

        nDevice.user = nUser._id;
        nDevice.save(function(error) {
            return res.create(result, error);
        });
    });
});

/**
 * @swagger
 * users/{id}:
 *   get:
 *     tags:
 *       - User
 *     description: Get a user
 *     produces:
 *       - application/json
 *     security:
 *       - deviceToken
 *     parameters:
 *       - $ref: '#/parameters/DeviceHeader'
 *       - name: id
 *         in: path
 *         description: User id
 *         type: string
 *         required: true
 *     responses:
 *       200:
 *         description: JSON Success Response
 *         schema:
 *           properties:
 *             success:
 *               type: boolean
 *             data:
 *               $ref: '#/definitions/User'
 *       401:
 *         description: JSON Unauthorized Response
 *         schema:
 *           $ref: '#/definitions/ErrorResponse'
 *       403:
 *         description: JSON Forbidden Response
 *         schema:
 *           $ref: '#/definitions/ErrorResponse'
 *       404:
 *         description: JSON Error Response
 *         schema:
 *           $ref: '#/definitions/ErrorResponse'
 */
router.get('/:id', function(req, res) {
    return res.ok(req.user);
});

/**
 * @swagger
 * users/{id}:
 *   put:
 *     tags:
 *       - User
 *     description: Updates a single user
 *     produces:
 *       - application/json
 *     security:
 *       - deviceToken
 *     parameters:
 *       - $ref: '#/parameters/DeviceHeader'
 *       - name: id
 *         in: path
 *         description: User id
 *         type: string
 *         required: true
 *       - name: body
 *         in: body
 *         type: object
 *         schema:
 *           properties:
 *             firstName:
 *               type: string
 *             lastName:
 *               type: string
 *     responses:
 *       200:
 *         description: JSON Success Response
 *         schema:
 *           $ref: '#/definitions/EmptyResponse'
 *       401:
 *         description: JSON Unauthorized Response
 *         schema:
 *           $ref: '#/definitions/ErrorResponse'
 *       403:
 *         description: JSON Forbidden Response
 *         schema:
 *           $ref: '#/definitions/ErrorResponse'
 *       404:
 *         description: JSON Error Response
 *         schema:
 *           $ref: '#/definitions/ErrorResponse'
 */
router.put('/:id', function(req, res) {
    User.findByIdAndUpdate(req.user._id, { $set: req.body }, function(error) {
        return res.update(error);
    });
});

/**
 * @swagger
 * users/{id}:
 *   delete:
 *     tags:
 *       - User
 *     description: Delete a user
 *     produces:
 *       - application/json
 *     security:
 *       - deviceToken
 *     parameters:
 *       - $ref: '#/parameters/DeviceHeader'
 *       - name: id
 *         in: path
 *         description: User id
 *         type: string
 *         required: true
 *     responses:
 *       200:
 *         description: JSON Success Response
 *         schema:
 *           $ref: '#/definitions/EmptyResponse'
 *       401:
 *         description: JSON Unauthorized Response
 *         schema:
 *           $ref: '#/definitions/ErrorResponse'
 *       403:
 *         description: JSON Forbidden Response
 *         schema:
 *           $ref: '#/definitions/ErrorResponse'
 *       404:
 *         description: JSON Error Response
 *         schema:
 *           $ref: '#/definitions/ErrorResponse'
 */
router.delete('/:id', function(req, res) {
    User.findByIdAndRemove(req.user._id, function(error) {
        return res.empty(error);
    });
});

module.exports = router;
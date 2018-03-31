var router = require('express').Router();
var mongoose = require('mongoose');

var Device = mongoose.model('Device'),
    AuthCode = mongoose.model('AuthCode');

/**
 * @swagger
 * devices:
 *   get:
 *     tags:
 *       - Device
 *     description: Get all devices
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
 *                 $ref: '#/definitions/Device'
 *       401:
 *         description: JSON Unauthorized Response
 *         schema:
 *           $ref: '#/definitions/ErrorResponse'
 *       404:
 *         description: JSON Error Response
 *         schema:
 *           $ref: '#/definitions/ErrorResponse'
 */
router.get('/', function(req, res) {
    req.search['_id'] = { $in: req.requestingUser.devices };
    Device.find(req.search)
    .limit(req.pagination.limit).skip(req.pagination.skip)
    .exec(function(error, result) {
        return res.ok(result, error);
    });
});

/**
 * @swagger
 * devices:
 *   post:
 *     tags:
 *       - Device
 *     description: Create a device (using an auth code)
 *     produces:
 *       - application/json
 *     parameters:
 *       - name: body
 *         description: Device object and transfer code
 *         in: body
 *         type: object
 *         required: true
 *         schema:
 *           properties:
 *             code:
 *               type: string
 *             device:
 *               properties:
 *                 code:
 *                   type: string
 *                 name:
 *                   type: string
 *     responses:
 *       201:
 *         description: JSON Success Response
 *         schema:
 *           properties:
 *             success:
 *               type: boolean
 *             data:
 *               $ref: '#/definitions/Device'
 *       401:
 *         description: JSON Unauthorized Response
 *         schema:
 *           $ref: '#/definitions/ErrorResponse'
 *       404:
 *         description: JSON Error Response
 *         schema:
 *           $ref: '#/definitions/ErrorResponse'
 */
router.post('/', function(req, res) {
    AuthCode.findOne({ code: req.body.code }).populate('user').exec(function(error, code) {
        if(!code) {
            return res.notFound('Code not found');
        }

        if(code.validUntil < new Date()) {
            return res.unauthorized('This code has expired!');
        }

        var nDevice = new Device(req.body.device);

        nDevice.user = code.user._id;
        nDevice.save(function(error, device) {
            if(error) {
                return res.error(error);
            }

            code.user.devices.push(nDevice);
            code.user.save(function(error) {
                return res.create(device, error);
            });
        });

    });
});

/**
 * @swagger
 * devices/{id}:
 *   get:
 *     tags:
 *       - Device
 *     description: Get a device
 *     produces:
 *       - application/json
 *     security:
 *       - deviceToken
 *     parameters:
 *       - $ref: '#/parameters/DeviceHeader'
 *       - name: id
 *         in: path
 *         description: Device id
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
 *               $ref: '#/definitions/Device'
 *       401:
 *         description: JSON Unauthorized Response
 *         schema:
 *           $ref: '#/definitions/ErrorResponse'
 *       404:
 *         description: JSON Error Response
 *         schema:
 *           $ref: '#/definitions/ErrorResponse'
 */
router.get('/:id', function(req, res) {
    return res.ok(req.device);
});

/**
 * @swagger
 * devices/{id}:
 *   delete:
 *     tags:
 *       - Device
 *     description: Delete a device
 *     produces:
 *       - application/json
 *     security:
 *       - deviceToken
 *     parameters:
 *       - $ref: '#/parameters/DeviceHeader'
 *       - name: id
 *         in: path
 *         description: Device id
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
 *       404:
 *         description: JSON Error Response
 *         schema:
 *           $ref: '#/definitions/ErrorResponse'
 */
router.delete('/:id', function(req, res) {
    Device.findByIdAndRemove(req.device._id, function(error) {
        if(error) {
            return res.error(error);
        }

        req.requestingUser.devices.remove(req.device._id);
        req.requestingUser.save(function(error) {
            return res.empty(error);
        });
    });
});

module.exports = router;
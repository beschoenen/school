var router = require('express').Router();
var mongoose = require('mongoose');

var AuthCode = mongoose.model('AuthCode');

/**
 * @swagger
 * codes:
 *   post:
 *     tags:
 *       - AuthCode
 *     description: Create auth code for the user
 *     produces:
 *       - application/json
 *     security:
 *       - deviceToken
 *     parameters:
 *       - $ref: '#/parameters/DeviceHeader'
 *       - name: body
 *         description: AuthCode object
 *         in: body
 *         type: string
 *         required: true
 *         schema:
 *           properties:
 *             code:
 *               type: string
 *             validUntil:
 *               type: string
 *               format: date-time
 *             isWeb:
 *               type: boolean
 *               default: false
 *     responses:
 *       201:
 *         description: JSON Success Response
 *         schema:
 *           properties:
 *             success:
 *               type: boolean
 *             data:
 *               type: array
 *               items:
 *                 $ref: '#/definitions/AuthCode'
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
    var nAuthCode = new AuthCode(req.body);

    nAuthCode.user = req.requestingUser._id;
    nAuthCode.save(function(error) {
        return res.empty(error);
    });
});

module.exports = router;
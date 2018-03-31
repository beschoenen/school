var router = require('express').Router();
var mongoose = require('mongoose');
var fs = require('fs');
var jimp = require('jimp');
var path = require('path');

var Image = mongoose.model('Image'),
    Entry = mongoose.model('Entry');

/**
 * @swagger
 * images/{id}:
 *   get:
 *     tags:
 *       - Image
 *     description: Get an image
 *     produces:
 *       - application/json
 *     security:
 *       - deviceToken
 *     parameters:
 *       - $ref: '#/parameters/DeviceHeader'
 *       - name: id
 *         in: path
 *         description: Image id
 *         type: string
 *         required: true
 *     responses:
 *       200:
 *         description: Image file
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
    res.setHeader('Content-Type', req.image.mimeType);
    return res.sendFile(path.resolve('./uploads/' + req.image.path));
});

/**
 * @swagger
 * images/{id}/thumb:
 *   get:
 *     tags:
 *       - Image
 *     description: Get a thumb of an image
 *     produces:
 *       - application/json
 *     security:
 *       - deviceToken
 *     parameters:
 *       - $ref: '#/parameters/DeviceHeader'
 *       - name: id
 *         in: path
 *         description: Image id
 *         type: string
 *         required: true
 *     responses:
 *       200:
 *         description: Image thumb file
 *       401:
 *         description: JSON Unauthorized Response
 *         schema:
 *           $ref: '#/definitions/ErrorResponse'
 *       404:
 *         description: JSON Error Response
 *         schema:
 *           $ref: '#/definitions/ErrorResponse'
 */
router.get('/:id/thumb', function(req, res) {
    res.setHeader('Content-Type', req.image.mimeType);

    var imagepath = path.resolve('./uploads/' + req.image.path);
    var thumbPath = path.resolve('./uploads/thumbs/' + req.image.path);

    if(fs.existsSync(thumbPath)) {
        return res.sendFile(thumbPath);
    }

    jimp.read(imagepath, function(error, image) {
        if(error) {
            return res.error(error);
        }

        image.resize(jimp.AUTO, 64).quality(60).write(thumbPath, function() {
            return res.sendFile(thumbPath);
        });
    });
});

/**
 * @swagger
 * images/{id}:
 *   put:
 *     tags:
 *       - Image
 *     description: Update an image's name
 *     produces:
 *       - application/json
 *     security:
 *       - deviceToken
 *     parameters:
 *       - $ref: '#/parameters/DeviceHeader'
 *       - name: id
 *         in: path
 *         description: Image id
 *         type: string
 *         required: true
 *       - name: body
 *         description: The image's name
 *         in: body
 *         type: object
 *         required: true
 *         schema:
 *           properties:
 *             name:
 *               type: string
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
router.put('/:id', function(req, res) {
    Image.findByIdAndUpdate(req.image._id, { $set: req.body }, function(error) {
        return res.update(error);
    });
});

/**
 * @swagger
 * images/{id}:
 *   delete:
 *     tags:
 *       - Image
 *     description: Delete an image
 *     produces:
 *       - application/json
 *     security:
 *       - deviceToken
 *     parameters:
 *       - $ref: '#/parameters/DeviceHeader'
 *       - name: id
 *         in: path
 *         description: Image id
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
    Image.findByIdAndRemove(req.image._id, function(error) {
        if(error) {
            return res.error(error);
        }

        Entry.findById(req.image.entry, function(error, entry) {
            if(error) {
                return res.error(error);
            }

            entry.images.remove(req.image._id);
            entry.save(function(error) {
                return res.delete(error);
            });
        });
    });
});

module.exports = router;
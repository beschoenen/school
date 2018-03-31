var router = require('express').Router();
var mongoose = require('mongoose');
var multer = require('multer');
var fs = require('fs');
var path = require('path');
var mime = require('mime');

var upload = multer({ dest: './uploads/' });
var helpers = require('../../helpers');

var Image = mongoose.model('Image'),
    Entry = mongoose.model('Entry');

/**
 * @swagger
 * entries:
 *   get:
 *     tags:
 *       - Entry
 *     description: Get all entries
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
 *              type: array
 *              items:
 *                 $ref: '#/definitions/Entry'
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
    req.search['_id'] = { $in: req.requestingUser.entries };
    Entry.find(req.search)
    .limit(req.pagination.limit).skip(req.pagination.skip)
    .exec(function(error, result) {
        return res.ok(result, error);
    });
});

/**
 * @swagger
 * entries:
 *   post:
 *     tags:
 *       - Entry
 *     description: Create an entry for a user
 *     produces:
 *       - application/json
 *     security:
 *       - deviceToken
 *     parameters:
 *       - $ref: '#/parameters/DeviceHeader'
 *       - name: body
 *         description: Entry object
 *         in: body
 *         type: object
 *         required: true
 *         schema:
 *           properties:
 *             title:
 *               type: string
 *             text:
 *               type: string
 *             datetimeCreated:
 *               type: string
 *               format: YYYY-mm-dd H:i:s
 *     responses:
 *       201:
 *         description: JSON Success Response
 *         schema:
 *           properties:
 *             success:
 *               type: boolean
 *             data:
 *               $ref: '#/definitions/Entry'
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
    var nEntry = new Entry(req.body);

    nEntry.user = req.requestingUser._id;
    nEntry.save(function(error, result) {
        if(error) {
            return res.error(error);
        }

        req.requestingUser.entries.push(nEntry);
        req.requestingUser.save(function(error) {
            return res.create(result, error);
        });
    });
});

/**
 * @swagger
 * entries/{id}:
 *   get:
 *     tags:
 *       - Entry
 *     description: Get a entry
 *     produces:
 *       - application/json
 *     security:
 *       - deviceToken
 *     parameters:
 *       - $ref: '#/parameters/DeviceHeader'
 *       - name: id
 *         in: path
 *         description: Entry id
 *         type: string
 *         required: true
 *     responses:
 *       200:
 *         description: JSON Success Response
 *         schema:
 *           $ref: '#/definitions/Entry'
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
    return res.ok(req.entry);
});

/**
 * @swagger
 * entries/{id}:
 *   put:
 *     tags:
 *       - Entry
 *     description: Update an entry
 *     produces:
 *       - application/json
 *     security:
 *       - deviceToken
 *     parameters:
 *       - $ref: '#/parameters/DeviceHeader'
 *       - name: body
 *         description: Entry object
 *         in: body
 *         type: object
 *         required: true
 *         schema:
 *           properties:
 *             title:
 *               type: string
 *             text:
 *               type: string
 *             datetimeCreated:
 *               type: string
 *               format: YYYY-mm-dd H:i:s
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
router.put('/:id', function(req, res) {
    Entry.findByIdAndUpdate(req.entry._id, { $set: req.body }, function(error) {
        return res.update(error);
    });
});

/**
 * @swagger
 * entries/{id}:
 *   delete:
 *     tags:
 *       - Entry
 *     description: Delete an entry
 *     produces:
 *       - application/json
 *     security:
 *       - deviceToken
 *     parameters:
 *       - $ref: '#/parameters/DeviceHeader'
 *       - name: id
 *         in: path
 *         description: Entry id
 *         type: string
 *         required: true
 *     responses:
 *       200:
 *         description: JSON Success Response
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
    Entry.findByIdAndRemove(req.entry._id, function(error) {
        if(error) {
            return res.error(error);
        }

        req.requestingUser.entries.remove(req.params.id);
        req.requestingUser.save(function(error) {
            return res.delete(error);
        });
    });
});

/**
 * @swagger
 * entries/{id}/images:
 *   get:
 *     tags:
 *       - Entry
 *     description: Get all the images of an entry
 *     produces:
 *       - application/json
 *     security:
 *       - deviceToken
 *     parameters:
 *       - $ref: '#/parameters/DeviceHeader'
 *       - name: id
 *         in: path
 *         description: Entry id
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
 *              type: array
 *              items:
 *                 $ref: '#/definitions/Image'
 *       401:
 *         description: JSON Unauthorized Response
 *         schema:
 *           $ref: '#/definitions/ErrorResponse'
 *       404:
 *         description: JSON Error Response
 *         schema:
 *           $ref: '#/definitions/ErrorResponse'
 */
router.get('/:id/images', function(req, res) {
    req.search['entry'] = req.entry._id;
    Image.find(req.search)
    .limit(req.pagination.limit).skip(req.pagination.skip)
    .exec(function(error, images) {
        return res.ok(images, error);
    });
});

/**
 * @swagger
 * entries/{id}/images:
 *   post:
 *     tags:
 *       - Entry
 *     description: Get all the images of an entry
 *     produces:
 *       - application/json
 *     security:
 *       - deviceToken
 *     parameters:
 *       - $ref: '#/parameters/DeviceHeader'
 *       - name: id
 *         in: path
 *         description: Entry id
 *         type: string
 *         required: true
 *       - name: images
 *         in: formData
 *         type: file
 *         required: true
 *     responses:
 *       201:
 *         description: JSON Success Response
 *         schema:
 *           properties:
 *             success:
 *               type: boolean
 *             data:
 *               $ref: '#/definitions/Image'
 *       401:
 *         description: JSON Unauthorized Response
 *         schema:
 *           $ref: '#/definitions/ErrorResponse'
 *       404:
 *         description: JSON Error Response
 *         schema:
 *           $ref: '#/definitions/ErrorResponse'
 */
router.post('/:id/images', upload.single('image'), function(req, res) {
    var image = new Image({
        name: req.file.originalname,
        path: req.file.filename + '.' + mime.extension(req.file.mimetype),
        mimeType: req.file.mimetype
    });

    var imagepath = path.resolve('./uploads/' + req.file.filename);
    if(fs.existsSync(imagepath)) {
        fs.renameSync(imagepath, imagepath + '.' + mime.extension(req.file.mimetype));
    }

    image.entry = req.entry._id;
    image.save(function(error, image) {
        if(error) {
            return res.error(error);
        }

        req.entry.images.push(image._id);
        req.entry.save(function(error) {
            return res.create(image, error);
        });
    });
});

module.exports = router;
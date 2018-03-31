var mongoose = require('mongoose');

console.log('Initializing image schema');

/**
 * @swagger
 * definitions:
 *   Image:
 *     properties:
 *       id:
 *         type: string
 *       name:
 *         type: string
 */
var imageSchema = mongoose.Schema({
    name: { type: String },
    path: { type: String, required: true },
    mimeType: { type: String, required: true },

    // Relationships
    entry: { type: mongoose.Schema.Types.ObjectId, ref: 'Entry' }
});

imageSchema.virtual('toJSON').get(function() {
    return {
        id: this._id,
        name: this.name
    }
});

module.exports = mongoose.model('Image', imageSchema);

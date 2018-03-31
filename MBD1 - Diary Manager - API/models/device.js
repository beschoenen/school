var mongoose = require('mongoose');

console.log('Initializing device schema');

/**
 * @swagger
 * definitions:
 *   Device:
 *     properties:
 *       id:
 *         type: string
 *       code:
 *         type: string
 *       name:
 *         type: string
 */
var deviceSchema = mongoose.Schema({
    code: { type: String, required: true, index: { unique: true } },
    name: { type: String, required: true },

    // Relationships
    user: { type: mongoose.Schema.Types.ObjectId, ref: 'User' }
});

deviceSchema.virtual('toJSON').get(function() {
    return {
        id: this._id,
        code: this.code,
        name: this.name
    }
});

module.exports = mongoose.model('Device', deviceSchema);
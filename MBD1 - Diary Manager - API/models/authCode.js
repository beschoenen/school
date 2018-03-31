var mongoose = require('mongoose');

console.log('Initializing authCode schema');

/**
 * @swagger
 * definitions:
 *   AuthCode:
 *     properties:
 *       id:
 *         type: string
 *       validUntil:
 *         type: string
 *         format: YYYY-mm-dd H:i:s
 *       isWeb:
 *         type: boolean
 */

var authCodeSchema = mongoose.Schema({
    code: { type: String, required: true, index: { unique: true } },
    validUntil: { type: Date, required: true },
    isWeb: { type: Boolean, default: false },

    // Relationships
    user: { type: mongoose.Schema.Types.ObjectId, ref: 'User' }
});

authCodeSchema.virtual('toJSON').get(function() {
    return {
        id: this._id,
        code: this.code,
        validUntil: this.name
    }
});

module.exports = mongoose.model('AuthCode', authCodeSchema);
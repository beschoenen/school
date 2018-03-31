var mongoose = require('mongoose');

console.log('Initializing user schema');

/**
 * @swagger
 * definitions:
 *   User:
 *     properties:
 *       id:
 *         type: string
 *       firstName:
 *         type: string
 *       lastName:
 *         type: string
 */
var userSchema = mongoose.Schema({
    firstName: { type: String, required: true },
    lastName: { type: String, required: true },
    isAdmin: { type: Boolean, default: false },

    // Relationships
    entries: [{ type: mongoose.Schema.Types.ObjectId, ref: 'Entry' }],
    devices: [{ type: mongoose.Schema.Types.ObjectId, ref: 'Device' }],
    authCodes: [{ type: mongoose.Schema.Types.ObjectId, ref: 'AuthCode' }]
});

userSchema.virtual('fullName').get(function() {
    return this.firstName + ' ' + this.lastName;
});

userSchema.virtual('toJSON').get(function() {
    return {
        id: this._id,
        firstName: this.firstName,
        lastName: this.lastName
    }
});

module.exports = mongoose.model('User', userSchema);
var mongoose = require('mongoose');

console.log('Initializing entry schema');

/**
 * @swagger
 * definitions:
 *   Entry:
 *     properties:
 *       id:
 *         type: string
 *       title:
 *         type: string
 *       text:
 *         type: string
 *       datetimeCreated:
 *         type: string
 *         format: YYYY-mm-dd H:i:s
 */
var entrySchema = mongoose.Schema({
    title: { type: String, required: true },
    text: { type: String },
    datetimeCreated: { type: Date, required: true },

    // Relationships
    user: { type: mongoose.Schema.Types.ObjectId, ref: 'User' },
    images: [{ type: mongoose.Schema.Types.ObjectId, ref: 'Image' }]
});

entrySchema.virtual('formatDate').get(function() {
    var monthNames = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];
    var date = new Date(this.datetimeCreated);

    var d = date.getDate();
    var m = monthNames[date.getMonth()];
    var y = date.getFullYear();

    return d + ' ' + m + ' ' + y;
});

entrySchema.virtual('toJSON').get(function() {
    return {
        id: this._id,
        title: this.title,
        text: this.text,
        datetimeCreated: this.datetimeCreated
    }
});

module.exports = mongoose.model('Entry', entrySchema);
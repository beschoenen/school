/**
 * @param x string
 * @param y number
 * @param length number
 * @param vertical [bool=false]
 * @returns {Ship}
 */
var Ship = function(x, y, length, vertical) {
	vertical = vertical || false;

	this.location = [];

	// Init
	(function() {
		for(var i = 0; i < length; i++) {
			this.location.push({
				hit: false
			});
			// TODO add location
		}
	}.bind(this))();

	this.getApiObject = function() {
		return {
			
		}
	};

	return this;
};
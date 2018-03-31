var Board = function(parent) {

	var fields = [];

	var columns = ['A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J'];
	var rows = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];

	columns.forEach(function(column) {
		rows.forEach(function(row) {
			fields[column + row] = new Tile(column, row);
		});
	});

	return {
		fields: fields
	}
};
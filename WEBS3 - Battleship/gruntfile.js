module.exports = function(grunt) {

	// Project configuration.
	grunt.initConfig({
		pkg: grunt.file.readJSON('package.json'),

		concat: {
			options: {
				separator: '\n\n'
			},
			build: {
				src: ['app/classes/*.js', 'app/screens/*.js', 'app/*.js'],
				dest: 'index.js'
			}
		},

		watch: {
			build: {
				files: [
					'app/**/*.js'
				],
				tasks: ['concat']
			}
		}
	});

	// Load the plugin that provides the "uglify" task.
	grunt.loadNpmTasks('grunt-contrib-concat');
	grunt.loadNpmTasks('grunt-contrib-watch');
	grunt.loadNpmTasks('grunt-contrib-template');

	// Default task(s).
	grunt.registerTask('default', ['concat', 'watch']);

};
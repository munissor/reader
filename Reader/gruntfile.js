module.exports = function (grunt) {

	grunt.initConfig({
		pkg: grunt.file.readJSON('package.json'),
		jshint: {
			options: {
				browser: true,
				multistr: true,
				globals: {
					jQuery: true
				}
			},
			all: ['Scripts/app/**/*.js']
		},
		karma: {
			unit: {
				configFile: 'karma.conf.js'
			}
		},
		watch: {
			options: {
					livereload: true
			},
			jshint: {
					files: ['Scripts/app_tests/**/*.js','Scripts/app/**/*.js'],
					tasks: ['jshint:all']
			},
			karma: {
				files: ['Scripts/app_tests/**/*.js', 'Scripts/app/**/*.js'],
				tasks: ['karma']
			}
		}
	});

	grunt.loadNpmTasks('grunt-contrib-watch');
	grunt.loadNpmTasks('grunt-contrib-jshint');
	grunt.loadNpmTasks('grunt-karma');

	// Default task(s).
	grunt.registerTask('default', ['jshint:all', 'karma', 'watch']);
};
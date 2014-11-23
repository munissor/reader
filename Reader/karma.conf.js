module.exports = function(config) {
    config.set({
        basePath: 'Scripts',

        frameworks: ['jasmine'],

        files: [
            // Application Dependencies 
            'jquery-2.1.1.js',
            'angular.js',
            'angular-mocks.js',

            // mocks
            'app_mocks/**/*.js',

            // Application
            'app/**/*.js',

            // Specs
            'app_tests/**/*.js',

            //Directives
            'app/views/directives/*.html'
        ],

        plugins: [
            'karma-jasmine',
            'karma-coverage',
            'karma-phantomjs-launcher',
            'karma-ng-html2js-preprocessor'
        ],

        ngHtml2JsPreprocessor: {
             cacheIdFromPath: function(filepath) {
                //console.log("FILEID " + 'Scripts/' + filepath);
                return 'Scripts/' + filepath;
            },
        },

        exclude: [],

        // test results reporter to use
        // possible values: 'dots', 'progress', 'junit', 'growl', 'coverage'
        reporters: ['progress', 'coverage'],

        // include files for code coverage
        preprocessors: {
            '**/*.html': 'ng-html2js',
            'app/**/*.js': ['coverage']
        },

        // optionally, configure the reporter
        coverageReporter: {
            reporters: [
                { type: 'html', dir: 'coverage/' },
                { type: 'text-summary', file: 'summary.txt', dir: 'coverage/' }
            ],
        },

        // web server port
        port: 6582,

        // enable / disable colors in the output (reporters and logs)
        colors: true,

        // level of logging
        // possible values: config.LOG_DISABLE || config.LOG_ERROR || config.LOG_WARN || config.LOG_INFO || config.LOG_DEBUG
        logLevel: config.LOG_INFO,

        // enable and disable watching file and executing tests whenever any file changes
        autoWatch: false,

        // Start these browsers, currently available:
        // - Chrome
        // - ChromeCanary
        // - Firefox
        // - Opera
        // - Safari (only Mac)
        // - PhantomJS
        // - IE (only Windows)
        browsers: ['PhantomJS'],

        // If browser does not capture in given timeout [ms], kill it
        captureTimeout: 60000,

        // Continuous Integration mode
        // if true, it capture browsers, run tests and exit
        singleRun: true
    });
};
/// <binding BeforeBuild='clean, sass, cssmin, uglify' Clean='clean' />
module.exports = function (grunt) {
    grunt.initConfig({
        // a task to know which files are edited 
        watch: ["wwwroot/scss/*", "wwwroot/ts/*"],

        // delete generated files
        clean: ["wwwroot/css/*", "pages/*.css", "shared/*.css", "wwwroot/js/*.js", "!wwwroot/js/*.min.js"],

        // compile sass to css with map
        sass: {
            dist: {
                files: {
                    'wwwroot/css/galerie-renovation.css': 'wwwroot/scss/galerie-renovation.scss'
                },
                sourcemap: 'auto'
            }
        },

        // minify javascript files
        uglify: {
            options: {
                beautify: true
            },
            gr: {
                files: {
                    'wwwroot/js/header-scroll.min.js': ['wwwroot/js/header-scroll.js']
                }
            }
        },

        // minify css files
        cssmin: {
            target: {
                files: [{
                    expand: true,
                    cwd: 'wwwroot/css',
                    src: ['*.css', '!*.min.css'],
                    dest: 'wwwroot/css',
                    ext: '.min.css'
                }]
            }
        }
    });

    grunt.loadNpmTasks("grunt-contrib-watch");
    grunt.loadNpmTasks('grunt-contrib-clean');
    grunt.loadNpmTasks("grunt-contrib-sass");
    grunt.loadNpmTasks("grunt-contrib-uglify");
    grunt.loadNpmTasks("grunt-contrib-cssmin");
};
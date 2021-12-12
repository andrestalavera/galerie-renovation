module.exports = function (grunt) {
    grunt.initConfig({
        // a task to know which files are edited 
        watch: ["wwwroot/scss/*", "wwwroot/ts/*"],

        // delete old generated files
        clean: ["wwwroot/css/*", "wwwroot/js/*"],

        // compile cass to css
        sass: {
            dist: {
                files: {
                    'wwwroot/css/galerie-renovation.css': 'wwwroot/scss/galerie-renovation.scss'
                }
            }
        },

        uglify: {
            options: {
                beautify: true
            },
            files: {
                'www/css/galerie-renovation.min.css': ['www/css/galerie-renovation.css']
            }
        }
    });

    grunt.loadNpmTasks("grunt-contrib-watch");
    grunt.loadNpmTasks('grunt-contrib-clean');
    grunt.loadNpmTasks("grunt-contrib-sass");
    grunt.loadNpmTasks("grunt-contrib-uglify");
};
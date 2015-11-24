/*
This file in the main entry point for defining Gulp tasks and using Gulp plugins.
Click here to learn more. http://go.microsoft.com/fwlink/?LinkId=518007
*/

var gulp = require('gulp');
var less = require('gulp-less');
var uglify = require('gulp-uglify');

var paths = {
    src: "./Assets/**/*.js",
    dest: "./wwwroot/js/"
}

gulp.task('default', function () {
    return gulp.src(paths.src)         // Returns a stream
        .pipe(gulp.dest(paths.dest))   // Pipes the stream somewhere
});
    gulp.task("clean", function () {
        del(paths.dest + '**/*');    // Delete everything in 'wwwroot/js'
    });

    gulp.task("hello", function () {
        console.log("gdgdg");
    });

    gulp.task('less', function () {
        gulp.src('./wwwroot/css/*.less')
            .pipe(less())
            .pipe(gulp.dest('./wwwroot/' + 'css'));
    });

    gulp.task('minify', function () {
        gulp.src('./wwwroot/js/*.js')
            .pipe(uglify())
            .pipe(gulp.dest('./wwwroot/lib/myjs'));
    });
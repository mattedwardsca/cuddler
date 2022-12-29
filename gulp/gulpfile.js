const { src, dest, watch, series, task } = require("gulp");
const minifyCss = require("gulp-clean-css");
const sourceMap = require("gulp-sourcemaps");
const concat = require("gulp-concat-css");

const cssFiles = [
	"../src/Cuddler/Pages/**/!(*.min).css",
];

const bundle = () => {
    return src(cssFiles)
        .pipe(sourceMap.init())
        //.pipe(minifyCss())
        .pipe(concat("components.css"))
        .pipe(sourceMap.write())
        .pipe(dest("../src/css/"));
};

const minify = () => {
    return src("../src/css/components.css")
        .pipe(concat("components.min.css"))
        .pipe(minifyCss())
        .pipe(dest("../src/css/"));
};

const place = () => {
    return src("../src/css/*.*")
        .pipe(dest("../src/Cuddler/wwwroot/css"));
};

const devWatch = () => {
    watch(cssFiles, series(bundle, minify, place));
};

exports.bundle = bundle;
exports.minify = minify;
exports.devWatch = devWatch;

task("default", series(bundle, minify, place, devWatch));


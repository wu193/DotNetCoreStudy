module.exports = function (grunt) {
    grunt.initConfig({
        /*删除文件*/
        clean: ["wwwroot/lib/*"],
        /*合并*/
        //concat: {
        //    all: {
        //        src:['']
        //    }
        //}
    })
    //grunt.registerTask("all", ['clean'])
    grunt.registerTask("clean")
}
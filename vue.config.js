module.exports = {
    outputDir: "./wwwroot/",
    transpileDependencies: [
        "vuetify"
    ],
    devServer: {
        public: "localhost",
        host: "127.0.0.1"
    },
    runtimeCompiler: true,
    productionSourceMap: false
}
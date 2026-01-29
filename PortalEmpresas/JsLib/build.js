const esbuild = require("esbuild");

const isWatch = process.argv.includes("--watch");

(async () => {
    if (isWatch) {
        const ctx = await esbuild.context({
            entryPoints: ["src/index.js"],
            bundle: true,
            minify: false,
            sourcemap: true,
            outfile: "../wwwroot/js/my_lib.js",
            format: "iife",
            globalName: "MyLib",
            target: ["es2020"]
        });

        await ctx.watch();
        console.log("👀 esbuild en modo WATCH");
    } else {
        await esbuild.build({
            entryPoints: ["src/index.js"],
            bundle: true,
            minify: true,
            sourcemap: false,
            outfile: "../wwwroot/js/my_lib.js",
            format: "iife",
            globalName: "MyLib",
            target: ["es2020"]
        });

        console.log("🚀 Build JS completado");
    }
})();

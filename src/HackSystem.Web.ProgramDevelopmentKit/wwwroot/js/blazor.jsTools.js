window.blazorJSTools = {
    loaded: [],
    importJavaScript: function (url) {
        if (blazorJSTools.loaded[url]) {
            console.log(`script ${url} already loaded.`);
            return;
        }

        var script = document.createElement('script');
        script.type = 'text/javascript';
        script.async = true;
        script.src = url;
        script.onload = function () {
            blazorJSTools.loaded[url] = true;
            console.log(`load script ${url} successfully.`);
        };
        script.onerror = function () {
            console.log(`load script ${url} failed.`);
            reject(scriptPath);
        }
        document.body.appendChild(script);
        console.log(`loading script ${url}.`);
    }
}
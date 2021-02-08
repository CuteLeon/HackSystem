/* Have to modify {PublishDir}/wwwroot/_framework/blazor.webassembly.js file Manually!
e.prototype.loadResources = function (e, t, n) {var r = this;var resourcesCount = Object.keys(e).length, index = 0;console.log(`Blazor WebAssembly => Loading ${resourcesCount} resources...`);return Object.keys(e).map((function (o) {var p = r.loadResource(o, t(o), e[o], n);p.response.then((x) => {if (typeof window.loadResourceCallback === "function") {window.loadResourceCallback(++index, resourcesCount, o);}});return p;}))},
e.prototype.loadResources = function (e, t, n) {
    var r = this;
    var resourcesCount = Object.keys(e).length, index = 0; // Added new logic
    console.log(`Blazor WebAssembly => Loading ${resourcesCount} resources...`); // Added new logic
    return Object.keys(e).map((function (o) {
        var p = r.loadResource(o, t(o), e[o], n);
        p.response.then((x) => { // Added new logic
            if (typeof window.loadResourceCallback === "function") { // Added new logic
                window.loadResourceCallback(++index, resourcesCount, o); // Added new logic
            }
        });
        return p;
    }))
},
 */
let setupProgressIndexLabel = $('#setupProgressIndexLabel'),
    setupProgressCurrentLabel = $('#setupProgressCurrentLabel'),
    setupProgressBar = $('#setupProgressBar');
setupProgressBar.toggleClass('progress-bar-animated');

window.loadResourceCallback = function (index, count, current) {
    // console.log(`${index} / ${count} => ${current}`);
    setupProgressIndexLabel.text(`Loading Resources ${index}/${count}`);
    setupProgressCurrentLabel.text(current);

    let progress = Math.round(index / count * 100);
    setupProgressBar.width(`${progress}%`);
};
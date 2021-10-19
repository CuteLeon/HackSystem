/* Have to modify {PublishDir}/wwwroot/_framework/blazor.webassembly.js file Manually!
    loadResources(e, t, n) {let resourcesCount = Object.keys(e).length, index = 0; console.log(`Blazor WebAssembly => Loading ${resourcesCount} resources...`); return Object.keys(e).map((r => { var resource = this.loadResource(r, t(r), e[r], n); resource.response.then((x) => {if (typeof window.loadResourceCallback === "function") {window.loadResourceCallback(++index, resourcesCount, r)}}); return resource;}))}
    loadResources(e, t, n) {
        let resourcesCount = Object.keys(e).length, index = 0;
        console.log(`Blazor WebAssembly => Loading ${resourcesCount} resources...`);
        return Object.keys(e).map((r => {
            var resource = this.loadResource(r, t(r), e[r], n);
            resource.response.then((x) => {
                if (typeof window.loadResourceCallback === "function") {
                    window.loadResourceCallback(++index, resourcesCount, r);
                }
            })
            return resource;
        }))
    }
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
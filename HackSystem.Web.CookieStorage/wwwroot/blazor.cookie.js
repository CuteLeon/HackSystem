export function getCookies() {
    var cookies = document.cookie.split(';');
    var cookieObject = new Object();
    if (cookies == null || cookies.length == 0)
        return cookieObject;

    for (let index = 0; index < cookies.length; index++) {
        let cookie = cookies[index];
        var pair = cookie.split('=', 2);
        if (pair != null && pair.length == 2) {
            let name = pair[0].trim();
            let value = pair[1];
            cookieObject[name] = value;
        }
    }

    return cookieObject;
}

export function saveCookie(name, value, expiresInSecond = -1) {
    if (!name) return;
    name = name.trim();
    let command = `${name}=${value}`;

    if (expiresInSecond > -1) {
        let expires = Date.now() + expiresInSecond * 1000;
        let expiresDate = new Date(expires);
        command = `${command};expires=${expiresDate.toGMTString()}`;
    }

    document.cookie = command;
}

export function removeCookie(name) {
    if (!name) return;
    name = name.trim();
    document.cookie = `${name}=;expires=${new Date(-1).toGMTString()}`;
}

export function getCookie(name) {
    if (!name) return null;
    name = name.trim();
    var matches = document.cookie.match(`(^|;\\s)${name}=(.*?)($|;)`);
    if (matches === null || matches.length < 3)
        return null;
    return matches[2];
}

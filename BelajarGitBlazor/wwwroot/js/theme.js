window.setTheme = function (theme) {
    document.documentElement.setAttribute('data-theme', theme);
    try { localStorage.setItem('blazor-theme', theme); } catch (e) { }
};

window.getStoredTheme = function () {
    try { return localStorage.getItem('blazor-theme') || 'light'; } catch (e) { return 'light'; }
};

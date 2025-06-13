var ROOT = "/Portal";
ROOT = "";

var EMPTY_GUID = "00000000-0000-0000-0000-000000000000";
var MODULES = [];
var MODSTACK = [];
var CURRENT_MODULE_ID = null;
var CONTEXT;
var SPA_TID;

var themes = {
    "default": "bootstrap/css/bootstrap.min.css",
    "amelia": "bootstrap/themes/amelia/bootstrap.min.css",
    "cerulean": "bootstrap/themes/cerulean/bootstrap.min.css",
    "cosmo": "bootstrap/themes/cosmo/bootstrap.min.css",
    "cyborg": "bootstrap/themes/cyborg/bootstrap.min.css",
    "darkly": "bootstrap/themes/darkly/bootstrap.min.css",
    "flatly": "bootstrap/themes/flatly/bootstrap.min.css",
    "journal": "bootstrap/themes/journal/bootstrap.min.css",
    "lumen": "bootstrap/themes/lumen/bootstrap.min.css",
    "paper": "bootstrap/themes/paper/bootstrap.min.css",
    "readable": "bootstrap/themes/readable/bootstrap.min.css",
    "sandstone": "bootstrap/themes/sandstone/bootstrap.min.css",
    "shamrock": "bootstrap/themes/shamrock/bootstrap.min.css",
    "simplex": "bootstrap/themes/simplex/bootstrap.min.css",
    "slate": "bootstrap/themes/slate/bootstrap.min.css",
    "spacelab": "bootstrap/themes/spacelab/bootstrap.min.css",
    "superhero": "bootstrap/themes/superhero/bootstrap.min.css",
    "united": "bootstrap/themes/united/bootstrap.min.css",
    "yeti": "bootstrap/themes/yeti/bootstrap.min.css"
}

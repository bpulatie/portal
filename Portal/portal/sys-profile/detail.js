function init(module, params) {

    var ID = CONTEXT.user.user_id;

    module.aMenu = new Array(['1', 'Top'], 
                             ['2', 'Left']);

    module.aTheme = new Array(["default",   "Default"],
                              ["amelia",    "Amelia"],
                              ["cerulean",  "Cerulean"],
                              ["cosmo",     "Cosmo"],
                              ["cyborg",    "Cyborg"],
                              ["darkly",    "Darkly"],
                              ["flatly",    "Flatly"],
                              ["journal",   "Journal"],
                              ["lumen",     "Lumen"],
                              ["paper",     "Paper"],
                              ["readable",  "Readable"],
                              ["sandstone", "Sandstone"],
                              ["shamrock",  "Shamrock"],
                              ["simplex",   "Simplex"],
                              ["slate",     "Slate"],
                              ["spacelab",  "Spacelab"],
                              ["superhero", "Superhero"],
                              ["united",    "United"],
                              ["yeti",      "Yeti"]);

    module.addOptions('menu', module.aMenu);
    module.addOptions('style', module.aTheme);

    module.dsUser = new spaDataset({
        id: 'dsUser',
        table: 'sys_user',
        pk: 'user_id',
        webservice: '/services/sys_user_Services.asmx/GetProfileUser'
    });


    module.saveBtn_onclick = function() {
        if (module.validateForm() == true) {
            module.dsUser.Save(function () {
                DisplayMessage(module, 'Save Successful');
            });
        };
    }


    module.closeBtn_onclick = function () {
        module.closeModule();
        return false;
    }

    module.validateForm = function () {
        return true;
    }

    //Events
    module.myEventColumns = [{ label: 'Event Category', width: '50%', field: 'event_category_name'},
                             { label: 'Event', width: '50%', field: 'event_name', class: 'key' }];

    module.myEventFilters = [{ name: 'myEventCategory', field: 'event_category_name', label: 'Event Category', criteria: 'contains' },
                             { name: 'myEventName', field: 'event_name', label: 'Event Name', criteria: 'contains'}];

    module.eventGrid = new spaGrid({
        form: module,
        id: 'eventGrid',
        title: "Events",
        columns: module.myEventColumns,
        filters: module.myEventFilters,
        rowID: 'event_subscription_id',
        height: 270,
        newBtn: 'Subscribe To Event',
        newLocation: function () {
            createModule("Event Subscription", "/portal/sys-profile/subscription.htm", { 'user_id': ID, 'id': EMPTY_GUID }, 0);
        },
        onSelect: function (id) {
            createModule("Event Subscription", "/portal/sys-profile/subscription.htm", { 'user_id': ID, 'id': id }, 0);
        }
    });

    module.dsBrowseEvents = new spaDataset({
        form: module,
        id: 'dsBrowseEvents',
        pk: 'event_type_id',
        webservice: '/services/sys_event_Services.asmx/GetAllSubscribedEvents',
        params: '"user_id": "' + ID + '"',
        pageSize: 30
    });

    module.refresh = function () {
        module.dsBrowseEvents.Reload();
    };
}




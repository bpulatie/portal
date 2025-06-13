function init(module, params) {

    var ID = CONTEXT.user.user_id;

    module.closeBtn_onclick = function () {
        module.closeModule();
        return false;
    }

    module.validateForm = function () {
        return true;
    }

    module.dsBrowseEvents = new spaDataset({
        form: module,
        id: 'dsBrowseEvents',
        pk: 'event_type_id',
        webservice: '/services/sys_event_Services.asmx/GetDashboardEvents',
        params: '"user_id": "' + ID + '"',
        pageSize: 30
    });


}




function init(module, params) {

    var ID = EMPTY_GUID;

    ID = params.id;

    module.dsDetail = new spaDataset({
        form: module,
        id: 'dsDetail',
        table: 'sys_event',
        pk: 'event_id',
        webservice: '/services/sys_event_Services.asmx/GetByID',
        params: '"id": "' + ID + '"'
    });

    module.closeBtn_onclick = function () {
        module.closeModule();
        return false;
    }

}





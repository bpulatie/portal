function init(module, params) {

    var ID = EMPTY_GUID;

    if (params.id != EMPTY_GUID) {
        ID = params.id;
    } 

    module.dsDetail = new spaDataset({
        form: module,
        id: 'dsDetail',
        table: 'sys_session',
        pk: 'session_id',
        webservice: '/services/sys_session_Services.asmx/GetByID',
        params: '"id": "' + ID + '"'
    });

}









function init(module, params) {

    var ID = EMPTY_GUID;

    if (params.id != EMPTY_GUID) {
        ID = params.id;
    } else {
        module.hideDeleteBtn();
    }


    module.dsClients = new spaDataset({
        form: module,
        id: 'dsClients',
        table: 'sys_client',
        pk: 'client_id',
        webservice: '/services/sys_client_Services.asmx/GetAll',
        onLoad: function () {
            module.aClients = new Array();

            for (var x = 0; x < module.dsClients.RowCount(); x++) {
                var row = module.dsClients.GetRow(x);
                module.aClients.push([row.client_id, row.name]);
            }

            module.addOptions('client_id', module.aClients);

            module.dsDetail = new spaDataset({
                form: module,
                id: 'dsDetail',
                table: 'sys_client_note',
                pk: 'client_note_id',
                webservice: '/services/sys_client_Services.asmx/GetNoteByID',
                params: '"client_note_id": "' + ID + '"',
                onLoad: function () {
                    var id = EMPTY_GUID;
                    if (ID == EMPTY_GUID) {
                        module.dsDetail.AddRow();
                    } else {
                        id = module.dsDetail.GetCurrentRow().client_id;
                    }

                    module.dsClient = new spaDataset({
                        form: module,
                        id: 'dsClient',
                        table: 'sys_client',
                        pk: 'client_id',
                        webservice: '/services/sys_client_Services.asmx/GetByID',
                        params: '"client_id": "' + id + '"',
                        onLoad: function () {

                        }
                    });

                }
            });
        }
    });

    module.client_id_change = function (id) {
        module.dsClient.settings.params = '"client_id": "' + id + '"';
        module.dsClient.Reload();
    }

    // Button Actions
    module.saveBtn_onclick = function () {
        if (module.validateForm() == true) {
            module.dsDetail.Save(function (id) {
                ID = id;
                module.showDeleteBtn();
                DisplayMessage(module, 'Save Successful');
            });
        }
    }

    module.deleteBtn_onclick = function () {
        module.dsDetail.DeleteRow();
        module.dsDetail.Save(function () {
            module.closeModule();
            return false;
        });
    }

    module.validateForm = function () {
        return true;
    }

}



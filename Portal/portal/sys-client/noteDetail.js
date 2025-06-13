function init(module, params) {
    
    var ID = EMPTY_GUID;

    if (params.id != EMPTY_GUID) {
        ID = params.id;
    } else {
        module.hideDeleteBtn();
    }

    module.dsDetail = new spaDataset({
        form: module,
        id: 'dsDetail',
        table: 'sys_client_note',
        pk: 'client_note_id',
        webservice: '/services/sys_client_Services.asmx/GetNoteByID',
        params: '"client_note_id": "' + ID + '"',
        onLoad: function () {
            if (ID == EMPTY_GUID) {
                module.dsDetail.AddRow();
                module.dsDetail.EditColumn("client_id", params.client_id);
            }
        }
    });

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



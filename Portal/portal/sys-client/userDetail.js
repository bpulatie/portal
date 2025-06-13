function init(module, params) {
    
    var ID = EMPTY_GUID;

    if (params.id != EMPTY_GUID) {
        ID = params.id;
    } else {
        module.hideDeleteBtn();
    }

    module.aGenders = new Array(['f', 'Female'], ['m', 'Male']);
    module.aUserType = new Array(['c', 'Admin'], ['u', 'User']);

    module.addOptions('gender', module.aGenders);
    module.addOptions('user_type', module.aUserType);

    module.dsDetail = new spaDataset({
        form: module,
        id: 'dsDetail',
        table: 'sys_user',
        pk: 'user_id',
        webservice: '/services/sys_user_Services.asmx/GetByID',
        params: '"user_id": "' + ID + '"',
        onLoad: function () {
            if (ID == EMPTY_GUID) {
                module.dsDetail.AddRow();
                module.dsDetail.EditColumn("client_id", params.client_id);
                module.dsDetail.EditColumn("user_type", "c");
                module.setValue("user_type", "c");
            }
        }
    });

    // Button Actions
    module.saveBtn_onclick = function () {
        if (module.validateForm() == true) {
            module.dsDetail.Save(function (id) {
                ID = id;
                module.dsDetail.settings.params = '"user_id": "' + ID + '"';
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



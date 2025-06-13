function init(module, params) {

    var ID = EMPTY_GUID;
    var APPID = EMPTY_GUID;

    if (params.id != EMPTY_GUID) {
        ID = params.id;
    } else {
        APPID = params.application_id;
        module.hideDeleteBtn();
    }

    module.dsDetail = new spaDataset({
        form: module,
        id: 'dsDetail',
        table: 'sys_feature',
        pk: 'feature_id',
        webservice: '/services/sys_feature_Services.asmx/GetByID',
        params: '"id": "' + ID + '"',
        onLoad: function () {
            if (ID == EMPTY_GUID) {
                module.dsDetail.AddRow();
                module.dsDetail.EditColumn("application_id", APPID);
            }
        }
    });

    module.saveBtn_onclick = function () {
        if (module.validateForm() == true) {
            module.dsDetail.Save(function () {
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

    module.closeBtn_onclick = function () {
        module.closeModule();
        return false;
    }

    module.validateForm = function () {
        return true;
    }
}





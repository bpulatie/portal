﻿function init(module, params) {

    var ID = EMPTY_GUID;

    if (params.id != EMPTY_GUID) {
        ID = params.id;
    } else {
        module.hideDeleteBtn();
    }

    module.dsDetail = new spaDataset({
        form: module,
        id: 'dsDetail',
        table: 'sys_reason_category',
        pk: 'reason_category_id',
        webservice: '/services/sys_reason_Services.asmx/GetByCategoryID',
        params: '"reason_category_id": "' + ID + '"',
        onLoad: function () {
            if (ID == EMPTY_GUID) {
                module.dsDetail.AddRow();
            }
        }
    });

    // Button Actions
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

    module.validateForm = function () {
        return true;
    }


}





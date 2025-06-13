function init(module, params) {

    var ID = EMPTY_GUID;

    if (params.id != EMPTY_GUID) {
        ID = params.id;
    } else {
        module.hideDeleteBtn();
    }

    module.aCategory = new Array(); 

    module.dsCategory = new spaDataset({
        form: module,
        id: 'dsCategory',
        table: 'sys_reason_category',
        pk: 'reason_category_id',
        webservice: '/services/sys_reason_Services.asmx/GetAllCategories',
        onLoad: function () {
            for (var x = 0; x < module.dsCategory.RowCount(); x++) {
                var row = module.dsCategory.GetRow(x);
                module.aCategory.push([row.reason_category_id, row.reason_category]);
            }

            module.addOptions('reason_category_id', module.aCategory);

            module.dsDetail = new spaDataset({
                form: module,
                id: 'dsDetail',
                table: 'sys_reason',
                pk: 'reason_id',
                webservice: '/services/sys_reason_Services.asmx/GetByID',
                params: '"reason_id": "' + ID + '"',
                onLoad: function () {
                    if (ID == EMPTY_GUID) {
                        module.dsDetail.AddRow();
                    }
                }
            });
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





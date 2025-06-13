function init(module, params) {

    var ID = EMPTY_GUID;

    if (params.id != EMPTY_GUID) {
        ID = params.id;
    } else {
        module.hideDeleteBtn();
    }

    module.aGroups = new Array(); ;

    module.dsGroup = new spaDataset({
        form: module,
        id: 'dsGroup',
        table: 'sys_feature_group',
        pk: 'group_id',
        webservice: '/services/sys_feature_Services.asmx/GetAllFeatureGroups',
        onLoad: function () {
            for (var x = 0; x < module.dsGroup.RowCount(); x++) {
                var row = module.dsGroup.GetRow(x);
                module.aGroups.push([row.group_id, row.group_name]);
            }

            module.addOptions('group_id', module.aGroups);

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
                    }
                }
            });

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





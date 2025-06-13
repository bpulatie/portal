function init(module, params) {

    var ID = EMPTY_GUID;

    if (params.id != EMPTY_GUID) {
        ID = params.id;
    } else {
        module.hideDeleteBtn();
    }

    module.dsLevel = new spaDataset({
        form: module,
        id: 'dsLevel',
        table: 'inv_category_level',
        pk: 'category_level_id',
        webservice: '/services/inv_category_level_Services.asmx/GetAll',
        onLoad: function () {
            module.aLevels = new Array();

            for (var x = 0; x < module.dsLevel.RowCount(); x++) {
                var row = module.dsLevel.GetRow(x);
                module.aLevels.push([row.category_level_id, row.level_name]);
            }

            module.addOptions('category_level_id', module.aLevels);            
        }
    });

    $('#' + module.module_id + "_category_level_id_hidden").change(function () {
        module.dsParent.settings.params = '"category_level_id": "' + module.getValue('category_level_id') + '"';
        module.dsParent.Reload();
    });

    module.dsParent = new spaDataset({
        form: module,
        id: 'dsParent',
        table: 'inv_category',
        pk: 'category_id',
        webservice: '/services/inv_category_Services.asmx/GetAllByLevel',
        params: '"category_level_id": "' + module.getValue('category_level_id') + '"',
        onLoad: function () {
            module.aParents = new Array();

            for (var x = 0; x < module.dsParent.RowCount(); x++) {
                var row = module.dsParent.GetRow(x);
                module.aParents.push([row.category_id, row.name]);
            }

            module.addOptions('parent_category_id', module.aParents);
        }
    });


    module.dsDetail = new spaDataset({
        form: module,
        id: 'dsDetail',
        table: 'inv_category',
        pk: 'category_id',
        webservice: '/services/inv_category_Services.asmx/GetByID',
        params: '"category_id": "' + ID + '"',
        onLoad: function () {
            if (ID == EMPTY_GUID) {
                module.dsDetail.AddRow();
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





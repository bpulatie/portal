function init(module, params) {

    var ID = EMPTY_GUID;

    if (params.id != EMPTY_GUID) {
        ID = params.id;
    } else {
        module.hideDeleteBtn();
    }

    module.aItemType = new Array( ['g', 'General Merchandise'], ['f', 'Fuel Item'] );
    module.addOptions('item_type', module.aItemType);

    module.dsCategory = new spaDataset({
        form: module,
        id: 'dsCategory',
        table: 'inv_category',
        pk: 'category_id',
        webservice: '/services/inv_category_Services.asmx/GetAllLeafCategories',
        onLoad: function () {
            module.aCategory = new Array();

            for (var x = 0; x < module.dsCategory.RowCount(); x++) {
                var row = module.dsCategory.GetRow(x);
                module.aCategory.push([row.category_id, row.name]);
            }

            module.addOptions('category_id', module.aCategory);

            module.dsDetail = new spaDataset({
                form: module,
                id: 'dsDetail',
                table: 'inv_item',
                pk: 'item_id',
                webservice: '/services/inv_item_Services.asmx/GetByID',
                params: '"item_id": "' + ID + '"',
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





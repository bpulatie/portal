function init(module, params) {

    var ID = EMPTY_GUID;

    if (params.id != EMPTY_GUID) {
        ID = params.id;
    } else {
        module.hideDeleteBtn();
    }

    module.dsDepartment = new spaDataset({
        form: module,
        id: 'dsDepartment',
        table: 'supplier_item_department',
        pk: 'supplier_item_department_id',
        webservice: '/services/custom_supplier_Services.asmx/GetAllDepartments',
        params: '"supplier_id": "' + params.supplier_id + '"',
        onLoad: function () {
            module.aDepartments = new Array();

            for (var x = 0; x < module.dsDepartment.RowCount(); x++) {
                var row = module.dsDepartment.GetRow(x);
                module.aDepartments.push([row.supplier_item_department_id, row.department_name]);
            }

            module.addOptions('SupplierItemDepartmentID', module.aDepartments);            

            module.dsDetail = new spaDataset({
                form: module,
                id: 'dsDetail',
                table: 'supplier_catalog_current',
                pk: 'supplier_catalog_id',
                webservice: '/services/custom_supplier_Services.asmx/GetSupplierItemByID',
                params: '"supplier_catalog_id": "' + ID + '"',
                onLoad: function () {

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


    module.closeBtn_onclick = function () {
        module.closeModule();
        return false;
    }

    module.validateForm = function () {
        return true;
    }

}





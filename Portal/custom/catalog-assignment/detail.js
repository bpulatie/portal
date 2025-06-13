function init(module, params) {

    var ID = EMPTY_GUID;

    if (params.id != EMPTY_GUID) {
        ID = params.id;
    }

    module.setValue("SupplierName", params.supplier_name);

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
        }
    });

    module.myColumns = [{ label: 'Description', width: '40%', field: 'product_description' },
                        { label: 'Supplier Category', width: '40%', field: 'supplier_category' },
                        { label: 'Supplier Item Code', width: '10%', field: 'supplier_item_code' },
                        { label: 'Select', width: '10%', field: 'Select', class: 'key' }];

    module.myFilters = [{ name: 'myItem', field: 'product_description', label: 'Description', criteria: 'contains' },
                        { name: 'myCategory', field: 'supplier_category', label: 'Supplier Category', criteria: 'contains' },
                        { name: 'myCode', field: 'supplier_item_code', label: 'Supplier Item Code', criteria: 'contains'}];

    module.myGrid = new spaGrid({
        form: module,
        id: 'myGrid',
        title: "Unassigned Supplier Items",
        columns: module.myColumns,
        filters: module.myFilters,
        rowID: 'supplier_catalog_id',
        height: 330,
        onSelect: function (id) {
            var SupplierItemDepartmentID = module.getValue("SupplierItemDepartmentID");
            if (SupplierItemDepartmentID == "")
                ShowSystemModal("Hint", 'Please Select a Department');

            $.ajax({
                type: "POST",
                contentType: "application/json",
                url: ROOT + "/services/custom_supplier_Services.asmx/AssignDepartment",
                data: "{ 'supplier_catalog_id': '" + id + "', 'supplier_item_department_id': '" + SupplierItemDepartmentID + "' }",
                dataType: "json",
                success: function (myResponse) {
                    if (myResponse.result == true) {
                        module.dsBrowse.Reload();
                    } else {
                        ShowSystemModal('Application Error', 'Message: ' + myResponse.message);
                    }
                },
                error: ajaxFailed
            });
        }
    });

    module.dsBrowse = new spaDataset({
        form: module,
        id: 'dsBrowse',
        pk: 'supplier_catalog_id',
        params: '"supplier_id": "' + params.supplier_id + '"',
        webservice: '/services/custom_supplier_Services.asmx/GetUnassignedBySupplier',
        pageSize: 30
    });

    module.refresh = function () {
        module.dsBrowse.Reload();
    };
}


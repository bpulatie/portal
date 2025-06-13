function init(module, params) {

    module.myColumns = [{ label: 'Supplier', width: '50%', field: 'supplier_name', class: 'key' },
                        { label: 'Assigned Item Count', width: '25%', field: 'assigned_items'},
                        { label: 'Unassigned Item Count', width: '25%', field: 'unassigned_items'}];

    module.myFilters = [{ name: 'mySupplier', field: 'name', label: 'Supplier Name', criteria: 'contains' }];

    module.myGrid = new spaGrid({
        form: module,
        id: 'myGrid',
        title: "Supplier Browse",
        columns: module.myColumns,
        filters: module.myFilters,
        rowID: 'supplier_id',
        height: 430,
        onSelect: function (id) {
            var SupplierID = module.dsBrowse.GetCurrentRow().supplier_id;
            var SupplierName = module.dsBrowse.GetCurrentRow().supplier_name;
            createModule("Unassigned Supplier Items", "/custom/catalog-assignment/detail.htm", { 'id': id, 'supplier_id': SupplierID, 'supplier_name': SupplierName }, 0);
        }
    });

    module.dsBrowse = new spaDataset({
        form: module,
        id: 'dsBrowse',
        pk: 'category_id',
        webservice: '/services/custom_supplier_Services.asmx/GetCountBySupplier',
        pageSize: 30
    });

    module.refresh = function () {
        module.dsBrowse.Reload();
    };

    // Actions
    module.getCatalogBtn_onclick = function () {
        $.ajax({
            type: "POST",
            contentType: "application/json",
            url: ROOT + "/services/custom_supplier_Services.asmx/GetCatalogData",
            data: '',
            dataType: "json",
            success: function (myResponse) {
                if (myResponse.result == true) {
                    DisplayMessage(module, 'Get Catalog Request Submitted');
                    module.dsBrowse.Reload();
                } else {
                    ShowSystemModal('Application Error', 'Message: ' + myResponse.message);
                }
            },
            error: ajaxFailed
        });
    }

    module.sendCatalogBtn_onclick = function () {
        $.ajax({
            type: "POST",
            contentType: "application/json",
            url: ROOT + "/services/custom_supplier_Services.asmx/SendCatalogData",
            data: '',
            dataType: "json",
            success: function (myResponse) {
                if (myResponse.result == true) {
                    DisplayMessage(module, 'Send Catalog Request Submitted');
                } else {
                    ShowSystemModal('Application Error', 'Message: ' + myResponse.message);
                }
            },
            error: ajaxFailed
        });
    }
}


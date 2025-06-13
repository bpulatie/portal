function init(module, params) {

    module.myColumns = [{ label: 'Description', width: '25%', field: 'product_description', class: 'key'},
                        { label: 'Supplier', width: '25%', field: 'supplier_name' },
                        { label: 'Supplier Category', width: '20%', field: 'supplier_category'},
                        { label: 'Supplier Item Code', width: '10%', field: 'supplier_item_code'},
                        { label: 'Group', width: '20%', field: 'department_name'}];

    module.myFilters = [{ name: 'myItem', field: 'product_description', label: 'Description', criteria: 'contains' },
                        { name: 'mySupplier', field: 's.name', label: 'Supplier Name', criteria: 'contains' },
                        { name: 'myCategory', field: 'supplier_category', label: 'Supplier Category', criteria: 'contains' },
                        { name: 'myCode', field: 'supplier_item_code', label: 'Supplier Item Code', criteria: 'contains' }];

    module.myGrid = new spaGrid({
        form: module,
        id: 'myGrid',
        title: "Supplier Item Catalog Browse",
        columns: module.myColumns,
        filters: module.myFilters,
        rowID: 'supplier_catalog_id',
        height: 430,
        onSelect: function (id) {
            var SupplierID = module.dsBrowse.GetCurrentRow().supplier_id;
            createModule("Supplier Catalog Detail", "/custom/supplier-catalog/detail.htm", { 'id': id, 'supplier_id': SupplierID }, 0);
        }
    });

    module.dsBrowse = new spaDataset({
        form: module,
        id: 'dsBrowse',
        pk: 'category_id',
        webservice: '/services/custom_supplier_Services.asmx/GetAllByCategory',
        pageSize: 30
    });

    module.refresh = function () {
        module.dsBrowse.Reload();
    };
}


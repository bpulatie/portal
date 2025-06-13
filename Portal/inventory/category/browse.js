function init(module, params) {

    module.myColumns = [{ label: 'Category', width: '50%', field: 'name', class: 'key', link: '/inventory/category/detail.htm', menu: 'Category: %%' },
                        { label: 'Level Name', width: '30%', field: 'level_name' },
                        { label: 'Level', width: '20%', field: 'depth'}];

    module.myFilters = [{ name: 'myHeader', field: 'name', label: 'Name', criteria: 'contains' }];

    module.myGrid = new spaGrid({
        form: module,
        id: 'myGrid',
        title: "Category Browse",
        columns: module.myColumns,
        filters: module.myFilters,
        rowID: 'category_id',
        height: 430,
        newBtn: 'Add New Category',
        newLocation: '/inventory/category/detail.htm',
        onLoad: function () {
        }
    });

    module.dsBrowse = new spaDataset({
        form: module,
        id: 'dsBrowse',
        pk: 'category_id',
        webservice: '/services/inv_category_Services.asmx/GetAllHierarchy',
        pageSize: 30
    });

    module.refresh = function () {
        module.dsBrowse.Reload();
    };
}


function init(module, params) {

    module.myColumns = [{ label: 'Level', width: '20%', field: 'depth' },
                        { label: 'Category Level', width: '80%', field: 'level_name', class: 'key', link: '/inventory/category-level/detail.htm', menu: 'Category Level: %%'} ];

    module.myFilters = [{ name: 'myHeader', field: 'level_name', label: 'Level Name', criteria: 'contains'}];

    module.myGrid = new spaGrid({
        form: module,
        id: 'myGrid',
        title: "Category Level Browse",
        columns: module.myColumns,
        filters: module.myFilters,
        rowID: 'category_level_id',
        height: 430,
        onLoad: function () {
        }
    });

    module.dsBrowse = new spaDataset({
        form: module,
        id: 'dsBrowse',
        pk: 'category_level_id',
        webservice: '/services/inv_category_level_Services.asmx/GetAll',
        pageSize: 30
    });

    module.refresh = function () {
        module.dsBrowse.Reload();
    };
}


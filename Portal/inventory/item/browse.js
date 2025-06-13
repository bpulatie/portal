function init(module, params) {

    module.myColumns = [{ label: 'Item Name', width: '20%', field: 'item_name', class: 'key', link: '/inventory/item/detail.htm', menu: 'Item: %%' },
                        { label: 'Category Name', width: '20%', field: 'category_name' },
                        { label: 'External ID', width: '20%', field: 'external_id' },
                        { label: 'Active', width: '10%', field: 'active_flag', type: 'check' },
                        { label: 'Buy', width: '10%', field: 'buy_flag', type: 'check' },
                        { label: 'Sell', width: '10%', field: 'sell_flag', type: 'check' },
                        { label: 'Count', width: '10%', field: 'count_flag', type: 'check'}];

    module.myFilters = [{ name: 'myHeader', field: 'item_name', label: 'Item Name', criteria: 'contains' },
                        { name: 'myCategory', field: 'category_name', label: 'Category Name', criteria: 'contains' }];

    module.myGrid = new spaGrid({
        form: module,
        id: 'myGrid',
        title: "Item Browse",
        columns: module.myColumns,
        filters: module.myFilters,
        rowID: 'item_id',
        height: 430,
        newBtn: 'Add New Item',
        newLocation: '/inventory/item/detail.htm',
        onLoad: function () {
        }
    });

    module.dsBrowse = new spaDataset({
        form: module,
        id: 'dsBrowse',
        pk: 'item_id',
        webservice: '/services/inv_item_Services.asmx/GetAll',
        pageSize: 30
    });

    module.refresh = function () {
        module.dsBrowse.Reload();
    };
}


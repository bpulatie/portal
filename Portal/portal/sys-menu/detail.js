function init(module, params) {

    var ID = EMPTY_GUID;

    if (params.id != EMPTY_GUID) {
        ID = params.id;
    } else {
        module.hideDeleteBtn();
        module.hideTab('features');
    }

    module.aAccess = new Array(['0', 'Update'], ['1', 'View']);
    module.addOptions('menu_mode', module.aAccess);
    module.setValue('menu_mode', '0');
    module.setValue("item_sort_order", "0");

    module.dsDetail = new spaDataset({
        form: module,
        id: 'dsDetail',
        table: 'sys_menu',
        pk: 'menu_id',
        webservice: '/services/sys_menu_Services.asmx/GetByID',
        params: '"menu_id": "' + ID + '"',
        onLoad: function () {
            if (ID == EMPTY_GUID) {
                module.dsDetail.AddRow();
            }
        } 
    });

    module.saveBtn_onclick = function () {
        if (module.validateForm() == true) {
            module.dsDetail.Save(function (id) {
                ID = id;
                module.dsBrowseFeatures.settings.params = '"menu_id": "' + ID + '"';
                module.dsBrowseFeatures.Reload();
                module.showDeleteBtn();
                module.showTab('features');
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

    // Browse Features
    module.myFeatureColumns = [{ label: 'Menu Item', width: '35%', field: 'menu_item_name' },
                               { label: 'Feature Name', width: '35%', field: 'feature_name' },
                               { label: 'Sort Order', width: '10%', field: 'sort_order' },
                               { label: 'Access Mode', width: '15%', field: 'menu_mode_name' },
                               { label: '', width: '5%', type: 'remove', class: 'delete'}];

    module.myFeatureFilters = [{ name: 'myMenuName', field: 'menu_item', label: 'Menu Item', criteria: 'contains' },
                               { name: 'myFeatureName', field: 'feature_name', label: 'Feature Name', criteria: 'contains'}];

    module.featureGrid = new spaGrid({
        form: module,
        id: 'featureGrid',
        title: "Assigned Menu Items",
        columns: module.myFeatureColumns,
        filters: module.myFeatureFilters,
        rowID: 'feature_id',
        height: 210,
        newBtn: 'Assign Menu Item',
        newLocation: function () {
            module.showModal('featureModal');
        },
        onRemove: function (id) {
            $.ajax({
                type: "POST",
                contentType: "application/json",
                url: ROOT + "/services/sys_menu_Services.asmx/RemoveMenuItem",
                data: "{ 'menu_id': '" + ID + "', 'feature_id': '" + id + "' }",
                dataType: "json",
                success: function (myResponse) {
                    if (myResponse.result == true) {
                        module.dsBrowseFeatures.Reload();
                    } else {
                        ShowSystemModal('Application Error', 'Message: ' + myResponse.message);
                    }
                },
                error: ajaxFailed
            });
        }
    });

    module.dsBrowseFeatures = new spaDataset({
        form: module,
        id: 'dsBrowseFeatures',
        pk: 'feature_id',
        webservice: '/services/sys_menu_Services.asmx/GetAllByMenuAssigned',
        params: '"menu_id": "' + ID + '"',
        pageSize: 30
    });

    // SELECT Feature
    module.mySelectFeatureColumns = [{ label: 'Feature Name', width: '50%', field: 'feature_name', class: 'key'}];

    module.mySelectFeatureFilters = [{ name: 'myName', field: 'feature_name', label: 'Feature Name', criteria: 'contains'}];

    module.selectFeatureGrid = new spaGrid({
        form: module,
        id: 'selectFeatureGrid',
        title: "Select Menu Item",
        columns: module.mySelectFeatureColumns,
        filters: module.mySelectFeatureFilters,
        rowID: 'feature_id',
        height: 210,
        onSelect: function (id) {
            var menu_item = module.getValue("menu_item");
            var sort_order = module.getValue("item_sort_order");
            var menu_mode = module.getValue("menu_mode");

            if (menu_item == "") {
                menu_item = module.dsSelectFeature.GetCurrentRow().feature_name;
            }

            $.ajax({
                type: "POST",
                contentType: "application/json",
                url: ROOT + "/services/sys_menu_Services.asmx/AssignFeatureToMenu",
                data: "{ 'menu_id': '" + ID + "', 'feature_id': '" + id + "', 'menu_item': '" + menu_item + "', 'sort_order': '" + sort_order + "', 'menu_mode': '" + menu_mode + "' }",
                dataType: "json",
                success: function (myResponse) {
                    if (myResponse.result == true) {
                        module.dsBrowseFeatures.Reload();
                        module.hideModal('featureModal');
                    } else {
                        ShowSystemModal('Application Error', 'Message: ' + myResponse.message);
                    }
                },
                error: ajaxFailed
            });
        }
    });

    module.dsSelectFeature = new spaDataset({
        form: module,
        id: 'dsSelectFeature',
        pk: 'feature_id',
        webservice: '/services/sys_menu_Services.asmx/GetAllByMenuUnAssigned',
        params: '"menu_id": "' + ID + '"',
        pageSize: 30
    });


}





function init(module, params) {

    var ID = EMPTY_GUID;

    if (params.id != EMPTY_GUID) {
        ID = params.id;
    } else {
        module.hideDeleteBtn();
        module.hideTab('features');
    }


    module.dsDetail = new spaDataset({
        form: module,
        id: 'dsDetail',
        table: 'sys_application',
        pk: 'application_id',
        webservice: '/services/sys_feature_Services.asmx/GetApplicationByID',
        params: '"id": "' + ID + '"',
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
                module.dsBrowseFeatures.settings.params = '"application_id": "' + ID + '"';
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
    module.myFeatureColumns = [{ label: 'Feature', width: '50%', field: 'feature_name', class: 'key', link: '/portal/sys-application/feature.htm', menu: 'Edit Feature: %%' },
                               { label: 'Moniker', width: '50%', field: 'moniker' }];

    module.myFeatureFilters = [{ name: 'myFeatureName', field: 'feature_name', label: 'Feature Name', criteria: 'contains'}];

    module.featureGrid = new spaGrid({
        form: module,
        id: 'featureGrid',
        title: "Features",
        columns: module.myFeatureColumns,
        filters: module.myFeatureFilters,
        rowID: 'feature_id',
        height: 270,
        newBtn: 'Add Feature',
        newLocation: function () {
            createModule('Add Feature', '/portal/sys-application/feature.htm', { 'id': EMPTY_GUID, 'application_id': ID });
        }
    });

    module.dsBrowseFeatures = new spaDataset({
        form: module,
        id: 'dsBrowseFeatures',
        pk: 'client_feature_id',
        webservice: '/services/sys_feature_Services.asmx/GetAllApplicationFeatures',
        params: '"application_id": "' + ID + '"',
        pageSize: 30
    });

    module.refresh = function () {
        module.dsBrowseFeatures.Reload();
    };

}





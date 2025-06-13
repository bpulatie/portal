function init(module, params) {

    var ID = EMPTY_GUID;

    if (params.id != EMPTY_GUID) {
        ID = params.id;
    } else {
        module.hideDeleteBtn();
    }


    module.dsDetail = new spaDataset({
        form: module,
        id: 'dsDetail',
        table: 'sys_value_group',
        pk: 'group_id',
        webservice: '/services/sys_value_Services.asmx/GetGroupByID',
        params: '"group_id": "' + ID + '"',
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
                module.dsBrowseValues.settings.params = '"group_id": "' + ID + '"';
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

    // Browse Values
    module.myValueColumns = [{ label: 'Code', width: '20%', field: 'value_code' },
                             { label: 'Value', width: '65%', field: 'value_text', class: 'key' },
                             { label: 'Sort Order', width: '10%', field: 'sort_order' },
                             { label: '', width: '5%', type: 'remove', class: 'delete'}];

    module.myValueFilters = [{ name: 'myCode', field: 'value_code', label: 'Code', criteria: 'contains' }];

    module.valueGrid = new spaGrid({
        form: module,
        id: 'valueGrid',
        title: "Valid Values",
        columns: module.myValueColumns,
        filters: module.myValueFilters,
        rowID: 'value_id',
        height: 310,
        newBtn: 'Add Value',
        newLocation: function () {
            module.dsDetailValue.AddRow();
            module.dsDetailValue.EditColumn("group_id", ID);
            module.showModal('valueModal');
        },
        onSelect: function (id) {
            module.dsDetailValue.settings.params = '"value_id": "' + id + '"';
            module.dsDetailValue.Reload();
            module.showModal('valueModal');
        },
        onRemove: function (id) {
            $.ajax({
                type: "POST",
                contentType: "application/json",
                url: ROOT + "/services/sys_value_Services.asmx/RemoveValueFromGroup",
                data: "{ 'value_id': '" + id + "' }",
                dataType: "json",
                success: function (myResponse) {
                    if (myResponse.result == true) {
                        module.dsBrowseValues.Reload();
                    } else {
                        ShowSystemModal('Application Error', 'Message: ' + myResponse.message);
                    }
                },
                error: ajaxFailed
            });
        }
    });

    module.dsBrowseValues = new spaDataset({
        form: module,
        id: 'dsBrowseValues',
        table: 'sys_value',
        pk: 'value_id',
        webservice: '/services/sys_value_Services.asmx/GetAllGroupValues',
        params: '"group_id": "' + ID + '"',
        pageSize: 30
    });


    module.dsDetailValue = new spaDataset({
        form: module,
        id: 'dsDetailValue',
        table: 'sys_value',
        pk: 'value_id',
        webservice: '/services/sys_value_Services.asmx/GetValueByID',
        params: '"value_id": "' + EMPTY_GUID + '"'
    });

    //Modal Actions
    module.modalSaveBtn_onclick = function () {
        var row = module.dsDetailValue.GetCurrentRow();

        if (row.value_code === "") {
            DisplayModalMessage(module, 'Please enter a Code');
            return;
        }

        if (row.value_text == null) {
            DisplayModalMessage(module, 'Please select a Value');
            return;
        }

        module.dsDetailValue.Save(function () {
            module.dsBrowseValues.Reload();
            module.hideModal('valueModal');
        });

    }
}





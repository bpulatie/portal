function init(module, params) {

    var ID = EMPTY_GUID;

    if (params.id != EMPTY_GUID) {
        ID = params.id;
    }

    module.aDataTypes = new Array(['a', 'Alphanumeric'], ['n', 'Numeric'], ['d', 'Date']);
    module.addOptions('data_type', module.aDataTypes);

    module.dsDetail = new spaDataset({
        form: module,
        id: 'dsDetail',
        table: 'async_task',
        pk: 'task_id',
        webservice: '/services/async_task_Services.asmx/GetByID',
        params: '"task_id": "' + ID + '"',
        onLoad: function () {
            if (ID == EMPTY_GUID) {
                module.dsDetail.AddRow();
            }
        }
    });

    // Button Actions
    module.saveBtn_onclick = function () {
        if (module.validateForm() == true) {
            module.dsDetail.Save(function () {
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

    module.validateForm = function () {
        return true;
    }

    // Browse Parameters
    module.myParamColumns = [{ label: 'Parameter Name', width: '35%', field: 'parameter_name' },
                             { label: 'Data Type', width: '30%', field: 'data_type_name' },
                             { label: 'Required', width: '30%', field: 'required_name' },
                             { label: '', width: '5%', type: 'remove', class: 'delete'}];

    module.myParamFilters = [{ name: 'myParamName', field: 'param_name', label: 'Parameter Name', criteria: 'contains' }];

    module.paramGrid = new spaGrid({
        form: module,
        id: 'paramGrid',
        title: "Task Parameters",
        columns: module.myParamColumns,
        filters: module.myParamFilters,
        rowID: 'parameter_id',
        height: 210,
        newBtn: 'Add Parameter',
        newLocation: function () {
            module.showModal('parameterModal');
        },
        onRemove: function (id) {
            $.ajax({
                type: "POST",
                contentType: "application/json",
                url: ROOT + "/services/async_task_Services.asmx/RemoveParameterFromTask",
                data: "{ 'parameter_id': '" + id + "' }",
                dataType: "json",
                success: function (myResponse) {
                    if (myResponse.result == true) {
                        module.dsBrowseParameters.Reload();
                    } else {
                        ShowSystemModal('Application Error', 'Message: ' + myResponse.message);
                    }
                },
                error: ajaxFailed
            });
        }
    });

    module.dsBrowseParameters = new spaDataset({
        form: module,
        id: 'dsBrowseParameters',
        pk: 'parameter_id',
        webservice: '/services/async_task_Services.asmx/GetAllParametersByTask',
        params: '"task_id": "' + ID + '"',
        pageSize: 30
    });

    //Modal Actions
    module.modalSaveBtn_onclick = function () {
        if (module.getValue("parameter_name") == "") {
            DisplayMessage(module, 'Please enter a Parameter Name');
        }

        $.ajax({
            type: "POST",
            contentType: "application/json",
            url: ROOT + "/services/async_task_Services.asmx/AddParameterToTask",
            data: "{ 'task_id': '" + ID + "', 'parameter_name': '" + module.getValue("parameter_name") + "', 'data_type': '" + module.getValue("data_type") + "', 'required': '" + module.getValue("required") + "' }",
            dataType: "json",
            success: function (myResponse) {
                if (myResponse.result == true) {
                    module.dsBrowseParameters.Reload();
                    module.hideModal('parameterModal');
                } else {
                    ShowSystemModal('Application Error', 'Message: ' + myResponse.message);
                }
            },
            error: ajaxFailed
        });
    }


}









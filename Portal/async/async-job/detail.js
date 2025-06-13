function init(module, params) {

    var ID = EMPTY_GUID;

    if (params.id != EMPTY_GUID) {
        ID = params.id;
    } else {
        module.hideDeleteBtn();
        module.hide("runBtn");
        module.hideTab('tasks');
        module.hideTab('parameters');
    }

    module.setValue("sort_order", 0);
    module.aSchedule = new Array(['o', 'On Demand'], ['s', 'Scheduled']);
    module.addOptions('schedule_code', module.aSchedule);

    module.dsDetail = new spaDataset({
        form: module,
        id: 'dsDetail',
        table: 'async_job',
        pk: 'job_id',
        webservice: '/services/async_job_Services.asmx/GetByID',
        params: '"job_id": "' + ID + '"',
        onLoad: function () {
            if (ID == EMPTY_GUID) {
                module.dsDetail.AddRow();
            }
        }
    });

    // Button Actions
    module.saveBtn_onclick = function () {
        if (module.validateForm() == true) {
            module.dsDetail.Save(function (id) {
                if (ID == EMPTY_GUID) {
                    ID = id;
                    module.dsBrowseTasks.settings.params = '"job_id": "' + ID + '"';
                    module.showTab('tasks');
                    module.showTab('parameters');
                    module.show("runBtn");
                    module.showDeleteBtn();
                }
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

    module.runBtn_onclick = function () {
        $.ajax({
            type: "POST",
            contentType: "application/json",
            url: ROOT + "/services/async_queue_Services.asmx/QueueJob",
            data: "{ 'job_id': '" + ID + "' }",
            dataType: "json",
            success: function (myResponse) {
                if (myResponse.result == true) {
                    ShowSystemModal('Async Queue:', 'Job has been queued');
                } else {
                    ShowSystemModal('Application Error', 'Message: ' + myResponse.message);
                }
            },
            error: ajaxFailed
        });
    }


    // Browse Tasks
    module.myTaskColumns = [{ label: 'Task Name', width: '25%', field: 'task_name' },
                            { label: 'Moniker', width: '60%', field: 'moniker' },
                            { label: 'Sort Order', width: '10%', field: 'sort_order' },
                            { label: '', width: '5%', type: 'remove', class: 'delete'}];

    module.myTaskFilters = [{ name: 'myTaskName', field: 'task_name', label: 'Task Name', criteria: 'contains' },
                            { name: 'myMoniker', field: 'moniker', label: 'Moniker', criteria: 'contains'}];

    module.taskGrid = new spaGrid({
        form: module,
        id: 'taskGrid',
        title: "Async Tasks",
        columns: module.myTaskColumns,
        filters: module.myTaskFilters,
        rowID: 'job_task_id',
        height: 210,
        newBtn: 'Select Task',
        newLocation: function () {
            module.showModal('taskModal');
        },
        onRemove: function (id) {
            $.ajax({
                type: "POST",
                contentType: "application/json",
                url: ROOT + "/services/async_task_Services.asmx/RemoveTaskFromJob",
                data: "{ 'job_task_id': '" + id + "' }",
                dataType: "json",
                success: function (myResponse) {
                    if (myResponse.result == true) {
                        module.dsBrowseTasks.Reload();
                    } else {
                        ShowSystemModal('Application Error', 'Message: ' + myResponse.message);
                    }
                },
                error: ajaxFailed
            });
        }
    });

    module.dsBrowseTasks = new spaDataset({
        form: module,
        id: 'dsBrowseTasks',
        pk: 'job_id',
        webservice: '/services/async_task_Services.asmx/GetAllByJob',
        params: '"job_id": "' + ID + '"',
        pageSize: 30
    });

    // SELECT Task
    module.mySelectTaskColumns = [{ label: 'Task Name', width: '50%', field: 'task_name', class: 'key' },
                                  { label: 'Moniker', width: '50%', field: 'moniker'      }];

    module.mySelectTaskFilters = [{ name: 'myName', field: 'task_name', label: 'Task Name', criteria: 'contains' }, 
                                  { name: 'myMon', field: 'moniker', label: 'Moniker', criteria: 'contains' }];

    module.selectTaskGrid = new spaGrid({
        form: module,
        id: 'selectTaskGrid',
        title: "Select Task",
        columns: module.mySelectTaskColumns,
        filters: module.mySelectTaskFilters,
        rowID: 'task_id',
        height: 210,
        onSelect: function (id) {
            var sort_order = module.getValue("sort_order");
            if (sort_order == "")
                sort_order = 0;

            $.ajax({
                type: "POST",
                contentType: "application/json",
                url: ROOT + "/services/async_task_Services.asmx/AddTaskToJob",
                data: "{ 'job_id': '" + ID + "', 'task_id': '" + id + "', 'sort_order': '" + sort_order + "' }",
                dataType: "json",
                success: function (myResponse) {
                    if (myResponse.result == true) {
                        module.dsBrowseTasks.Reload();
                    } else {
                        ShowSystemModal('Application Error', 'Message: ' + myResponse.message);
                    }
                },
                error: ajaxFailed
            });
        }
    });

    module.dsSelectTask = new spaDataset({
        form: module,
        id: 'dsSelectTask',
        pk: 'task_id',
        webservice: '/services/async_task_Services.asmx/GetAll',
        pageSize: 30
    });


    // Parameters
    module.myParamColumns = [{ label: 'Parameter Name', width: '25%', field: 'parameter_name' },
                            { label: 'Data Type', width: '20%', field: 'data_type_name' },
                            { label: 'Required', width: '20%', field: 'required_name' },
                            { label: 'Value', width: '35%', field: 'value' }];

    module.myParamFilters = [{ name: 'myParamName', field: 'parameter_name', label: 'Parameter Name', criteria: 'contains'}];
                            

    module.paramGrid = new spaGrid({
        form: module,
        id: 'paramGrid',
        title: "Job Parameters",
        columns: module.myParamColumns,
        filters: module.myParamFilters,
        rowID: 'job_task_id',
        height: 210
    });

    module.dsJobParameters = new spaDataset({
        form: module,
        id: 'dsJobParameters',
        pk: 'job_id',
        webservice: '/services/async_job_Services.asmx/GetAllParametersByJob',
        params: '"job_id": "' + ID + '"',
        pageSize: 30
    });



}









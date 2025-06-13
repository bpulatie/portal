function init(module, params) {
    
    var ID = EMPTY_GUID;

    if (params.id != EMPTY_GUID) {
        ID = params.id;
    } else {
        module.hideDeleteBtn();
    }

    module.aPayCodes = new Array();

    module.dsDetail = new spaDataset({
        form: module,
        id: 'dsDetail',
        table: 'pay_employee',
        pk: 'employee_id',
        webservice: '/services/pay_employee_Services.asmx/GetByID',
        params: '"employee_id": "' + ID + '"'
    });

    module.dsEarningCodes = new spaDataset({
        form: module,
        id: 'dsEarningCodes',
        table: 'pay_code',
        pk: 'pay_code_id',
        webservice: '/services/pay_detail_Services.asmx/GetAllPayCodes',
        onLoad: function () {

            module.aPayCodes.push(['', 'Show All']);

            for (var x = 0; x < module.dsEarningCodes.RowCount(); x++) {
                var row = module.dsEarningCodes.GetRow(x);
                module.aPayCodes.push([row.source_code, row.target_code]);
            }

            // Browse
            module.myColumns = [{ label: 'Site Code', width: '10%', field: 'site_code' },
                                { label: 'Job Code', width: '10%', field: 'job_code' },
                                { label: 'Job Name', width: '20%', field: 'job_name' },
                                { label: 'Earning Code', width: '10%', field: 'target_code' },
                                { label: 'Description', width: '20%', field: 'description' },
                                { label: 'Hours', width: '10%', field: 'earning_hours', type: 'numeric', format: 'money' },
                                { label: 'Rate', width: '10%', field: 'rate', type: 'numeric', format: 'money' },
                                { label: 'Amount', width: '10%', field: 'earning_amount', type: 'numeric', format: 'money'}];

            module.myFilters = [{ name: 'myLastName', field: 'last_name', label: 'Last Name', criteria: 'contains' },
                                { name: 'myFirstName', field: 'first_name', label: 'First Name', criteria: 'contains' },
                                { name: 'myEarnCode', field: 'earning_code', label: 'Earning Code', type: 'array', values: module.aPayCodes, criteria: 'equals'}];

            module.myGrid = new spaGrid({
                form: module,
                id: 'myGrid',
                title: "Pay History",
                columns: module.myColumns,
                filters: module.myFilters,
                rowID: 'pay_period_id',
                height: 250
            });

            module.dsBrowse = new spaDataset({
                form: module,
                id: 'dsBrowse',
                webservice: '/services/pay_detail_Services.asmx/GetAllByEmployee',
                params: '"employee_id": "' + ID + '"',
                pageSize: 30
            });
        }
    });

    // Employee History
    module.employeeHistoryColumns = [{ label: 'Date Modified', width: '10%', field: 'modified_date', type: 'date', format: 'date' },
                                     { label: 'Site', width: '10%', field: 'bu_code' },
                                     { label: 'Job', width: '20%', field: 'job_name' },
                                     { label: 'Job Code', width: '10%', field: 'job_code' },
                                     { label: 'User Level', width: '20%', field: 'user_level' },
                                     { label: 'Pay Rate', width: '10%', field: 'pay_rate', type: 'numeric', format: 'money', accessFeature: 'AllowViewPayRates' },
                                     { label: 'Security Level', width: '10%', field: 'security_level' },
                                     { label: 'Access Level', width: '10%', field: 'access_level' }];

    module.employeeHistoryFilters = [{ name: 'myDate', field: 'modified_date', label: 'Date Modified Before', type: 'date', format: 'date', criteria: 'lessthan'}];

    module.employeeHistoryGrid = new spaGrid({
        form: module,
        id: 'employeeHistoryGrid',
        title: "Employee History",
        columns: module.employeeHistoryColumns,
        filters: module.employeeHistoryFilters,
        rowID: 'employee_history_id',
        height: 250
    });

    module.dsEmployeeHistory = new spaDataset({
        form: module,
        id: 'dsEmployeeHistory',
        webservice: '/services/pay_employee_Services.asmx/GetAllHistoryForEmployee',
        params: '"employee_id": "' + ID + '"',
        pageSize: 30
    });

    // Actions
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

    // Actions
    module.queueBtn_onclick = function () {
        $.ajax({
            type: "POST",
            contentType: "application/json",
            url: ROOT + "/services/pay_employee_Services.asmx/queueEmployeeUpdate",
            data: '{ "employee_id": "' + ID + '" }',
            dataType: "json",
            success: function (myResponse) {
                if (myResponse.result == true) {
                    DisplayMessage(module, 'Employee Update Request Submitted');
                } else {
                    ShowSystemModal('Application Error', 'Message: ' + myResponse.message);
                }
            },
            error: ajaxFailed
        });
    }

    module.triggerBtn_onclick = function () {
        $.ajax({
            type: "POST",
            contentType: "application/json",
            url: ROOT + "/services/pay_employee_Services.asmx/triggerEmployeeUpdate",
            data: '{ "employee_id": "' + ID + '" }',
            dataType: "json",
            success: function (myResponse) {
                if (myResponse.result == true) {
                    DisplayMessage(module, 'Employee Update Submitted');
                } else {
                    ShowSystemModal('Application Error', 'Message: ' + myResponse.message);
                }
            },
            error: ajaxFailed
        });
    }


}



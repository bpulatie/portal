function init(module, params) {

    var ID = EMPTY_GUID;
    var PSID = 'ALL';
    var PGID = '';

    if (params.id != EMPTY_GUID) {
        ID = params.id;
    }

    module.aUpdateStatus = new Array(['o', 'Open'], ['p', 'Pending'], ['c', 'Closed'], ['k', 'Pass'], ['l', 'Fail']);
    module.addOptions('status_code', module.aUpdateStatus);

    module.dsReasons = new spaDataset({
        form: module,
        id: 'dsReasons',
        table: 'sys_reason',
        pk: 'reason_id',
        webservice: '/services/sys_reason_Services.asmx/GetAllPayException',
        onLoad: function () {
            module.aReasons = new Array();

            for (var x = 0; x < module.dsReasons.RowCount(); x++) {
                var row = module.dsReasons.GetRow(x);
                module.aReasons.push([row.reason_id, row.reason_code]);
            }

            module.addOptions('reason_code', module.aReasons);
        }
    });

    module.dsGroups = new spaDataset({
        form: module,
        id: 'dsGroups',
        table: 'pay_group',
        pk: 'pay_group_id',
        webservice: '/services/pay_group_Services.asmx/GetAllInPayPeriod',
        params: '"pay_period_id": "' + ID + '"',
        pageSize: 30,
        onLoad: function () {
            module.aGroups = new Array();
            module.aGroups.push(['', 'All Pay Groups']);

            for (var x = 0; x < module.dsGroups.RowCount(); x++) {
                var row = module.dsGroups.GetRow(x);
                module.aGroups.push([row.pay_group_id, row.pay_group_code]);
            }

            module.addOptions('pay_group_code', module.aGroups);

            $('#' + module.module_id + "_pay_group_code_hidden").change(function () {
                PGID = module.getValue("pay_group_code");
                module.displaySaveBtn();
            });

            module.setValue('pay_group_code', '');
        }
    });

    module.dsDetail = new spaDataset({
        form: module,
        id: 'dsDetail',
        table: 'pay_period',
        pk: 'pay_period_id',
        webservice: '/services/pay_period_Services.asmx/GetByID',
        params: '"id": "' + ID + '"',
        onLoad: function () {
            if (ID == EMPTY_GUID) {
                module.dsDetail.AddRow();
            }
        }
    });

    // Pay Browse
    module.aPayCodes = new Array();

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


            module.myColumns = [{ label: 'Location', width: '8%', field: 'site_code' },
                        { label: 'Last Name', width: '15%', field: 'last_name' },
                        { label: 'First Name', width: '12%', field: 'first_name' },
                        { label: 'Job Code', width: '8%', field: 'job_code' },
                        { label: 'Job Name', width: '17%', field: 'job_name' },
                        { label: 'Earning Code', width: '10%', field: 'target_code' },
                        { label: 'Description', width: '15%', field: 'description' },
                        { label: 'Hours', width: '5%', field: 'earning_hours', type: 'numeric', format: 'money' },
                        { label: 'Rate', width: '5%', field: 'rate', type: 'numeric', format: 'money' },
                        { label: 'Amount', width: '5%', field: 'earning_amount', type: 'numeric', format: 'money'}];

            module.myFilters = [{ name: 'myLastName', field: 'last_name', label: 'Last Name', criteria: 'contains' },
                        { name: 'myFirstName', field: 'first_name', label: 'First Name', criteria: 'contains' },
                        { name: 'myEarnCode', field: 'earning_code', label: 'Earning Code', type: 'array', values: module.aPayCodes, criteria: 'equals'}];

            module.myGrid = new spaGrid({
                form: module,
                id: 'myGrid',
                title: "Employee Pay",
                columns: module.myColumns,
                filters: module.myFilters,
                rowID: 'employee_id',
                height: 280
            });

        }

    });

    // Pay Summary
    module.summaryColumns = [{ label: 'Location', width: '10%', field: 'site_code' },
                             { label: 'Reg Hrs', width: '6%', field: 'reg_hours', type: 'numeric', format: 'money' },
                             { label: 'OT Hrs', width: '6%', field: 'ot_hours', type: 'numeric', format: 'money' },
                             { label: 'DBT Hrs', width: '6%', field: 'dbltm_hours', type: 'numeric', format: 'money' },
                             { label: 'Tip Amt', width: '6%', field: 'tip_amount', type: 'numeric', format: 'money' },
                             { label: 'Hday Hrs', width: '6%', field: 'hday_hours', type: 'numeric', format: 'money' },
                             { label: 'Rpt Hrs', width: '6%', field: 'rptme_hours', type: 'numeric', format: 'money' },
                             { label: 'Rpt Pay', width: '6%', field: 'rptme_amount', type: 'numeric', format: 'money' },
                             { label: 'SS Hrs', width: '6%', field: 'spsft_hours', type: 'numeric', format: 'money' },
                             { label: 'SS Pay', width: '6%', field: 'spsft_amount', type: 'numeric', format: 'money' },
                             { label: 'MB Hrs', width: '6%', field: 'mlbrk_hours', type: 'numeric', format: 'money' },
                             { label: 'Jury Hrs', width: '6%', field: 'jury_hours', type: 'numeric', format: 'money' },
                             { label: 'Bmt Hrs', width: '6%', field: 'breav_hours', type: 'numeric', format: 'money' },
                             { label: 'Oth Hrs', width: '6%', field: 'other_hours', type: 'numeric', format: 'money' },
                             { label: 'Tot Hrs', width: '6%', field: 'total_hours', type: 'numeric', format: 'money' },
                             { label: 'Tot Pay', width: '6%', field: 'total_amount', type: 'numeric', format: 'money'}]

    module.aManager = new Array(['', 'All Employees'], ['y', 'Hourly Team Member'], ['n', 'Hourly Manager']);

    module.summaryFilters = [{ name: 'myManager', field: 'include_in_mgr_group', label: 'Employee Type', type: 'array', values: module.aManager, criteria: 'equals'}];




    module.summaryGrid = new spaGrid({
        form: module,
        id: 'summaryGrid',
        title: "Pay Summary",
        columns: module.summaryColumns,
        filters: module.summaryFilters,
        rowID: 'employee_id',
        height: 280
    });


    // Exception Browse
    module.aStatus = new Array(['', 'All'], ['o', 'Open'], ['c', 'Closed'], ['p', 'Pending'], ['k', 'Pass'], ['l', 'Fail']);

    module.myColumns2 = [{ label: 'Location', width: '10%', field: 'site_code' },
                         { label: 'Last Name', width: '20%', field: 'last_name' },
                         { label: 'First Name', width: '15%', field: 'first_name' },
                         { label: 'Exception', width: '25%', field: 'exception_code_name' },
                         { label: 'Reason Code', width: '20%', field: 'reason_code' },
                         { label: 'Status', width: '10%', field: 'status_code_name', class: 'key'}];

    module.myFilters2 = [{ name: 'myLastName', field: 'last_name', label: 'Last Name', criteria: 'contains' },
                         { name: 'myFirstName', field: 'first_name', label: 'First Name', criteria: 'contains' },
                         { name: 'myStatus', field: 'd.status_code', label: 'Status', type: 'array', values: module.aStatus, index: '1', criteria: 'equals'}];

    module.myExGrid = new spaGrid({
        form: module,
        id: 'myExGrid',
        title: "Pay Exceptions",
        columns: module.myColumns2,
        filters: module.myFilters2,
        rowID: 'pay_exception_id',
        height: 280,
        onSelect: function (id) {
            module.dsException.settings.params = '"pay_exception_id": "' + id + '"';
            module.dsException.Reload();
            module.showModal('exceptionModal');
        }
    });

    var exFilter = module.myExGrid.GetFilter();

    module.dsException = new spaDataset({
        form: module,
        id: 'dsException',
        table: 'pay_exception',
        pk: 'pay_exception_id',
        webservice: '/services/pay_detail_Services.asmx/GetExceptionByID',
        params: '"pay_exception_id": "' + EMPTY_GUID + '"',
        pageSize: 30
    });


    module.dsPay = new spaDataset({
        form: module,
        id: 'dsPay',
        table: 'sys_user',
        pk: 'user_id',
        webservice: '/services/sys_user_Services.asmx/GetAllPaySpecialists',
        onLoad: function () {

            module.aSpecialists = new Array();

            var isSpecialist = false;
            for (var x = 0; x < module.dsPay.RowCount(); x++) {
                var row = module.dsPay.GetRow(x);
                if (row.user_id === CONTEXT.user.user_id) {
                    isSpecialist = true;
                }
                module.aSpecialists.push([row.user_id, row.first_name + " " + row.last_name]);
            }

            module.aSpecialists.push(['ALL', '[All Pay Specialists]']);

            module.addOptions('specialist_id', module.aSpecialists);

            if (isSpecialist == true) {
                module.setValue('specialist_id', CONTEXT.user.user_id);
                PSID = CONTEXT.user.user_id;
            } else {
                module.setValue('specialist_id', 'ALL');
            }

            var value = "Manage Pay Detail - " + $('#' + module.module_id + "_specialist_id").val() + ", " + $('#' + module.module_id + "_pay_group_code").val();
            module.setPanelTitle("topPanel", value);

            $('#' + module.module_id + "_specialist_id_hidden").change(function () {
                PSID = module.getValue('specialist_id');

                module.dsSummary.settings.params = '"psid": "' + PSID + '", "pgid": "' + PGID + '", "level": "1", "p1": "' + ID + '", "p2": "", "p3": "", "p4": "", "p5": ""';
                module.dsSummary.Reload();

                module.dsBrowse.settings.params = '"psid": "' + PSID + '", "pgid": "' + PGID + '", "pay_period_id": "' + ID + '"';
                module.dsBrowse.Reload();

                module.dsPaySummary.settings.params = '"psid": "' + PSID + '", "pgid": "' + PGID + '", "pay_period_id": "' + ID + '"';
                module.dsPaySummary.Reload();

                module.dsExBrowse.settings.params = '"psid": "' + PSID + '", "pay_period_id": "' + ID + '"';
                module.dsExBrowse.Reload();

                var value = "Manage Pay Detail - " + $('#' + module.module_id + "_specialist_id").val() + ", " + $('#' + module.module_id + "_pay_group_code").val();
                module.setPanelTitle("topPanel", value);
            });

            $('#' + module.module_id + "_pay_group_code_hidden").change(function () {
                PGID = module.getValue("pay_group_code");

                module.dsSummary.settings.params = '"psid": "' + PSID + '", "pgid": "' + PGID + '", "level": "1", "p1": "' + ID + '", "p2": "", "p3": "", "p4": "", "p5": ""';
                module.dsSummary.Reload();

                module.dsBrowse.settings.params = '"psid": "' + PSID + '", "pgid": "' + PGID + '", "pay_period_id": "' + ID + '"';
                module.dsBrowse.Reload();

                module.dsPaySummary.settings.params = '"psid": "' + PSID + '", "pgid": "' + PGID + '", "pay_period_id": "' + ID + '"';
                module.dsPaySummary.Reload();

                module.dsExBrowse.settings.params = '"psid": "' + PSID + '", "pay_period_id": "' + ID + '"';
                module.dsExBrowse.Reload();

                var value = "Manage Pay Detail - " + $('#' + module.module_id + "_specialist_id").val() + ", " + $('#' + module.module_id + "_pay_group_code").val();
                module.setPanelTitle("topPanel", value);
            });

            module.dsSummary = new spaDataset({
                form: module,
                id: 'dsSummary',
                webservice: '/services/pay_detail_Services.asmx/GetPayDetail',
                params: '"psid": "' + PSID + '", "pgid": "' + PGID + '",  "ppid": "' + ID + '", "level": "1", "p1": "", "p2": "", "p3": "", "p4": ""'
            });

            module.dsBrowse = new spaDataset({
                form: module,
                id: 'dsBrowse',
                webservice: '/services/pay_detail_Services.asmx/GetAllByPayPeriod',
                params: '"psid": "' + PSID + '", "pgid": "' + PGID + '", "pay_period_id": "' + ID + '"',
                pageSize: 30
            });

            module.dsPaySummary = new spaDataset({
                form: module,
                id: 'dsPaySummary',
                webservice: '/services/pay_detail_Services.asmx/GetPaySummary',
                params: '"psid": "' + PSID + '", "pgid": "' + PGID + '", "pay_period_id": "' + ID + '"',
                pageSize: 30
            });

            module.dsExBrowse = new spaDataset({
                form: module,
                id: 'dsExBrowse',
                webservice: '/services/pay_detail_Services.asmx/GetAllExceptionsByPayPeriod',
                params: '"psid": "' + PSID + '", "pay_period_id": "' + ID + '"',
                filter: exFilter,
                pageSize: 30
            });

        }
    });

    module.displaySaveBtn = function () {
        $.ajax({
            type: "POST",
            contentType: "application/json",
            url: ROOT + "/services/pay_period_Services.asmx/GetPayPeriodStatus",
            data: '{ "pay_period_id": "' + ID + '" }',
            dataType: "json",
            success: function (myResponse) {
                if (myResponse.result == true) {
                    if (myResponse.data == "ready" && PGID != "") {
                        $("#" + module.module_id + "_saveBtn").addClass('btn-success');
                        $("#" + module.module_id + "_saveBtn").removeClass('disabled');
                        $("#" + module.module_id + "_saveBtn").prop("disabled", false);
                        $("#" + module.module_id + "_reprocessBtn").addClass('btn-success');
                        $("#" + module.module_id + "_reprocessBtn").removeClass('disabled');
                        $("#" + module.module_id + "_reprocessBtn").prop("disabled", false);
                    } else {
                        $("#" + module.module_id + "_saveBtn").removeClass('btn-success');
                        $("#" + module.module_id + "_saveBtn").addClass('disabled');
                        $("#" + module.module_id + "_saveBtn").attr("disabled", "disabled");
                        $("#" + module.module_id + "_reprocessBtn").removeClass('btn-success');
                        $("#" + module.module_id + "_reprocessBtn").addClass('disabled');
                        $("#" + module.module_id + "_reprocessBtn").attr("disabled", "disabled");
                    }
                } else {
                    ShowSystemModal('Application Error', 'Message: ' + myResponse.message);
                }
            },
            error: ajaxFailed
        });

    }

    // Actions
    module.importBtn_onclick = function () {
        $.ajax({
            type: "POST",
            contentType: "application/json",
            url: ROOT + "/services/pay_period_Services.asmx/ImportPayData",
            data: '{ "pay_period_id": "' + ID + '", "user_id": "' + CONTEXT.user.user_id + '" }',
            dataType: "json",
            success: function (myResponse) {
                if (myResponse.result == true) {
                    DisplayMessage(module, 'Validation Import Request Submitted');
                    module.displaySaveBtn();
                    module.dsExBrowse.Reload();
                } else {
                    ShowSystemModal('Application Error', 'Message: ' + myResponse.message);
                }
            },
            error: ajaxFailed
        });
    }

    module.saveBtn_onclick = function () {
        $.ajax({
            type: "POST",
            contentType: "application/json",
            url: ROOT + "/services/pay_period_Services.asmx/GeneratePayFile",
            data: '{ "pay_period_id": "' + ID + '", "pay_group_id": "' + PGID + '", "user_id": "' + CONTEXT.user.user_id + '" }',
            dataType: "json",
            success: function (myResponse) {
                if (myResponse.result == true) {
                    DisplayMessage(module, 'Create Pay File Request Submitted');
                } else {
                    ShowSystemModal('Application Error', 'Message: ' + myResponse.message);
                }
            },
            error: ajaxFailed
        });
    }

    module.reprocessBtn_onclick = function () {
        $.ajax({
            type: "POST",
            contentType: "application/json",
            url: ROOT + "/services/pay_period_Services.asmx/ReprocessPayFile",
            data: '{ "pay_period_id": "' + ID + '", "pay_group_id": "' + PGID + '", "user_id": "' + CONTEXT.user.user_id + '" }',
            dataType: "json",
            success: function (myResponse) {
                if (myResponse.result == true) {
                    DisplayMessage(module, 'Reprocess Pay File Request Submitted');
                } else {
                    ShowSystemModal('Application Error', 'Message: ' + myResponse.message);
                }
            },
            error: ajaxFailed
        });
    }

    module.refresh = function () {
        module.dsPay.Reload();
    };

    //Modal Actions
    module.modalSaveBtn_onclick = function () {
        var row = module.dsException.GetCurrentRow();

        if (row.comment === "") {
            DisplayModalMessage(module, 'Please enter a comment');
            return;
        }

        if (row.reason_id == null) {
            DisplayModalMessage(module, 'Please select a Reason Code');
            return;
        }

        module.dsException.Save(function () {
            module.displaySaveBtn();
            module.dsExBrowse.Reload();
            module.hideModal('exceptionModal');
        });

    }

    module.modalSaveAllBtn_onclick = function () {
        var row = module.dsException.GetCurrentRow();

        if (row.comment === "") {
            DisplayModalMessage(module, 'Please enter a comment');
            return;
        }

        if (row.reason_id == null) {
            DisplayModalMessage(module, 'Please select a Reason Code');
            return;
        }

        $.ajax({
            type: "POST",
            contentType: "application/json",
            url: ROOT + "/services/pay_detail_Services.asmx/CloseAllExceptions",
            data: '{ "pay_period_id": "' + ID + '", "exception_code": "' + row.exception_code + '", "status_code": "' + row.status_code + '", "reason_id": "' + row.reason_id + '", "comment": "' + row.comment + '" }',
            dataType: "json",
            success: function (myResponse) {
                if (myResponse.result == true) {
                    module.dsExBrowse.Reload();
                } else {
                    ShowSystemModal('Application Error', 'Message: ' + myResponse.message);
                }
            },
            error: ajaxFailed
        });


        module.hideModal('exceptionModal');
    };

    module.displaySaveBtn();

    module.printBtn_onclick = function () {
        var filter;
        if (module.dsPaySummary.settings.filter === null) {
            filter = '{ "filter": [] }';
        } else {
            filter = '{ "filter": ' + module.dsPaySummary.settings.filter + '}';
        }
        window.open(ROOT + "/handlers/GetPdfReport.ashx?user_id=" + PSID + "&pgid=" + PGID + "&id=" + ID + "&filter=" + filter, "_blank");
    }

    module.printExceptionBtn_onclick = function () {
        var filter;
        if (module.dsExBrowse.settings.filter === null) {
            filter = '{ "filter": [] }';
        } else {
            filter = '{ "filter": ' + module.dsExBrowse.settings.filter + '}';
        }
        window.open(ROOT + "/handlers/GetExceptionReport.ashx?user_id=" + PSID + "&pgid=" + PGID + "&id=" + ID + "&filter=" + filter, "_blank");
    }

    // Tree Grid
    module.mySummaryColumns = [{ label: 'Location', width: '5%', field: 'site_code', level: "1" },
                                { label: 'Manager', width: '15%', field: 'manager', level: "2" },
                                { label: 'Employee Name', width: '15%', field: 'username', level: "3" },
                                { label: 'Business Date', width: '15%', field: 'charge_date', type: 'date', format: 'date', level: "4" },
                                { label: 'Pay Code', width: '10%', field: 'target_code' },
                                { label: 'Description', width: '15%', field: 'description' },
                                { label: 'Rate', width: '5%', field: 'rate', type: 'numeric', format: 'money' },
                                { label: 'Hours', width: '10%', field: 'earning_hours', type: 'numeric', format: 'money' },
                                { label: 'Amount', width: '10%', field: 'earning_amount', type: 'numeric', format: 'money'}];

    module.summaryTreeGrid = new spaTreeGrid({
        form: module,
        id: 'summaryTreeGrid',
        title: "Pay Summary Detail",
        columns: module.mySummaryColumns,
        rowID: 'employee_id',
        height: 380,
        onExpand: function (g) {
            var id = $(this).attr('level');

            if ($("#" + id)[0].childElementCount < 1) {

                var row = jQuery.parseJSON($(this).parent().parent().attr("row"));

                level = parseInt(row.level, 10) + 1;
                var params = '"psid": "", "pgid": "' + PGID + '", "ppid": "' + ID + '", "level": "' + level + '", "p1": "' + row['p1'] + '", "p2": "' + row['p2'] + '", "p3": "' + row['p3'] + '", "p4": "' + row['p4'] + '"';

                module.dsSummary.settings.params = params;
                module.dsSummary.Reload();
            }

            $("#" + id).show();
            $(this).removeClass('glyphicon-triangle-right');
            $(this).addClass('glyphicon-triangle-bottom');

            $('.spaTreeGrid.glyphicon-triangle-right', '#' + g.module_id).unbind('click');
            $('.spaTreeGrid.glyphicon-triangle-bottom', '#' + g.module_id).unbind('click');

            if (typeof g.settings.onExpand === 'function') {
                $('.spaTreeGrid.glyphicon-triangle-right', '#' + g.module_id).on('click', $.proxy(g.settings.onExpand, null, g));
            }

            if (typeof g.settings.onCollapse === 'function') {
                $('.spaTreeGrid.glyphicon-triangle-bottom', '#' + g.module_id).on('click', $.proxy(g.settings.onCollapse, null, g));
            }

        },
        onCollapse: function (g) {
            var id = $(this).attr('level');
            $("#" + id).hide();
            $(this).removeClass('glyphicon-triangle-bottom');
            $(this).addClass('glyphicon-triangle-right');

            $('.spaTreeGrid.glyphicon-triangle-right', '#' + g.module_id).unbind('click');
            $('.spaTreeGrid.glyphicon-triangle-bottom', '#' + g.module_id).unbind('click');

            if (typeof g.settings.onExpand === 'function') {
                $('.spaTreeGrid.glyphicon-triangle-right', '#' + g.module_id).on('click', $.proxy(g.settings.onExpand, null, g));
            }

            if (typeof g.settings.onCollapse === 'function') {
                $('.spaTreeGrid.glyphicon-triangle-bottom', '#' + g.module_id).on('click', $.proxy(g.settings.onCollapse, null, g));
            }
        }
    });




}


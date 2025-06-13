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
        table: 'sys_option_client',
        pk: 'option_id',
        webservice: '/services/sys_option_Services.asmx/GetByIDClient',
        params: '"option_id": "' + ID + '"',
        onLoad: function () {
            if (ID == EMPTY_GUID) {
                module.dsDetail.AddRow();
            }
        }
    });

    module.saveBtn_onclick = function () {
        var row = module.dsDetail.GetCurrentRow();

        $.ajax({
            type: "POST",
            contentType: "application/json",
            url: ROOT + "/services/sys_option_Services.asmx/SaveOptionValue",
            data: "{ 'option_id': '" + ID + "', 'option_value': '" + row.option_value + "' }",
            dataType: "json",
            success: function (myResponse) {
                if (myResponse.result == true) {
                    DisplayMessage(module, 'Save Successful');
                } else {
                    ShowSystemModal('Application Error', 'Message: ' + myResponse.message);
                }
            },
            error: ajaxFailed
        });
    }

    module.closeBtn_onclick = function () {
        module.closeModule();
        return false;
    }

}





function init(module, params) {

    var ID = EMPTY_GUID;

    if (params.id != EMPTY_GUID) {
        ID = params.id;
    } else {
        module.hideDeleteBtn();
    }

    module.aStatus = new Array(['o', 'Open'], ['c', 'Closed']);

    module.addOptions('status_code', module.aStatus);

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
        if ($("#" + module.module_id + "_week_no").val() === "") {
            DisplayMessage(module, 'Please enter a Week No');
            return false;
        }
        return true;
    }

}


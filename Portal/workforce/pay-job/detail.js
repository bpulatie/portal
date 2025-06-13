function init(module, params) {

    var ID = EMPTY_GUID;

    if (params.id != EMPTY_GUID) {
        ID = params.id;
    } else {
        module.hideDeleteBtn();
    }

    module.aCompany = new Array(['1', '1 - PF Changs'], ['2', '2 - PeiWei']);
    module.aTips = new Array(['0', '0 - zero'], ['1', '1 - one'], ['2', '2 - two']);
    module.aSecurity = new Array(['0', '0 - zero'], ['1', '1 - one'], ['3', '3 - three']);
    module.aAccess = new Array(['0', '0 - zero'],
                               ['2', '2 - two'],
                               ['3', '3 - three'],
                               ['4', '4 - four'],
                               ['10', '10 - ten'],
                               ['12', '12 - twelve'],
                               ['15', '15 - fifteen'],
                               ['18', '18 - eighteen'],
                               ['20', '20 - twenty'],
                               ['30', '30 - thirty'],
                               ['40', '40 - fourty'],
                               ['50', '50 - fifty']);

    module.addOptions('company', module.aCompany);
    module.addOptions('tip_status', module.aTips);
    module.addOptions('security_level', module.aSecurity);
    module.addOptions('access_level', module.aAccess);

    module.dsDetail = new spaDataset({
        form: module,
        id: 'dsDetail',
        table: 'pay_job',
        pk: 'job_id',
        webservice: '/services/pay_job_Services.asmx/GetByID',
        params: '"id": "' + ID + '"',
        onLoad: function () {
            if (ID == EMPTY_GUID) {
                module.dsDetail.AddRow();
            }
        }
    });

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

}


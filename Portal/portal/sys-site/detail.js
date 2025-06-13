function init(module, params) {

    var ID = EMPTY_GUID;

    if (params.id != EMPTY_GUID) {
        ID = params.id;
    } else {
        module.hideDeleteBtn();
    }

    module.aStatus = new Array(['a', 'Active'], ['i', 'Inactive']);
    module.aTypes = new Array(['1', 'PF Changs'], ['2', 'PeiWei']);
    module.aOrgs = new Array(['1', 'One'], ['2', 'Two']);
    /*
    module.aStates = new Array(
['AK', 'Alaska'],
['AL', 'Alabama'],
['AR', 'Arkansas'],
['AZ', 'Arizona'],
['CA', 'California'],
['CO', 'Colorado'],
['CT', 'Connecticut'],
['DC', 'District of Columbia'],
['DE', 'Delaware'],
['FL', 'Florida'],
['GA', 'Georgia'],
['HI', 'Hawaii'],
['IA', 'Iowa'],
['ID', 'Idaho'],
['IL', 'Illinois'],
['IN', 'Indiana'],
['KS', 'Kansas'],
['KY', 'Kentucky'],
['LA', 'Louisiana'],
['MA', 'Massachusetts'],
['MD', 'Maryland'],
['ME', 'Maine'],
['MI', 'Michigan'],
['MN', 'Minnesota'],
['MO', 'Missouri'],
['MS', 'Mississippi'],
['MT', 'Montana'],
['NC', 'North Carolina'],
['ND', 'North Dakota'],
['NE', 'Nebraska'],
['NH', 'New Hampshire'],
['NJ', 'New Jersey'],
['NM', 'New Mexico'],
['NV', 'Nevada'],
['NY', 'New York'],
['OH', 'Ohio'],
['OK', 'Oklahoma'],
['OR', 'Oregon'],
['PA', 'Pennsylvania'],
['RI', 'Rhode Island'],
['SC', 'South Carolina'],
['SD', 'South Dakota'],
['TN', 'Tennessee'],
['TX', 'Texas'],
['UT', 'Utah'],
['VA', 'Virginia'],
['VT', 'Vermont'],
['WA', 'Washington'],
['WI', 'Wisconsin'],
['WV', 'West Virginia'],
['WY', 'Wyoming']);
*/
    module.states = [{ 'id': 'AL', 'name': 'Alabama' },
                      { 'id': 'AK', 'name': 'Alaska' },
                      { 'id': 'AZ', 'name': 'Arizona' },
                      { 'id': 'AR', 'name': 'Arkansas' },
                      { 'id': 'CA', 'name': 'California' },
                      { 'id': 'CO', 'name': 'Colorado' },
                      { 'id': 'CT', 'name': 'Connecticut' },
                      { 'id': 'DE', 'name': 'Delaware' },
                      { 'id': 'FL', 'name': 'Florida' },
                      { 'id': 'GA', 'name': 'Georgia' },
                      { 'id': 'HI', 'name': 'Hawaii' },
                      { 'id': 'ID', 'name': 'Idaho' },
                      { 'id': 'IL', 'name': 'Illinois' },
                      { 'id': 'IN', 'name': 'Indiana' },
                      { 'id': 'IA', 'name': 'Iowa' },
                      { 'id': 'KS', 'name': 'Kansas' },
                      { 'id': 'KY', 'name': 'Kentucky' },
                      { 'id': 'LA', 'name': 'Louisiana' },
                      { 'id': 'ME', 'name': 'Maine' },
                      { 'id': 'MD', 'name': 'Maryland' },
                      { 'id': 'MA', 'name': 'Massachusetts' },
                      { 'id': 'MI', 'name': 'Michigan' },
                      { 'id': 'MN', 'name': 'Minnesota' },
                      { 'id': 'MS', 'name': 'Mississippi' },
                      { 'id': 'MO', 'name': 'Missouri' },
                      { 'id': 'MT', 'name': 'Montana' },
                      { 'id': 'NE', 'name': 'Nebraska' },
                      { 'id': 'NV', 'name': 'Nevada' },
                      { 'id': 'NH', 'name': 'New Hampshire' },
                      { 'id': 'NJ', 'name': 'New Jersey' },
                      { 'id': 'NM', 'name': 'New Mexico' },
                      { 'id': 'NY', 'name': 'New York' },
                      { 'id': 'NC', 'name': 'North Carolina' },
                      { 'id': 'ND', 'name': 'North Dakota' },
                      { 'id': 'OH', 'name': 'Ohio' },
                      { 'id': 'OK', 'name': 'Oklahoma' },
                      { 'id': 'OR', 'name': 'Oregon' },
                      { 'id': 'PA', 'name': 'Pennsylvania' },
                      { 'id': 'RI', 'name': 'Rhode Island' },
                      { 'id': 'SC', 'name': 'South Carolina' },
                      { 'id': 'SD', 'name': 'South Dakota' },
                      { 'id': 'TN', 'name': 'Tennessee' },
                      { 'id': 'TX', 'name': 'Texas' },
                      { 'id': 'UT', 'name': 'Utah' },
                      { 'id': 'VT', 'name': 'Vermont' },
                      { 'id': 'VA', 'name': 'Virginia' },
                      { 'id': 'WA', 'name': 'Washington' },
                      { 'id': 'WV', 'name': 'West Virginia' },
                      { 'id': 'WI', 'name': 'Wisconsin' },
                      { 'id': 'WY', 'name': 'Wyoming'}];

    module.addAutoOptions('state', module.states);

    module.addOptions('site_type', module.aTypes);
    module.addOptions('site_status', module.aStatus);
    module.addOptions('payroll_status', module.aStatus);
    module.addOptions('org_id', module.aOrgs);
    //module.addOptions('state', module.aStates);

    module.dsDetail = new spaDataset({
        form: module,
        id: 'dsDetail',
        table: 'sys_site',
        pk: 'site_id',
        webservice: '/services/sys_site_Services.asmx/GetByID',
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


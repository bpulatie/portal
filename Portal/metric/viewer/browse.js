function init(module, params) {

    module.hideTab('data');

    module.dsMetric = new spaDataset({
        form: module,
        id: 'dsMetric',
        pk: 'metric_id',
        webservice: '/services/metric_Services.asmx/GetAllMetrics',
        onLoad: function () {
            module.aMetric = new Array();

            for (var x = 0; x < module.dsMetric.RowCount(); x++) {
                var row = module.dsMetric.GetRow(x);
                module.aMetric.push([row.metric_id, row.metric_name]);
            }

            module.addOptions('metric_id', module.aMetric);
        }
    });

    module.metric_id_change = function (id) {
        module.dsHeader = new spaDataset({
            form: module,
            id: 'dsHeader',
            pk: 'metric_id',
            params: '"metric_id": "' + id + '"',
            webservice: '/services/metric_Services.asmx/GetByMetricID',
            onLoad: function () {
                var dim1 = "";
                var dim2 = "";
                var dim3 = "";
                var val1 = "";

                if (module.dsHeader.GetCurrentRow().dimension_1_label != null) {
                    dim1 = module.dsHeader.GetCurrentRow().dimension_1_label;
                }
                if (module.dsHeader.GetCurrentRow().dimension_2_label != null) {
                    dim2 = module.dsHeader.GetCurrentRow().dimension_2_label;
                }
                if (module.dsHeader.GetCurrentRow().dimension_3_label != null) {
                    dim3 = module.dsHeader.GetCurrentRow().dimension_3_label;
                }
                if (module.dsHeader.GetCurrentRow().value_1_label != null) {
                    val1 = module.dsHeader.GetCurrentRow().value_1_label;
                }

                module.myColumns = [{ label: 'Business Date', width: '20%', field: 'business_date', type: 'date' },
                        { label: dim1, width: '20%', field: 'dimension_1_name' },
                        { label: dim2, width: '20%', field: 'dimension_2_name' },
                        { label: dim3, width: '20%', field: 'dimension_3_name' },
                        { label: val1, width: '20%', field: 'value_1', type: 'numeric'}];

                module.myFilters = [{ name: 'myStartDate', field: 'business_date', label: 'Start Date', type: 'date', criteria: 'greaterthan' },
                                    { name: 'myEndDate', field: 'business_date', label: 'End Date', type: 'date', criteria: 'lessthan'}];

                module.myGrid = new spaGrid({
                    form: module,
                    id: 'myGrid',
                    title: "Metric Browse",
                    columns: module.myColumns,
                    filters: module.myFilters,
                    height: 430,
                    onLoad: function () {
                    }
                });

                module.showTab('data');

                module.dsBrowse = new spaDataset({
                    form: module,
                    id: 'dsBrowse',
                    pk: 'destination_no',
                    webservice: '/services/metric_Services.asmx/GetAll',
                    pageSize: 30
                });

                module.refresh = function () {
                    module.dsBrowse.Reload();
                };

            }
        });
    }
}


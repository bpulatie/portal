function spaGrid(options) {

    this.defaultOptions = {
        form: null,
        title: null,
        columns: [],
        filters: [],
        rowID: null,
        height: 430,
        newBtn: null,
        newBtnFeature: null,
        newLocation: null,
        buttons: [],
        onLoad: null,
        onSelect: null,
        onRemove: null
    };

    this.myFilter = "";
    this.settings = $.extend({}, this.defaultOptions, options);
    this.module_id = CURRENT_MODULE_ID;
    this.element = $('div[spaGrid="' + this.settings.id + '"]', '#' + this.module_id);

    this.id = this.module_id + "_" + this.settings.id;

    // Private Methods
    this._init = function () {
        this.pageNo = 1;
        var base = this;

        var panel_height = base.settings.height + 120;

        // Wrapper
        var html = "<div class='panel panel-info'>";

        // Title
        if (base.settings.title != null) {
            html += "    <div class='panel-heading'>";
            html += "        <h3 id='" + base.id + "_title" + "' class='panel-title'>" + base.settings.title + "</h3>";
            html += "    </div>";
        }

        html += "    <div style='height:" + panel_height + "px'>";

        // Filters
        if (base.settings.filters.length > 0) {

            html += "        <div id='" + base.id + "_gridFilter' class='panel-body' style='display:none;'>";
            html += "            <form class='form-horizontal' role='form'>";


            for (var x = 0; x < base.settings.filters.length; x++) {
                var fil = base.settings.filters[x];

                switch (fil.type) {
                    case 'date':
                        html += "            <div class='form-group'>";
                        html += "                <label for='" + base.id + "_" + fil.name + "' class='col-sm-4 control-label'>" + fil.label + "</label>";
                        html += "                <div class='col-sm-8'>";
                        html += "                    <div class='input-group date' id='" + base.id + "_" + fil.name + "'>";
                        html += "                        <input type='text' class='form-control' placeholder='" + fil.label + "'/>";
                        html += "                        <span class='input-group-addon'>";
                        html += "                            <span class='glyphicon glyphicon-calendar'></span>";
                        html += "                        </span>";
                        html += "                    </div>";
                        html += "                </div>";
                        html += "            </div>";
                        break;

                    case 'array':
                        var id = base.id + "_" + fil.name;

                        html += "            <div class='form-group'>";
                        html += "                <label for='" + id + "' class='col-sm-4 control-label'>" + fil.label + "</label>";
                        html += "                <div class='col-sm-8'>";
                        html += "                    <input id='" + id + "_hidden' type='hidden' />";
                        html += "                    <div class='input-group'>";
                        html += "                        <input id='" + id + "' type='text' class='form-control select' readOnly placeholder='" + fil.label + "'/>";
                        html += "                        <div class='input-group-btn'>";
                        html += "                            <button class='btn btn-default dropdown-toggle' data-toggle='dropdown' type='button'>";
                        html += "                                <span class='glyphicon glyphicon-triangle-bottom'></span>";
                        html += "                            </button>";
                        html += "                            <ul id='" + id + "_dropdown' class='dropdown-menu pull-right'>";

                        $.each(fil.values, function (x, item) {
                            html += "                                <li><a id='" + id + "_" + item[0] + "' href='#'>" + item[1] + "</a></li>";
                        });

                        html += "                            </ul>";
                        html += "                        </div>";
                        html += "                    </div>";
                        html += "                </div>";
                        html += "            </div>";


                        break;

                    default:
                        html += "            <div class='form-group'>";
                        html += "                <label for='" + base.id + "_" + fil.name + "' class='col-sm-4 control-label'>" + fil.label + "</label>";
                        html += "                <div class='col-sm-8'><input id='" + base.id + "_" + fil.name + "' class='form-control' type='text' placeholder='" + fil.label + "'/></div>";
                        html += "            </div>";
                        break;
                }
            }

            html += "                <div class='form-group'>";
            html += "                    <div class='col-sm-offset-4 col-sm-8'>";
            html += "                        <button id='" + base.id + "_goGrid' class='btn btn-primary' type='button'>Apply</button>";
            html += "                        <button id='" + base.id + "_cancelGrid' class='btn' type='button'>Cancel</button>";
            html += "                        <button id='" + base.id + "_resetBtn' class='btn' type='button'>Reset</button>";
            html += "                    </div>";
            html += "                </div>";
            html += "            </form>";
            html += "        </div>";
        }

        // Grid
        html += "        <div id='" + base.id + "_gridData' class='table-responsive'>";
        html += "            <table style='margin-bottom: 0;' class='table table-striped table-condensed'>";

        //id = '" + base.id + "_div' 
        //Header
        html += "                <thead>";
        html += "                    <tr>";

        for (var x = 0; x < base.settings.columns.length; x++) {
            var col = base.settings.columns[x];

            var tag;
            switch (col.type) {
                case "numeric":
                    tag = "<th style='text-align:right;width:" + col.width + "'>";
                    break;

                case "check":
                    tag = "<th style='text-align:center;width:" + col.width + "'>";
                    break;

                case "remove":
                    tag = "<th style='text-align:center;width:" + col.width + "'>";
                    col.label = '&nbsp;';
                    break;

                default:
                    tag = "<th style='width:" + col.width + "'>";
            }

            html += tag + col.label + "</th>";
        }
        html += "                        <th width='1em'>&nbsp;</th>";
        html += "                    </tr>";
        html += "                </thead>";
        html += "            </table>";

        //Body
        html += "            <div class='scrollable' style='height: " + base.settings.height + "px; width: 100%; margin: 0; overflow-y: scroll; border-bottom: 1px solid #cccccc;'>";

        html += "                <table style='margin: 0; padding: 0;' class='table table-striped table-condensed'>";
        html += "                    <tbody id='" + base.id + "_grid_body'>";
        html += "                    </tbody>";
        html += "                </table>";
        html += "            </div>";

        //Footer
        html += "            <div id='" + base.id + "_grid_footer' class='panel-body'>";
        html += "            </div>";

        html += "        </div>";
        html += "    </div>";
        html += "</div>";

        $(base.element).html(html);


        for (var x = 0; x < base.settings.filters.length; x++) {
            var fil = base.settings.filters[x];

            switch (fil.type) {

                case 'array':
                    var id = base.id + "_" + fil.name;

                    var y = 0;
                    if (fil.index != undefined)
                        y = fil.index;

                    $("#" + id + "_hidden").val(id + "_" + fil.values[y][0]);
                    $("#" + id).val(fil.values[y][1]);

                    $("#" + id + "_dropdown li a").click(function (e) {
                        $("#" + id).val($(this).text());
                        $("#" + id + "_hidden").val($(this).attr("id")).trigger('change');
                    });
                    break;

                default:
            }
        }

    }

    //Public methods
    spaGrid.prototype.DisplayGridData = function (data, Scroll) {
        var base = this;
        var mode = base.settings.form.settings.mode;
        var html = "";
        var odd = true;
        var rowNo = 0;

        for (var row in data.Rows) {
            var line = data.Rows[row];

            if (base.settings.rowID != null) {
                html += "<tr id='" + line[base.settings.rowID] + "' rowNo='" + rowNo + "' >";
            } else {
                html += "<tr rowNo='" + rowNo + "' >";
            }

            rowNo++;

            for (var x = 0; x < base.settings.columns.length; x++) {
                //get column name 
                var col = base.settings.columns[x];
                var Value = "";
                try {
                    Value = line[col.field];
                } catch (e) { };

                if (Value === undefined) {
                    Value = col.field;
                }

                if (col.accessFeature != undefined) {
                    if (hasAccessFeature(col.accessFeature) == false) {
                        Value = "####";
                    }
                }

                var spaStyle = " style='width:" + col.width + ";";
                switch (col.type) {
                    case "numeric":
                        spaStyle += "text-align:right;";
                        break;

                    case "check":
                        spaStyle += "text-align:center;";
                        break;

                    default:
                        spaStyle += "";
                }

                spaStyle += "'";

                var spaClass = "";
                if (col.class != undefined) {
                    spaClass = " class='" + col.class + "'";
                }

                var spaLink = "";
                if (col.link != undefined) {
                    spaLink = " spaLink='" + col.link + "'";
                }

                var tag = "<td" + spaStyle + spaClass + spaLink + ">";

                //Value = Value.replace("<", "&lt;");
                //Value = Value.replace(">", "&gt;");

                //format data
                switch (col.type) {
                    case "datetime":
                        {
                            Value = FormatDateWithTime(Value);
                            break;
                        }
                    case "date":
                        {
                            Value = FormatDate(Value);
                            break;
                        }
                    case "numeric":
                        {
                            if (Value == null) {
                                Value = 0;
                            }
                            if (col.format == "money") {
                                Value = formatMoney(Value);
                            }
                            break;
                        }
                    case "check":
                        {
                            if (Value == 'y') {
                                Value = "<span class='glyphicon glyphicon-ok'></span>";
                            } else {
                                Value = "";
                            }
                            break;
                        }
                    case "remove":
                        {
                            var disabled = false;
                            if (col.accessFeature != undefined) {
                                if (hasAccessFeature(col.accessFeature) == false) {
                                    disabled = true;
                                }
                            }

                            if (disabled)
                                Value = "&nbsp;"
                            else
                                Value = "<button type='button' class='btn btn-xs btn-danger'><i class='fa fa-times' aria-hidden='true'></i></button>";
                        }
                    default:
                        {
                            break;
                        }
                }

                if (Value == null || Value == "") {
                    html += tag + "&nbsp;</td>";
                } else {
                    html += tag + Value + "</td>";
                }
            }

            html += "</tr>";
        }

        $('#' + base.id + '_grid_body').html(html);

        var shtml = "";

        if (base.settings.newBtn != null) {
            if (mode != 'view') {
                shtml += "<button id='" + base.id + "_newBtn' style='float:left;margin-top:20px;' class='btn btn-primary' type='button'>" + base.settings.newBtn + "</button>";
            }
        }

        for (var x = 0; x < base.settings.buttons.length; x++) {
            var btn = base.settings.buttons[x];
            shtml += "<button id='" + base.id + "_" + btn.name + "' style='float:left;margin-left:10px;margin-top:20px;' class='btn " + btn.class + "' type='button'>" + btn.label + "</button>";
        }

        // Set Pagination
        if (Scroll != true) {

            shtml += "<ul style='float:right' class='pagination'>";

            var iStart = 1;
            var iEnd = 1;

            if (data.PageNo > 2)
                iStart = data.PageNo - 2;

            if (data.PageCount < (iStart + 4)) {
                iEnd = parseInt(data.PageCount, 10);
            } else {
                iEnd = iStart + 4;
            }

            if (iEnd == parseInt(data.PageCount, 10)) {
                if (iEnd > 5) {
                    iStart = (iEnd - 4);
                }
            }

            if (data.PageNo == "1") {
                shtml += "<li class='disabled'><a id='" + base.id + "___1" + "' class='pageNo' href='#'><span class='glyphicon glyphicon-step-backward' aria-hidden='true'></span></a></li>";
                shtml += "<li class='disabled'><a id='" + base.id + "_previous' href='#'><span class='glyphicon glyphicon-chevron-left' aria-hidden='true'></span></a></li>";
            } else {
                shtml += "<li><a id='" + base.id + "___1" + "' class='pageNo' href='#'><span class='glyphicon glyphicon-step-backward' aria-hidden='true'></span></a></li>";
                shtml += "<li><a id='" + base.id + "_previous' href='#'><span class='glyphicon glyphicon-chevron-left' aria-hidden='true'></span></a></li>";
            }

            for (var x = iStart; x < iEnd + 1; x++) {
                if (x == data.PageNo) {
                    shtml += "<li class='active'><a id='" + base.id + "___" + x + "' class='pageNo' href='#'>" + x + " <span class='sr-only'>(current)</span></a></li>";
                } else {
                    shtml += "<li ><a id='" + base.id + "___" + x + "' class='pageNo' href='#'>" + x + " <span class='sr-only'>(current)</span></a></li>";
                }
            }

            if (data.MoreData == "n") {
                shtml += "<li class='disabled'><a id='" + base.id + "_next' href='#'><span class='glyphicon glyphicon-chevron-right' aria-hidden='true'></span></a></li>";
                shtml += "<li class='disabled'><a id='" + base.id + "___" + data.PageCount + "' class='pageNo' href='#'><span class='glyphicon glyphicon-step-forward' aria-hidden='true'></span></a></li>";
            } else {
                shtml += "<li><a id='" + base.id + "_next' href='#'><span class='glyphicon glyphicon-chevron-right' aria-hidden='true'></span></a></li>";
                shtml += "<li><a id='" + base.id + "___" + data.PageCount + "' class='pageNo' href='#'><span class='glyphicon glyphicon-step-forward' aria-hidden='true'></span></a></li>";
            }

            if (base.settings.filters.length > 0) {
                shtml += "<li><a id='" + base.id + "_searchGrid' href='#'>Search</a></li>";
                shtml += "</ul>";
            }
        }

        // Scroll
        else {
            shtml += "<div id='" + base.id + "_mouse'></div>";
        }

        $('#' + base.id + '_grid_footer').html(shtml);

        // Attach Events
        for (var x = 0; x < base.settings.columns.length; x++) {
            var col = base.settings.columns[x];

            if (col.class != '') {
                $('#' + base.id + '_grid_body td.' + col.class).hover(function () {
                    $(this).addClass('hover');
                }, function () {
                    $(this).removeClass('hover');
                });
            };

            if (col.class == 'delete') {
                if (typeof base.settings.onRemove === 'function') {
                    var temp = col.menu;
                    $('#' + base.id + '_grid_body td.delete').on('click', function () {
                        var id = $(this).parent().attr('id');
                        base.settings.onRemove(id);
                    });
                };
            } else {
                if (typeof base.settings.onSelect === 'function') {
                    var temp = col.menu;
                    $('#' + base.id + '_grid_body td.' + col.class).on('click', function () {
                        // Move dataset to this row
                        var rowNo = $(this).parent().attr('rowNo');
                        var form = base.settings.form;
                        var cmd = "form." + $(base.element).attr('dataset') + ".MoveTo(" + rowNo + ")";
                        eval(cmd);
                        var id = $(this).parent().attr('id');
                        base.settings.onSelect(id);
                    });
                } else {
                    if (col.link != undefined) {
                        var temp = col.menu;
                        $('#' + base.id + '_grid_body td.' + col.class).on('click', function () {
                            var id = $(this).parent().attr('id');
                            var link = $(this).attr('spaLink');
                            var data = $(this).text();
                            var name = "Detail";
                            var mode = base.settings.form.settings.mode;

                            if (temp != '') {
                                name = temp.replace('%%', data);
                            }
                            createModule(name, link, { 'id': id }, mode);
                        });
                    };
                }
            }
        };

        // If Paged Dataset
        if (Scroll != true) {
            $('#' + base.id + '_previous').on('click', function () {
                var form = base.settings.form;
                var cmd = "form." + $(base.element).attr('dataset') + ".PreviousPage()";
                eval(cmd);
                //$('div[spaDataset=' + $(base.element).attr('dataset') + ']', $('#' + base.module_id)).data('plugin_spaDataset').PreviousPage();
            });

            $('#' + base.id + '_grid_body tr').on('click', function (event) {
                $(this).addClass('info').siblings().removeClass('info');
                // Move dataset to this row
                var rowNo = $(this).attr('rowNo');
                var form = base.settings.form;
                var cmd = "form." + $(base.element).attr('dataset') + ".MoveTo(" + rowNo + ")";
                eval(cmd);
            });

            $('#' + base.id + '_next').on('click', function () {
                var form = base.settings.form;
                var cmd = "form." + $(base.element).attr('dataset') + ".NextPage()";
                eval(cmd);
                //$('div[spaDataset=' + $(base.element).attr('dataset') + ']', $('#' + base.module_id)).data('plugin_spaDataset').NextPage();
            });

            $('.pageNo').on('click', function () {
                var myId = $(this).attr('id');
                var myPage = myId.split("___");
                var form = base.settings.form;
                var cmd = "form." + $(base.element).attr('dataset') + ".Page(" + myPage[1] + ")";
                eval(cmd);
                //$('div[spaDataset=' + $(base.element).attr('dataset') + ']', $('#' + base.module_id)).data('plugin_spaDataset').Page(myPage[1]);
            });
        }

        // Auto Paging with Scroll
        else {
            $("#" + base.id + "_grid_body").scroll(function () {
                var iScrollHeight = $("#" + base.id + "_grid_body").prop('scrollHeight');
                var iScrollTop = $("#" + base.id + "_grid_body").scrollTop();
                var iHeight = $("#" + base.id + "_grid_body").height();

                var myHtml = "Height=" + iHeight + " ScrollHeight=" + iScrollHeight + " ScrollTop=" + iScrollTop;

                $("#" + base.id + "_mouse").html(myHtml);

                if ((iScrollHeight - iHeight) <= (iScrollTop + 10)) {
                    alert('Page');
                    var form = base.settings.form;
                    var cmd = "form." + $(base.element).attr('dataset') + ".NextPage()";
                    eval(cmd);
                }
            });
        }

        $('#gridFilter').hide();

        $("#" + base.id + "_searchGrid").on('click', function () {
            $("#" + base.id + "_gridData").hide();
            $("#" + base.id + "_gridFilter").show();
        });

        $("#" + base.id + "_goGrid").on('click', function () {
            $("#" + base.id + "_gridFilter").hide();

            var form = base.settings.form;
            var cmd = "form." + $(base.element).attr('dataset') + ".Reload(\"" + base.GetFilter() + "\")";
            eval(cmd);

            $("#" + base.id + "_title")[0].innerText = base.settings.title + base.myFilter;
            $("#" + base.id + "_gridData").show();
        });

        for (var x = 0; x < base.settings.filters.length; x++) {
            var fil = base.settings.filters[x];

            if (fil.type === 'date') {
                $('#' + base.id + "_" + fil.name).datetimepicker({
                    format: 'MM/DD/YYYY',
                    showClose: true
                });
            }
        }

        $("#" + base.id + "_cancelGrid").on('click', function () {
            $("#" + base.id + "_gridFilter").hide();
            $("#" + base.id + "_gridData").show();
        });

        $("#" + base.id + "_resetBtn").on('click', function () {
            for (var x = 0; x < base.settings.filters.length; x++) {
                var fil = base.settings.filters[x];

                switch (fil.type) {
                    case 'date':
                        $("#" + base.id + "_" + fil.name).data("DateTimePicker").date(null);
                        break;

                    case 'array':
                        $("#" + base.id + "_" + fil.name + "_hidden").val(base.id + "_" + fil.name + "_" + fil.values[0][0]);
                        $("#" + base.id + "_" + fil.name).val(fil.values[0][1]);
                        break;

                    default:
                        $("#" + base.id + "_" + fil.name).val("");
                }
            }
        });

        if (mode != 'view') {
            $("#" + base.id + "_newBtn").click(function () {
                if (typeof base.settings.newLocation === 'function') {
                    base.settings.newLocation();
                } else {
                    var id = EMPTY_GUID;
                    var link = base.settings.newLocation;
                    var name = base.settings.newBtn;
                    createModule(name, link, { 'id': id });
                };
            });
        }

        if (typeof base.settings.onLoad === 'function')
            base.settings.onLoad();
    }

    spaGrid.prototype.GetFilter = function () {
        var base = this;
        base.myFilter = "";
        var filter = "[";
        for (var x = 0; x < base.settings.filters.length; x++) {
            var fil = base.settings.filters[x];

            var criteria = "Equals";
            switch (fil.criteria) {
                case 'greaterthan':
                    criteria = '>';
                    break;

                case 'lessthan':
                    criteria = '<';
                    break;

                case 'contains':
                    criteria = 'Contains';
                    break;

                case 'startswith':
                    criteria = 'Startswith';
                    break;

                case 'endswith':
                    criteria = 'Endswith';
                    break;

                case 'equals':
                    criteria = 'Equals';
                    break;

                default:
                    criteria = fil.criteria;

            }

            switch (fil.type) {
                case 'date':
                    if ($("#" + base.id + "_" + fil.name).data("DateTimePicker").date() !== null) {
                        if (filter.length > 15)
                            filter += ", ";
                        var dte = $("#" + base.id + "_" + fil.name).data("DateTimePicker").date().format();
                        dte = dte.substring(0, 10);
                        filter += "{ 'column': '" + fil.field + "', 'comparison': '" + fil.criteria + "', 'value': '" + dte + "' }";
                        base.myFilter += " - " + fil.label + " " + criteria + " " + dte + " ";
                    };
                    break;

                case 'array':
                    var id = base.id + "_" + fil.name;
                    var sVal = $("#" + id + "_hidden").val();
                    var ssVal = $("#" + id).val();
                    var val = sVal.substr(id.length + 1);

                    if (val != "") {
                        if (filter.length > 15)
                            filter += ", ";
                        filter += "{ 'column': '" + fil.field + "', 'comparison': '" + fil.criteria + "', 'value': '" + val + "' }";
                        base.myFilter += " - " + fil.label + " " + criteria + " " + ssVal + " ";
                    };
                    break;

                default:
                    var val = $("#" + base.id + "_" + fil.name).val();
                    if (val != "") {
                        if (filter.length > 15)
                            filter += ", ";
                        filter += "{ 'column': '" + fil.field + "', 'comparison': '" + fil.criteria + "', 'value': '" + val + "' }";
                        base.myFilter += " - " + fil.label + " " + criteria + " " + val + " ";
                    }
            }
        }
        filter += "]";
        return filter;
    }

    this._init();
};



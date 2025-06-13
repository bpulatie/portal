function spaTreeGrid(options) {

    this.defaultOptions = {
        form: null,
        title: null,
        columns: [],
        rowID: null,
        filter: null,
        height: 430,
        onLoad: null,
        onSelect: null
    };

    this.settings = $.extend({}, this.defaultOptions, options);
    this.module_id = CURRENT_MODULE_ID;
    this.element = $('div[spaTreeGrid="' + this.settings.id + '"]', '#' + this.module_id)
    this.dataset = $(this.element).attr("dataset"); 

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
            html += "        <h3 class='panel-title'>" + base.settings.title + "</h3>";
            html += "    </div>";
        }

        html += "    <div style='height:" + panel_height + "px'>";

        // Grid
        html += "        <div id='" + base.id + "_gridData'>";
        html += "             <div class='spa-grid' id='" + base.id + "_div'>";

        //Header
        html += "<div class='spa-grid-header'>";
        for (var x = 0; x < base.settings.columns.length; x++) {
            var col = base.settings.columns[x];

            var tag;
            switch (col.type) {
                case "numeric":
                    tag = "<div class='spa-grid-column-header' style='text-align:right;width:" + col.width + "'>";
                    break;

                case "check":
                    tag = "<div class='spa-grid-column-header' style='text-align:center;width:" + col.width + "'>";
                    break;

                default:
                    tag = "<div class='spa-grid-column-header' style='width:" + col.width + "'>";
            }

            html += tag + col.label + "</div>";
        }
        html += "</div><br/>";

        //Body
        html += "<div id='" + base.id + "_grid_body' style='height:" + base.settings.height + "px;' class='spa-grid-body'>";
        html += "</div>";

        //Footer
        html += "<div id='" + base.id + "_grid_footer' class='panel-body'>";
        html += "</div>";

        html += "</div>";
        html += "</div>";

        $(base.element).html(html);

    }

    //Public methods
    spaTreeGrid.prototype.DisplayTreeGridData = function (data) {
        var base = this;
        var html = "";
        var odd = true;
        var rowclass = "spa-grid-odd";
        var targetDiv = null;

        for (var row in data.Rows) {
            var line = data.Rows[row];

            if (targetDiv === null) {
                var targetDiv = this.module_id + "_" + (line.level - 1);
                if (line.level == "1") {
                    targetDiv = null;
                }
                if (line.level == "2") {
                    targetDiv += "_" + line.p1;
                }
                if (line.level == "3") {
                    targetDiv += "_" + line.p1 + "_" + line.p2;
                }
                if (line.level == "4") {
                    targetDiv += "_" + line.p1 + "_" + line.p2 + "_" + line.p3;
                }
                if (line.level == "5") {
                    targetDiv += "_" + line.p1 + "_" + line.p2 + "_" + line.p3 + "_" + line.p4;
                }
                if (line.level == "6") {
                    targetDiv += "_" + line.p1 + "_" + line.p2 + "_" + line.p3 + "_" + line.p4 + "_" + line.p5;
                }
            }

            if (odd == true) {
                rowclass = "spa-grid-odd";
                odd = false;
            } else {
                rowclass = "spa-grid-even";
                odd = true;
            }

            html += "<div class='spa-grid-row " + rowclass + "' row='" + JSON.stringify(line) + "'>";

            var blockDiv = "";
            for (var x = 0; x < base.settings.columns.length; x++) {

                //get column name 
                var col = base.settings.columns[x];

                var Value = "";
                try {
                    Value = line[col.field];
                } catch (e) { };

                if (Value === undefined) {
                    Value = '';
                }

                //format data
                switch (col.type) {
                    case "datetime":
                        {
                            if (Value != "") {
                                Value = FormatDateWithTime(Value);
                            }
                            break;
                        }
                    case "date":
                        {
                            if (Value != "") {
                                Value = FormatDate(Value);
                            }
                            break;
                        }
                    case "numeric":
                        {
                            if (Value != "") {
                                if (Value == null) {
                                    Value = 0;
                                }
                                if (col.format == "money") {
                                    Value = Value.toFixed(2);
                                }
                            }
                            break;
                        }
                    case "check":
                        {
                            if (Value == true) {
                                Value = "<span class='glyphicon glyphicon-ok'></span>";
                            } else {
                                Value = "";
                            }
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }

                var tag;
                switch (col.type) {
                    case "numeric":
                        tag = "<div class='spa-grid-column' style='text-align:right;width:" + col.width + "'>";
                        break;

                    case "check":
                        tag = "<div class='spa-grid-column' style='text-align:center;width:" + col.width + "'>";
                        break;

                    default:
                        tag = "<div class='spa-grid-column' style='width:" + col.width + "'>";
                }


                if (col.level !== undefined) {
                    if (parseInt(col.level, 10) !== parseInt(line.level, 10)) {
                        //a group field but not the right one blank it out
                        html += tag + "&nbsp;</div>";
                    } else {
                        // a group field - the right one!

                        var myId = this.module_id + "_" + line.level;
                        if (line.p1 != undefined) {
                            myId += "_" + line.p1
                        }
                        if (line.p2 != undefined) {
                            myId += "_" + line.p2
                        }
                        if (line.p3 != undefined) {
                            myId += "_" + line.p3
                        }
                        if (line.p4 != undefined) {
                            myId += "_" + line.p4
                        }
                        if (line.p5 != undefined) {
                            myId += "_" + line.p5
                        }
                        if (line.p6 != undefined) {
                            myId += "_" + line.p6
                        }

                        blockDiv = "<div id='" + myId + "'></div>";
                        html += tag + "<span level='" + myId + "' class='glyphicon spaTreeGrid glyphicon-triangle-right'></span>" + Value + "</div>";
                    }

                } else {

                    if (Value == null || Value == "") {
                        html += tag + "&nbsp;</div>";
                    } else {
                        html += tag + Value + "</div>";
                    }
                }

            }

            html += "</div>" + blockDiv;
        }

        if (targetDiv == null) {
            $('#' + base.id + '_grid_body').html(html);
        } else {
            $('#' + targetDiv).html(html);
        }

        // Attach Events
        $('#' + base.id + '_grid_body div.spa-grid-row').on('click', function (event) {
            $('#' + base.id + '_grid_body div.spa-grid-row').removeClass('bg-info');
            $(this).addClass('bg-info');
        });

        $('.glyphicon', '#' + base.module_id).hover(function () {
            $(this).addClass('hover');
        }, function () {
            $(this).removeClass('hover');
        });

        if (typeof base.settings.onExpand === 'function') {
            $('.spaTreeGrid.glyphicon-triangle-right', '#' + base.module_id).on('click', $.proxy(base.settings.onExpand, null, base));
        }

        if (typeof base.settings.onCollapse === 'function') {
            $('.spaTreeGrid.glyphicon-triangle-bottom', '#' + base.module_id).on('click', $.proxy(base.settings.onCollapse, null, base));
        }

        if (typeof base.settings.onLoad === 'function') {
            base.settings.onLoad();
        }
    }

    this._init();

};




function init(module, params) {


    var ID = EMPTY_GUID;
    var PARENT = EMPTY_GUID;

    RefreshTree();

    $("#New_btn").click(function () {
        Requirement_New();
    });

    $("#Edit").click(function () {
        Requirement_Edit();
    });

    $("#Delete").click(function () {
        Requirement_Delete();
    });

    $("#Print").click(function () {
        Requirement_Print();
    });

    $('#ReqDetail1').summernote({
        height: 300
    });

    CreateAttachments([], []);

    function RefreshTree() {

        $.ajax({
            type: "POST",
            contentType: "application/json",
            url: ROOT + "/services/requirement_Services.asmx/GetTopLevelRequirements",
            data: "",
            dataType: "json",
            success: function (oResponse) {
                if (oResponse.result == true) {
                    var sHTML = "";
                    var dataset = jQuery.parseJSON(oResponse.data)
                    for (var x = 0; x < dataset.Rows.length; x++) {
                        var row = dataset.Rows[x];
                        sHTML += "<div id='" + row.requirement_id + "_Head' draggable='true'  ondragstart='drag(event)' class='RequirementHeader'>";
                        sHTML += TreeNodeMarkup(row, false);
                        sHTML += "</div>";
                    }
                    $("#Treeview").html(sHTML);
                    ClearRequirementPanel();
                    ContentMenuListener();
                }
            },
            error: ajaxFailed
        });
    }

    function GetChildRequirements() {

        $.ajax({
            type: "POST",
            contentType: "application/json",
            url: ROOT + "/services/requirement_Services.asmx/GetChildRequirements",
            data: "{ 'id': '" + ID + "' }",
            dataType: "json",
            success: function (oResponse) {
                if (oResponse.result == true) {
                    var sHTML = "";
                    var datasets = jQuery.parseJSON(oResponse.data);
                    var row = datasets.Header.Rows[0];

                    sHTML += TreeNodeMarkup(row, true);

                    for (var x = 0; x < datasets.Children.Rows.length; x++) {
                        var row = datasets.Children.Rows[x];

                        sHTML += "<div id='" + row.requirement_id + "_Head' draggable='true'  ondragstart='drag(event)' class='RequirementHeader' style='margin-left:20px;white-space:nowrap;'>";
                        sHTML += TreeNodeMarkup(row, false);
                        sHTML += "</div>";
                    }

                    $("#" + ID + "_Head").html(sHTML);
                    ContentMenuListener();
                }
            },
            error: ajaxFailed
        });
    }

    function GetRequirementOnly() {

        $.ajax({
            type: "POST",
            contentType: "application/json",
            url: ROOT + "/services/requirement_Services.asmx/GetRequirementOnly",
            data: "{ 'id': '" + ID + "' }",
            dataType: "json",
            success: function (oResponse) {
                if (oResponse.result == true) {
                    var sHTML = "";
                    var datasets = jQuery.parseJSON(oResponse.data);
                    var row = datasets.Header.Rows[0];

                    var sHTML = TreeNodeMarkup(row, false);

                    $("#" + ID + "_Head").html(sHTML);
                    ContentMenuListener();
                }
            },
            error: ajaxFailed
        });
    }

    function TreeNodeMarkup(row, expanded) {

        var sHTML = "";

        if (row.counter == "0") {
            sHTML += "<span class='indent'></span>";
        } else {
            if (expanded) {
                sHTML += "<i class='fa fa-chevron-down' aria-hidden='true' onmouseover='chevron_mouseover(this)' onmouseout='chevron_mouseover(this)' onclick='Minus_Click(" + '"' + row.requirement_id + '"' + ");' >&nbsp;</i>";
            } else {
                sHTML += "<i class='fa fa-chevron-down' aria-hidden='true' onmouseover='chevron_mouseover(this)' onmouseout='chevron_mouseover(this)' onclick='Plus_Click(" + '"' + row.requirement_id + '"' + ");' >&nbsp;</i>";
            }
        }

        sHTML += "<i class='fa fa-bookmark' onmouseover='bookmark_mouseover(this)' onmouseout='bookmark_mouseover(this)' aria-hidden='true'>&nbsp;</i>" +
                 "<div   id='" + row.requirement_id + "'  ondrop='drop(event)' ondragover='allowDrop(event)' " +
				    "class='Requirement context' " +
				    "counter='" + row.counter + "' " +
                    "data-toggle='context' data-target='context-menu' " +
				    "onclick='Requirement_Click(this);'  " +
				    "onmouseover='Requirement_mouseover(this);' " +
				    "onmouseout='Requirement_mouseout(this);'>" + row.name +
                 "</div>";

        sHTML += "</div>";


        return sHTML;
    }

    function ContentMenuListener() {
        $('.context').contextmenu({
            target: '#context-menu',
            before: function (e, context) {
                Requirement_Click(context[0]);
            },
            onItem: function (context, e) {
                if ($(e.target).text() == " Add") {
                    Requirement_New();
                }
                if ($(e.target).text() == " Edit") {
                    Requirement_Edit();
                }
                if ($(e.target).text() == " Delete") {
                    Requirement_Delete();
                }
                if ($(e.target).text() == " Print") {
                    Requirement_Print();
                }
            }
        })
    }

    function allowDrop(ev) {
        ev.preventDefault();
    }

    function drag(ev) {
        var data = ev.target.id;
        ev.dataTransfer.setData("text", data.substring(0, 36));
    }

    function drop(ev) {
        ev.preventDefault();
        var data = ev.dataTransfer.getData("text");
        MoveRequirement(data, ev.target.id);
    }

    function Requirement_Click(e) {

        $('.RequirementHighlight').removeClass('RequirementHighlight').addClass('Requirement');
        e.className = "RequirementHighlight";

        GetRequirement(e.id);
    }

    function Requirement_New() {
        var top = true;
        if (ID != EMPTY_GUID)
            top = false;

        if (top) {
            bootbox.confirm({
                message: "Top Level Requirement?",
                buttons: {
                    confirm: {
                        label: 'Yes',
                        className: 'btn-primary'
                    },
                    cancel: {
                        label: 'No',
                        className: 'btn-default'
                    }
                },
                callback: function (result) {
                    if (result == false) {
                        bootbox.alert("Please select a Parent Requirement ");
                        return;
                    }
                    PARENT = EMPTY_GUID;
                    ID = EMPTY_GUID;
                    LoadRequirementModal("ADD", top);
                }
            });
        }

        if (!top) {
            bootbox.confirm({
                message: "Child Requirement?",
                buttons: {
                    confirm: {
                        label: 'Yes',
                        className: 'btn-primary'
                    },
                    cancel: {
                        label: 'No',
                        className: 'btn-default'
                    }
                },
                callback: function (result) {
                    if (result == false) {
                        top = true;
                        PARENT = EMPTY_GUID;
                    } else {
                        PARENT = ID;
                    }
                    ID = EMPTY_GUID;
                    LoadRequirementModal("ADD", top);
                }
            });
        }

    }

    function Requirement_Edit() {
        LoadRequirementModal("EDIT", false);
    }

    function Requirement_Delete() {
        if ($("#ReqCount").val() != "0") {
            bootbox.alert("Please delete child requirements first!");
            return;
        }

        bootbox.confirm({
            message: "Delete this Requirement?",
            buttons: {
                confirm: {
                    label: 'Yes',
                    className: 'btn-primary'
                },
                cancel: {
                    label: 'No',
                    className: 'btn-default'
                }
            },
            callback: function (result) {
                if (result == false) {
                    bootbox.alert("Please select a Parent Requirement ");
                    return;
                }
                DeleteRequirement();
            }
        });
    }

    function Requirement_Print() {
        bootbox.confirm({
            message: "Include all child requirements?",
            buttons: {
                confirm: {
                    label: 'Yes',
                    className: 'btn-primary'
                },
                cancel: {
                    label: 'No',
                    className: 'btn-default'
                }
            },
            callback: function (result) {
                var mode = "s";
                if (result == true) {
                    mode = "f";
                }
                window.open(ROOT + "/handlers/GetReport.ashx?mode=" + mode + "&id=" + ID, "print");
            }
        });
    }

    function LoadRequirementModal(mode, top) {
        if (mode == "ADD") {

            $("#ReqName1").val("");
            $("#ReqRef1").val("");
            $("#ReqOrder1").val("");
            $("#Keywords1").val("");
            //        $("#Clients1").val("");
            //        $("#Version1").val("");
            $("#ReqDetail1").summernote('code', "");

            if (top) {
                $("#RequirementModal_Title").html("New Top Level Requirement");
            } else {
                $("#RequirementModal_Title").html("New Child Requirement");
            }
        } else {

            $("#ReqName1").val($("#ReqName").val());
            $("#ReqRef1").val($("#ReqRef").val());
            $("#ReqOrder1").val($("#ReqOrder").val());
            $("#Keywords1").val($("#Keywords").val());
            //        $("#Clients1").val($("#Clients").val());
            //        $("#Version1").val($("#Version").val());
            $("#ReqDetail1").summernote('code', $("#ReqDetail").html());

            $("#RequirementModal_Title").html("Edit Requirement");
        }

        $("#SaveBtn").unbind('click');
        $("#SaveBtn").click(function (event) {
            Requirement_Save(mode);
        });
        $("#RequirementModal").modal();
    }

    function Requirement_Save(mode) {
        var data = {
            action: mode,
            parent: PARENT,
            id: ID,
            name: $("#ReqName1").val(),
            reference: $("#ReqRef1").val(),
            order: $("#ReqOrder1").val(),
            keywords: $("#Keywords1").val(),
            clients: '', //$("#Clients1").val(),
            version: '', //$("#Version1").val(),
            detail: $("#ReqDetail1").summernote('code')
        };

        $.ajax({
            type: "POST",
            contentType: "application/json",
            url: ROOT + "/services/requirement_Services.asmx/SaveRequirement",
            data: JSON.stringify(data),
            dataType: "json",
            success: function (oResponse) {
                if (oResponse.result == true) {
                    var sHTML = "";
                    ID = oResponse.data;
                    GetRequirementOnly();
                }
            },
            error: ajaxFailed
        });

    }

    function ClearRequirementPanel() {
        $("#ReqName").val("");
        $("#ReqRef").val("");
        $("#ReqOrder").val("");
        $("#ReqDetail").html("");

        $("#New_btn").show();
        $("#Edit").hide();
        $("#Delete").hide();
        $("#Print").hide();
    }

    function Plus_Click(e, NewID) {
        ID = e;
        GetChildRequirements();
    }

    function End_Click(e) {

    }

    function Minus_Click(e) {
        ID = e;
        GetRequirementOnly();
    }

    function chevron_mouseover(e) {
        e.style.cursor = "pointer";
    }

    function chevron_mouseout(e) {
        e.style.cursor = "";
    }

    function bookmark_mouseover(e) {
        e.style.cursor = "move";
    }

    function bookmark_mouseout(e) {
        e.style.cursor = "";
    }

    function Requirement_mouseover(e) {
        e.style.cursor = "pointer";
        e.style.textDecoration = "underline";
    }

    function Requirement_mouseout(e) {
        e.style.cursor = "";
        e.style.textDecoration = "none";
    }

    function GetRequirement(id) {

        $.ajax({
            type: "POST",
            contentType: "application/json",
            url: ROOT + "/services/requirement_Services.asmx/GetRequirementById",
            data: "{ 'id': '" + id + "' }",
            dataType: "json",
            success: function (oResponse) {
                if (oResponse.result == true) {
                    var sHTML = "";
                    var dataset = jQuery.parseJSON(oResponse.data)
                    var row = dataset.Rows[0];
                    ID = row.requirement_id;

                    $("#ReqID").val(row.requirement_id);
                    $("#ReqCount").val(row.counter);
                    $("#ReqName").val(row.name);
                    $("#ReqRef").val(row.reference_no);
                    $("#ReqOrder").val(row.sort_order);
                    $("#Keywords").val(row.keywords);
                    //$("#Clients").val(row.clients);
                    //$("#Version").val(row.version);
                    //                document.getElementById("Moniker").innerHTML = dataset.Row[0].moniker;
                    $("#ReqDetail").html(row.detail);

                    $("#New_btn").show();
                    $("#Edit").show();
                    $("#Delete").show();
                    $("#Print").show();
                }
            },
            error: ajaxFailed
        });

        GetAttachments(id);
    }

    function DeleteRequirement() {

        $.ajax({
            type: "POST",
            contentType: "application/json",
            url: ROOT + "/services/requirement_Services.asmx/DeleteRequirement",
            data: "{ 'id': '" + ID + "' }",
            dataType: "json",
            success: function (oResponse) {
                if (oResponse.result == true) {
                    RefreshTree();
                }
            },
            error: ajaxFailed
        });
    }

    function MoveRequirement(from, to) {

        $.ajax({
            type: "POST",
            contentType: "application/json",
            url: ROOT + "/services/requirement_Services.asmx/MoveRequirement",
            data: "{ 'from': '" + from + "', 'to': '" + to + "' }",
            dataType: "json",
            success: function (oResponse) {
                if (oResponse.result == true) {
                    RefreshTree();
                } else {
                    bootbox.alert(oResponse.message);
                }
            },
            error: ajaxFailed
        });
    }

    function Delete_Node(id) {
        var oNode = document.getElementById(id);
        var oParent = oNode.parentNode;
        oParent.parentNode.removeChild(oParent);
    }

    function GetAttachments(id) {

        $.ajax({
            type: "POST",
            contentType: "application/json",
            url: ROOT + "/services/requirement_Services.asmx/GetAttachments",
            data: "{ 'id': '" + id + "' }",
            dataType: "json",
            success: function (oResponse) {
                if (oResponse.result == true) {
                    var sHTML = "";
                    var dataset = jQuery.parseJSON(oResponse.data)

                    var Preview = [];
                    var Config = [];
                    for (var x = 0; x < dataset.Rows.length; x++) {
                        var row = dataset.Rows[x];
                        Preview[x] = ROOT + "/handlers/GetFile.ashx?id=" + row.attachment_id;
                        Config[x] = { caption: row.filename, width: "120px", size: row.size, url: ROOT + "/services/requirement_Services.asmx/DeleteAttachment", key: row.attachment_id };
                    }

                    CreateAttachments(Preview, Config);
                }
            },
            error: ajaxFailed
        });
    }

    function CreateAttachments(Preview, Config) {

        $("#input-ke-2").fileinput('destroy');

        var btns = '<button type="button" class="kv-cust-btn btn btn-xs btn-default" title="Download File" data-key="{dataKey}"><i class="glyphicon glyphicon-download"></i></button>';

        $("#input-ke-2").fileinput({
            theme: "explorer-fa",
            uploadUrl: ROOT + "/services/requirement_Services.asmx/SaveFile",
            uploadExtraData: function () {
                return { id: ID };
            },
            otherActionButtons: btns,
            showZoom: false,
            minFileCount: 1,
            maxFileCount: 10,
            overwriteInitial: false,
            previewFileIcon: '<i class="fa fa-file"></i>',
            initialPreviewAsData: false, // defaults markup  
            preferIconicPreview: true, // this will force thumbnails to display icons for following file extensions
            initialPreview: Preview,
            initialPreviewConfig: Config,
            previewFileIconSettings: { // configure your icon file extensions
                'doc': '<i class="fa fa-file-word-o text-primary"></i>',
                'xls': '<i class="fa fa-file-excel-o text-success"></i>',
                'ppt': '<i class="fa fa-file-powerpoint-o text-danger"></i>',
                'pdf': '<i class="fa fa-file-pdf-o text-danger"></i>',
                'zip': '<i class="fa fa-file-archive-o text-muted"></i>',
                'htm': '<i class="fa fa-file-code-o text-info"></i>',
                'txt': '<i class="fa fa-file-text-o text-info"></i>',
                'mov': '<i class="fa fa-file-movie-o text-warning"></i>',
                'mp3': '<i class="fa fa-file-audio-o text-warning"></i>',
                // note for these file types below no extension determination logic 
                // has been configured (the keys itself will be used as extensions)
                'jpg': '<i class="fa fa-file-photo-o text-danger"></i>',
                'gif': '<i class="fa fa-file-photo-o text-muted"></i>',
                'png': '<i class="fa fa-file-photo-o text-primary"></i>'
            },
            previewFileExtSettings: { // configure the logic for determining icon file extensions
                'doc': function (ext) {
                    return ext.match(/(doc|docx)$/i);
                },
                'xls': function (ext) {
                    return ext.match(/(xls|xlsx)$/i);
                },
                'ppt': function (ext) {
                    return ext.match(/(ppt|pptx)$/i);
                },
                'zip': function (ext) {
                    return ext.match(/(zip|rar|tar|gzip|gz|7z)$/i);
                },
                'htm': function (ext) {
                    return ext.match(/(htm|html)$/i);
                },
                'txt': function (ext) {
                    return ext.match(/(txt|ini|csv|java|php|js|css)$/i);
                },
                'mov': function (ext) {
                    return ext.match(/(avi|mpg|mkv|mov|mp4|3gp|webm|wmv)$/i);
                },
                'mp3': function (ext) {
                    return ext.match(/(mp3|wav)$/i);
                }
            }
        });

        $('#input-ke-2').on('fileuploaded', function (event, data, previewId, index) {
            if (data.response.result != true) {
                bootbox.alert(data);
            }
        });

        $('.kv-cust-btn').on('click', function () {
            var $btn = $(this), key = $btn.data('key');
            window.open(ROOT + "/handlers/GetFile.ashx?id=" + key, "_blank");
        });
    }

}
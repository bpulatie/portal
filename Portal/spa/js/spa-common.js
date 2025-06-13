
var myFORM;

$(function () {

    $('body').append(
        "<div id='canvasloader-container' class='spa-loading-image'></div>"
    );

    cl = new CanvasLoader('canvasloader-container');
    cl.setColor('#1E2A36'); // default is '#000000'
    cl.setShape('spiral'); // default is 'oval'
    cl.setDiameter(40); // default is 40
    cl.setFPS(18); // default is 24

    var loaderObj = document.getElementById("canvasLoader");
    loaderObj.style.position = "absolute";
    loaderObj.style["top"] = cl.getDiameter() * -0.5 + "px";
    loaderObj.style["left"] = cl.getDiameter() * -0.5 + "px";

    $('body').spaControls();
    myFORM = $('body').spaControls().data('plugin_spaControls');

});

function ShowSystemModal(title, message) {
    if (title != null) {
        $('#spa_system_modal_title').html(title);
    } else {
        $('#spa_system_modal_title').html("System Message");
    }

    if (title != null) {
        $('#spa_system_modal_message').html(message);
    } else {
        $('#spa_system_modal_title').html("Unknown Message");
    }

    $('#spa_system_modal').modal();
}

function spaConfirmationModal(message, callback, x) {
    if (message != undefined) {
        $('#spa_confirmation_modal_ok').unbind('click');
        $('#spa_confirmation_modal_message').html(message);
        $('#spa_confirmation_modal_ok').click(function (event) {
            if (typeof(eval(callback)) == "function") {
                var funcPtr = eval(callback);
                funcPtr();
            }
        });
        $('#spa_confirmation_modal').modal();
    } else {
        if (typeof(eval(callback)) == "function") {
            var funcPtr = eval(callback);
            funcPtr(x);
        }
    }
}

function ajaxSuccess(xmlRequest) {

}

function ajaxFailed(xmlRequest, textStatus) {
    if (typeof (cl) !== "undefined") {
        cl.hide();
    }

    ShowSystemModal('System Error', '<table>' +
                                '<tr><td width="150px"><b>Request Status: </b></td><td>' + xmlRequest.status + '</td></tr>' +
                                '<tr><td><b>Status Text: </b></td><td>' + xmlRequest.statusText + '</td></tr>' +
                                '<tr><td><b>Response Text: </b></td><td>' + xmlRequest.responseText + '</td></tr>' +
                                '<tr><td><b>Text Status: </b></td><td>' + textStatus + '</td></tr>' +
                                '</table>');
}

function parseJsonDate(jsonDateString) {
    if (jsonDateString === null) {
        return null;
    }
    var d = new Date(parseInt(jsonDateString.replace('/Date(', '')));
    if (d.getTime() === -2208970800000) {
        return null;
    } else {
        return d;
    }
}

function AppFuncExists(funcName) {
    return (eval("typeof( " + funcName + ")") == "function");
}

function FireEvent(id, eventName, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10) {
    var funcName = id + "_" + eventName;
    if (AppFuncExists(funcName)) {
        var funcPtr = eval(funcName);
        return funcPtr(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
    }
}

function BindTreeGridEvents(grid) {

    $('.glyphicon-triangle-bottom').unbind('click');
    $('.glyphicon-triangle-right').unbind('click');

    $('.glyphicon-triangle-bottom').on('click', function () {
        var level = $(this).attr('level');
        var key = $(this).parent().text();
        var id = level + "_" + key;
        $("#" + id).hide();
        $(this).removeClass('glyphicon-triangle-bottom');
        $(this).addClass('glyphicon-triangle-right');
        BindTreeGridEvents(grid);
    });

    $('.glyphicon-triangle-right').on('click', function () {
        var level = $(this).attr('level');
        var key = $(this).parent().text();
        var id = level + "_" + key;
        $("#" + id).show();
        $(this).removeClass('glyphicon-triangle-right');
        $(this).addClass('glyphicon-triangle-bottom');

        level = parseInt(level, 10) + 1;
        var cmd = 'grid.dataset.params = ' + '"pay_period_id": "' + ID + '", "level": "' + level + '", "company": "", "site_id": "", "job_id": ""';


        BindTreeGridEvents(grid);
    });
}

function hasAccessFeature(af) {
    for (var x = 0; x < CONTEXT.accessFeatures.length; x++) {
        if (CONTEXT.accessFeatures[x].access_name === af) {
            return true;
        }
    }
    return false;
}


function bindTreeGridEvents(g) {

    $('.spaTreeGrid.glyphicon-triangle-right', '#' + g.module_id).unbind('click');
    $('.spaTreeGrid.glyphicon-triangle-bottom', '#' + g.module_id).unbind('click');


    $('.spaTreeGrid.glyphicon-triangle-right', '#' + g.module_id).on('click', $.proxy(_treeExpand, null, g));
    $('.spaTreeGrid.glyphicon-triangle-bottom', '#' + g.module_id).on('click', $.proxy(_treeCollapse, null, g));

}

function _treeExpand(g) {
    var id = $(this).attr('level');

    if ($("#" + id)[0].childElementCount < 1) {

        var row = jQuery.parseJSON($(this).parent().parent().attr("row"));

        level = parseInt(row.level, 10) + 1;
        var params = '"psid": "", "level": "' + level + '", "p1": "' + row['p1'] + '", "p2": "' + row['p2'] + '", "p3": "' + row['p3'] + '", "p4": "' + row['p4'] + '", "p5": "' + row['p5'] + '"';

        var form = g.settings.form;
        var cmd1 = "form." + $(g.element).attr('dataset') + ".settings.params = params";
        eval(cmd1);
        var cmd2 = "form." + $(g.element).attr('dataset') + ".Reload()";
        eval(cmd2);
    }

    $("#" + id).show();
    $(this).removeClass('glyphicon-triangle-right');
    $(this).addClass('glyphicon-triangle-bottom');

    bindTreeGridEvents(g);
}

function _treeCollapse(g) {
    var id = $(this).attr('level');
    $("#" + id).hide();
    $(this).removeClass('glyphicon-triangle-bottom');
    $(this).addClass('glyphicon-triangle-right');

    bindTreeGridEvents(g);
}
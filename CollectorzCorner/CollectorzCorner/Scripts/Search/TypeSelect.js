$(document).ready(function () {
    var type = null;

    $("#TypeList").prepend("<option value='' selected='selected'></option>");
    $("#TypeList").change(function () {

        if (type != null)
            $("#" + type).hide();

        type = $("#TypeList option:selected").text();
        type = type.split(' ').join('');
        $("#" + type).show();
    });
});



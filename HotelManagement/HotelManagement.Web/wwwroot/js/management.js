$(document).on("click", "#createNoteButton", function () {
    debugger;
    $.getJSON("/Management/Management/CreateNote", function (data) {
        var myJSON = JSON.parse(data);
        console.log(myJSON);
        $.each(myJSON, function (i, val) {
            $("#department").append("<option>" + val + "</option>");
        });
    });
});
$(document).on("click", "#createNoteButton", function (event) {
    event.preventDefault();
    $('#noteModal').modal('show');

    $.getJSON('/Management/Management/CreateNote', { name: event.target.dataset.name }, function (retrievedCategoriesData) {
        var categoriesArray = retrievedCategoriesData.categories;
        var userLogbooksArray = retrievedCategoriesData.userLogbooks;

        var categoriesString = JSON.stringify(categoriesArray);
        var userLogbooksString = JSON.stringify(userLogbooksArray);
        var categoriesJson = JSON.parse(categoriesString);
        var userLogbooksJson = JSON.parse(userLogbooksString);

        $.each(categoriesJson, function (i, val) {
            if ($('#department option:contains(' + val + ')').length === 0) {
                $("#department").append("<option>" + val + "</option>");
            }
        });

        $.each(userLogbooksJson, function (i, val) {
            if ($('#pickLogbook option:contains(' + val + ')').length === 0) {
                $("#pickLogbook").append("<option>" + val + "</option>");
            }
        });
    });
});

$(document).ready(function () {
    // Construct URL object using current browser URL
    var url = new URL(document.location);

    // Get query parameters object
    var params = url.searchParams;

    // Get value of delivery results
    var specifiedLogbook = params.get("specifiedLogbook");
    console.log(specifiedLogbook);

    // Set it as the dropdown value
    if (specifiedLogbook === null) {
        $("#dropdownMenuButton").text('Select your option');
    } else {
        $("#dropdownMenuButton").text(specifiedLogbook);
    }

});

$(function () {
    const $createNoteForm = $('#createNoteForm');

    $createNoteForm.on('submit', function (event) {
        event.preventDefault();

        const dataToSend = $createNoteForm.serialize();
        console.log(dataToSend);
        console.log($createNoteForm.attr('action'));

        $.post($createNoteForm.attr('action'), dataToSend, function (serverData) {
            $('#createNoteForm')[0].reset();

            $('.pricing-table').prepend(serverData);

            $('.modal').modal('hide'); // used to hide the modal on success.
        })
    })
})

$(document).on("click", "#deleteNote", function (event) {
    var id = $(this).attr('data-Id')
    console.log(id);

    //$.ajax({
    //    url: '/Management/Management/DeleteNote',
    //    type: 'POST',
    //    data: id: id,
    //    success: function (result) {
    //        console.log(result);
    //    }
    //});
    $.post("/Management/Management/DeleteNote", { "data": id }, function () {
        $('#' + id).remove();
    })
});

//$(document).on("click", "#editNote", function (event) {
//    var idx = $(this).attr('data-Id')

//    $.post('Management/Management/Delete, dataToSend, function (serverData) {
//        $('#send-reply-form')[0].reset();
//        $(feedbackParentElement).prepend(serverData);

//        $('.modal').modal('hide'); // used to hide the modal on success.
//    })
//    //$(".modal-body #feedbackparentId").val(feedbackId);
//});

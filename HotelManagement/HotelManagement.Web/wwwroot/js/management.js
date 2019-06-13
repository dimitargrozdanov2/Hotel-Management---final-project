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

// setting value for drop down button text
$(document).ready(function () {
    // Construct URL object using current browser URL
    var url = new URL(document.location);

    // Get query parameters object
    var params = url.searchParams;

    // Get value of delivery results
    var specifiedLogbook = params.get("specifiedLogbook");

    // Set it as the dropdown value
    if (specifiedLogbook !== null) {
        $("#dropdownMenuButton").text(specifiedLogbook);
    }

});

$(function () {
    const $createNoteForm = $('#createNoteForm');

    $createNoteForm.on('submit', function (event) {
        event.preventDefault();

        const dataToSend = $createNoteForm.serialize();

        // gets the logbook you are looking at right now
        let currentLogbook = $("[name='currentLogbookOn']").val();
        // gets the picked logbook
        let pickedLogbook = $('#pickLogbook').val();

        // Perform other work here ...
        $.post($createNoteForm.attr('action'), dataToSend, function (serverData) {
            toastr["success"]("Your note has been posted!", "Note")
            console.log($('.pricing-table'));
            if ($('.pricing-table').find('#noNotes').length) {
                $('#noNotes').hide();
            }
            $('#createNoteForm')[0].reset();
            // compares, if the current logbook and picked logbook are the same, it will prepend the result
            if (currentLogbook == pickedLogbook) {
                console.log(serverData);
                $('.pricing-table').prepend(serverData);
            }
        })
            .done(function () {
                $('.modal').modal('hide'); // used to hide the modal on success.
            })

            .fail(function () {
                // when it fails
            })
            .always(function () {
            })
    })
})

$(document).on("click", "#deleteNote", function (event) {
    var id = $(this).attr('data-Id')

    swal({
        title: "Are you sure?",
        text: "Once deleted, only the administration will be able to recover the note!",
        icon: "warning",
        buttons: true,
        dangerMode: true,
    })
        .then((willDelete) => {
            if (willDelete) {
                $.post("/Management/Management/DeleteNote", { "data": id }, function () {
                    $('#' + id).remove();
                })
                swal("Poof! The note has been deleted!", {
                    icon: "success",
                });
            } else {
                swal("You decided to keep the note, good!", {
                    icon: "info",
                });
            }
        });

    
});


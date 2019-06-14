//$(document).on("click", "#createNoteButton", function (event) {
//    event.preventDefault();
//    $('#noteModal').modal('show');

//    $.getJSON('/Management/Management/CreateNote', { name: event.target.dataset.name }, function (retrievedCategoriesData) {
//        var categoriesArray = retrievedCategoriesData.categories;
//        var userLogbooksArray = retrievedCategoriesData.userLogbooks;

//        var categoriesString = JSON.stringify(categoriesArray);
//        var userLogbooksString = JSON.stringify(userLogbooksArray);
//        var categoriesJson = JSON.parse(categoriesString);
//        var userLogbooksJson = JSON.parse(userLogbooksString);

//        $.each(categoriesJson, function (i, val) {
//            if ($('#department option:contains(' + val + ')').length === 0) {
//                $("#department").append("<option>" + val + "</option>");
//            }
//        });

//        $.each(userLogbooksJson, function (i, val) {
//            if ($('#pickLogbook option:contains(' + val + ')').length === 0) {
//                $("#pickLogbook").append("<option>" + val + "</option>");
//            }
//        });
//    });
//});


//$(function () {
//    const $createNoteForm = $('#createNoteForm');

//    $createNoteForm.on('submit', function (event) {
//        event.preventDefault();

//        const dataToSend = $createNoteForm.serialize();

//        // gets the logbook you are looking at right now
//        let currentLogbook = $("[name='currentLogbookOn']").val();
//        // gets the picked logbook
//        let pickedLogbook = $('#pickLogbook').val();

//        // Perform other work here ...
//        $.post($createNoteForm.attr('action'), dataToSend, function (serverData) {
//            toastr["success"]("Your note has been posted!", "Note")
//            console.log($('.pricing-table'));
//            if ($('.pricing-table').find('#noNotes').length) {
//                $('#noNotes').hide();
//            }
//            $('#createNoteForm')[0].reset();
//            // compares, if the current logbook and picked logbook are the same, it will prepend the result
//            if (currentLogbook == pickedLogbook) {
//                console.log(serverData);
//                $('.pricing-table').prepend(serverData);
//            }
//        })
//            .done(function () {
//                $('.modal').modal('hide'); // used to hide the modal on success.
//            })

//            .fail(function () {
//                // when it fails
//            })
//            .always(function () {
//            })
//    })
//})

var connection =
    new signalR.HubConnectionBuilder()
        .withUrl("/notesHub")
        .build();

connection.on("NewMessage",
    function (message) {
        var currentLogbookValue = $('#dropdownMenuButton').text().trim();
        var selectedLogbook = message.logbook;

        if (currentLogbookValue === selectedLogbook) {
            if ($('.pricing-table').find('#noNotes').length) {
                $('#noNotes').hide();
            }
            var textToPrepend =
                "<div class='pricing-option' id='idPlace'>" +
                "<i class='material-icons'> mode_comment</i>" +
                "<h1 class='note-title' id='titlePlace'>" + message.email + "</h1>" +
                "<hr />" +
                "<p id='textPlace'>" + message.text + "</p>" +
                "<hr />" +
                "<p id='priorityPlace'>" + message.priority + "</p>" +
                "<hr />" +
                "<div class='price'>" +
                "<div class='front'>" +
                "<span class='note-title'>" + message.category + "</span>" +
                "</div>" +
                "<div class='back'>" +
                "<i class='material-icons' id='editNote' data-Id='" + message.id + "'>edit</i>" +
                "<i class='material-icons' id='deleteNote' data-Id='" + message.id + "'>remove_circle</i>" +
                "<i class='material-icons' id='archiveNote' data-Id='" + message.id + "'>archive</i>" +
                "</div>" +
                "</div>" +
                "</div>";

            $('.pricing-table').prepend(textToPrepend);

        }
        toastr["success"]("A new note has been posted in logbook: " + message.logbook, "Note")
        $('.modal').modal('hide');
        $('#createNoteForm')[0].reset();
    });

$(function () {
    const $createNoteForm = $('#createNoteForm');

    $createNoteForm.on('submit', function (event) {
        event.preventDefault();

        const dataToSend = $createNoteForm.serializeArray();
        var jsonData = {};
        $(dataToSend).each(function (index, obj) {
            jsonData[obj.name] = obj.value;
        });
        connection.invoke("Send", jsonData);
    })
})

$(document).on("click", "#createNoteButton", function (event) {
    event.preventDefault();
    $('#noteModal').modal('show');
    debugger;
    console.log(event.target);
    $.getJSON('/Management/Management/CreateNote', { name: event.target.dataset.name, logbookname: event.target.dataset.logbookname }, function (retrievedCategoriesData) {
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

connection.start().catch(function (err) {
    return console.error(err.toString());
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


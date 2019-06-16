﻿//$(document).on("click", "#createNoteButton", function (event) {
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

connection.on("handle_exception", function (err) {
    if (err.ClassName === "System.ArgumentException") {
        toastr["error"](err.Message, "Failed to create note!")
    }
});

connection.on("NewMessage",
    function (message) {
        var currentLogbookValue = $('#dropdownMenuButton').text().trim();
        var selectedLogbook = message.logbook;
        console.log(message);
        if (currentLogbookValue === selectedLogbook) {
            if ($('.pricing-table').find('#noNotes').length) {
                $('#noNotes').hide();
            }
            var textToPrepend =
                "<div class='pricing-option' id='" + message.id + "'>" +
                "<i class='fas fa-clipboard'></i>" +
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
                "<i class='fas fa-edit' id='editNote' data-Id='" + message.id + "'></i>" +
                "<i class='fas fa-trash-alt' id='deleteNote' data-Id='" + message.id + "'></i>" +
                "</div>" +
                "</div>" +
                "</div>";

            $('.pricing-table').prepend(textToPrepend);

        }
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

function updateCategories(logbookName) {
    $.getJSON('/Management/Management/CreateNote', {
        logbookName,
    }, function (retrievedCategoriesData) {
        var categoriesArray = retrievedCategoriesData.categories;

        var departmentSelect = $('#department');
        var categoryElements = categoriesArray.map(function (category) {
            return $('<option />').text(category);
        });

        departmentSelect.html(categoryElements);
    });
}

$(document).on("click", "#createNoteButton", function (event) {
    event.preventDefault();
    $('#noteModal').modal('show');
    updateCategories(event.target.dataset.logbookname);

    $('#pickLogbook').on('change', function (ev) {
        var selectedValue = ev.target.value;
        updateCategories(selectedValue);
    })
});

$('#noteModal').on('hidden.bs.modal', function () {
    $('#pickLogbook').off();
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
                
            })
                .done(function (dataResponse) {
                    $('#' + id).remove();
                    swal("Poof! The note has been deleted!", {
                        icon: "success",
                    });
                })
                .fail(function (dataResponse) {
                    toastr["error"](dataResponse.responseJSON.Message, "Failed to delete note!")
                })
        } else {
            swal("You decided to keep the note, good!", {
                icon: "info",
            });
        }
    });
});


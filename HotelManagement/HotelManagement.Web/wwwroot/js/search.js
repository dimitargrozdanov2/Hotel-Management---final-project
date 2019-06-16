$("#target").keyup(function (event) {
    var userIdentity = event.target.dataset.id;
    var searchByValue = $('#searchBySelector option:selected').text();

    if ($(this).val().length >= 2) {
        var request = $.ajax({
            url: "/Management/Management/GetNotesAsyncJson",
            type: "POST",
            data: { "data": event.target.value, "userIdentity": userIdentity, "searchByValue": searchByValue },
            dataType: 'json'
        });

        request.done(function (data) {
            $('.pricing-table').empty();
            $('#resultsCount').html(`Results found: ${data.length}`);
            if (data.length == 0) {
                $('.pricing-table').prepend('<p id="notShownNotes" class="mt-5">I couldnt find any notes that matched the requirements, please try again!</p>')
            }
            $.each(data, function (index) {
                var textToPrepend =
                    "<div class='pricing-option' id='" + data[index].id + "'>" +
                    "<i class='fas fa-clipboard'></i>" +
                    "<h1 class='note-title' id='titlePlace'>" + data[index].user.email + "</h1>" +
                    "<hr />" +
                    "<p id='textPlace'>" + data[index].text + "</p>" +
                    "<hr />" +
                    "<p id='priorityPlace'>" + data[index].priorityType + "</p>" +
                    "<hr />" +
                    "<div class='price'>" +
                    "<div class='front'>" +
                    "<span class='note-title'>" + data[index].category.name + "</span>" +
                    "</div>" +
                    "<div class='back'>" +
                    "<i class='fas fa-edit' id='editNote' data-Id='" + data[index].id + "'></i>" +
                    "<i class='fas fa-trash-alt' id='deleteNote' data-Id='" + data[index].id + "'></i>" +
                    "</div>" +
                    "</div>" +
                    "</div>";

                $('.pricing-table').prepend(textToPrepend)
            });
        })
        request.fail(function (data) {
            console.log(data);
            console.log(data.responseText)
        })
    }
});


$("#target").keyup(function (event) {
    //console.log("Handler for .keypress() called.");
    //console.log(event.target.value);
    var userIdentity = event.target.dataset.id;

    if ($(this).val().length >= 2) {
        var request = $.ajax({
            url: "/Management/Management/GetNotesAsyncJson",
            type: "POST",
            data: { "data": event.target.value, "userIdentity": userIdentity },
            dataType: 'json'
        });

        request.done(function (data) {
            console.log('success');
            $.each(data, function (index) {
                console.log(data[index]);
                // alert("id= "+data[index].id+" name="+data[index].name);
                //$('#myTable tbody').append("<tr class='child'><td>" + data[index].id + "</td><td>" + data[index].name + "</td></tr>");
                var textToPrepend =
                    "<div class='pricing-option' id='idPlace'>" +
                    "<i class='material-icons'> mode_comment</i>" +
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
                    "<i class='material-icons' id='editNote' data-Id='" + data[index].id + "'>edit</i>" +
                    "<i class='material-icons' id='deleteNote' data-Id='" + data[index].id + "'>remove_circle</i>" +
                    "<i class='material-icons' id='archiveNote' data-Id='" + data[index].id + "'>archive</i>" +
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

    //$.ajax({
    //    url: "/Management/Management/GetNotesAsyncJson",
    //    type: "POST",
    //    data: { "data": event.target.value },
    //    dataType: 'json',
    //    success: function (data) {
    //        console.log('success');
    //        $.each(data, function (index) {
    //            console.log(data[index].text);
    //            console.log(data[index].id)
    //            // alert("id= "+data[index].id+" name="+data[index].name);
    //            //$('#myTable tbody').append("<tr class='child'><td>" + data[index].id + "</td><td>" + data[index].name + "</td></tr>");
    //            var textToPrepend =
    //                "<div class='pricing-option' id='idPlace'>" +
    //                    "<i class='material-icons'> mode_comment</i>" +
    //                    "<h1 class='note-title' id='titlePlace'>admin@admin.admin</h1>" +
    //                    "<hr />" +
    //                    "<p id='textPlace'>" + data[index].text + "</p>" +
    //                    "<hr />" +
    //                    "<p id='priorityPlace'>" + data[index].prioritytype + "</p>" +
    //                    "<hr />" +
    //                    "<div class='price'>" +
    //                        "<div class='front'>" +
    //                            "<span class='note-title'>@Model.Category?.Name </span>" +
    //                        "</div>" +
    //                        "<div class='back'>" +
    //                            "<i class='material-icons' id='editNote' data-Id='@Model.Id'>edit</i>" +
    //                            "<i class='material-icons' id='deleteNote' data-Id='@Model.Id'>remove_circle</i>" +
    //                            "<i class='material-icons' id='archiveNote'>archive</i>" +
    //                        "</div>" +
    //                    "</div>" +
    //                "</div>";

    //            $('.pricing-table').prepend(textToPrepend)
    //        });

    //    },
    //    //failure: function (response) {
    //    //    alert(response.responseText);
    //    //},
    //    error: function (response) {
    //        console.log(response.responseText);
    //    }
    //});
    }
});


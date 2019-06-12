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

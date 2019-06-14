//$('.promote-user-form').on('submit', function (event) {
//    event.preventDefault();
//    console.log('test');

//    //var myformdata = $(".promote-user-form").serialize();

//    if (!$(this).valid()) {
//        return;
//    }
//    console.log(myformdata)

//});




$('.open-promote-user').on('click', function () {
    var userid = $(this).attr('att');
    console.log(userid);
    $('#UserId').val(userid);
});

$('#PromoteUserRecord').on('click', function (event) {
    event.preventDefault();
    var myformdata = $("#formPromote").serialize();

  //  console.log(myformdata);
    //if (!$(this).valid()) {
    //    return;
    //}

    $.post("/Administration/Admin/PromoteUser", myformdata)
        .done(function (dataResponse) {
            toastr.success("User promoted succesfully");
            $("#MyModal2").modal('hide');
            var idOfRoleTd = "#role-" + dataResponse.userId;
            var text = $(idOfRoleTd).text();
            if (text !== '') {
                dataResponse.roleName = ', ' + dataResponse.roleName;
            }
            $(idOfRoleTd).append(dataResponse.roleName);


        }).fail(function (dataResponse) {
            console.log(dataResponse);
            toastr.error(dataResponse.responseJSON.message);

        });
});








//$("#PromoteUser").click(function (event) {
//    event.preventDefault();

//    var $this = $(this);
//    console.log($this);

//    var idOfUserToDelete = $this.attr('att');

//    console.log(idOfUserToDelete);
//    var antiForgery = ($('#anti-forgery-span').find('input'))[0];

//    var postData = "userId=" + idOfUserToDelete + "&" + antiForgery.name + "=" + antiForgery.value;

//    //var myformdata = $("#formPromote").serialize();
//    console.log(postData);
//    $.post("/Administration/Admin/PromoteUser", myformdata)
//        .done(function (dataResponse) {

//            toastr.success("User succesfully promoted");
//            $("#MyModal2").modal('hide');
//            $test = dataResponse.attr('RoleName');
//            console.log($test);
//            $(".roleofUser").append(dataResponse);


//        }).fail(function (dataResponse) {
//            toastr.error("User promotion failed");

//        });
//});
$('.promote-user-form').on('submit', function (event) {
    event.preventDefault();

    var myformdata = $(".promote-user-form").serialize();
    console.log(myformdata);
    if (!$(this).valid()) {
        return;
    }

    $.post("/Administration/Admin/PromoteUser", myformdata)
        .done(function (dataResponse) {
            toastr.success("User promoted succesfully");
            $("#MyModal2").modal('hide');
            $(".roleofUser").html = ("test");


        }).fail(function (dataResponse) {
            toastr.error("User promotion failed");

        });
        });








//$("#PromoteUser").click(function (event) {
//    event.preventDefault();

//    //var $this = $(this);
//    //console.log($this);

//    //var idOfUserToDelete = $this.attr('att');

//    //console.log(idOfUserToDelete);
//    //var antiForgery = ($('#anti-forgery-span').find('input'))[0];

//    //var postData = "userId=" + idOfUserToDelete + "&" + antiForgery.name + "=" + antiForgery.value;

//    var myformdata = $("#formPromote").serialize();
//    console.log(myformdata);
//    $.post("/Administration/Admin/PromoteUser", myformdata)
//        .done(function (dataResponse) {

//            toastr.success("User succesfully promoted");
//            $("#MyModal2").modal('hide');
//            $test = dataResponse.attr('RoleName');
//            console.log($test);
//            $(".roleofUser").append(dataResponse);


//        }).fail(function (dataResponse) {
//            toastr.error("User promotion failed");

//            }
//        });
//})
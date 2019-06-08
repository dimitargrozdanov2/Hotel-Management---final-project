$(function () {
    const $commentForm = $('#send-feedback-form');

    $commentForm.on('submit', function (event) {
        event.preventDefault();
        const dataToSend = $commentForm.serialize();

        $.post($commentForm.attr('action'), dataToSend, function (serverData) {
            $('.comments-area').prepend(serverData);

            const commentCount = $('.single-comment').length
            $('#amount').html(commentCount);
        })
    })
})

// using this to set the id of the feedback someone is replying to, in the modal, otherwise the modal will always take the first feedback id.
$(document).on("click", "#reply-button", function () {
    var myBookId = $(this).data('id');
    $(".modal-body #feedbackparentId").val(myBookId);
    console.log(myBookId);
});

// reply ajax;
$(function () {
    const $replyForm = $('#send-reply-form');
    $replyForm.on('submit', function (event) {
        event.preventDefault();
        const dataToSend = $replyForm.serialize();

        $.post($replyForm.attr('action'), dataToSend, function (serverData) {
            $('.comments-list').prepend(serverData);
        })
    })
})

//$(function () {
//    $('.comment-list').dblclick(function () {
//        var contentPanelId = jQuery(this).attr("id");
//        console.log(contentPanelId);
//    })
//})


// scrolling to the top parent of the comment section upon clicking on the comment section
$(".comments-area").click(function () {
    $('html, body').stop().animate({
        scrollTop: $("#comments-amount").offset().top
    }, 1000);
});





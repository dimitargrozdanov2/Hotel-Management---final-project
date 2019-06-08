$(function () {
    const $commentForm = $('#send-feedback-form');

    $commentForm.on('submit', function (event) {
        event.preventDefault();
        const dataToSend = $commentForm.serialize();

        $.post($commentForm.attr('action'), dataToSend, function (serverData) {
            if ($('.comments-area').find('#noBusinesses').length) {
                $('#noBusinesses').hide();
            }
            // used to clear modal input fields.
            $('#send-feedback-form')[0].reset();

            $('.comments-area').prepend(serverData);

            const commentCount = $('.feedbackCom').length
            $('#amount').html(commentCount);
        })
    })
})

// using this to set the id of the feedback someone is replying to, in the modal, otherwise the modal will always take the first feedback id.
$(document).on("click", "#reply-button", function () {
    var feedbackId = $(this).data('id');
    $(".modal-body #feedbackparentId").val(feedbackId);
    console.log(feedbackId);
});

// reply ajax;
$(function () {
    const $replyForm = $('#send-reply-form');

    $replyForm.on('submit', function (event) {
        event.preventDefault();
        const dataToSend = $replyForm.serialize();

        const dataArray = $replyForm.serializeArray();
        var feedbackId = dataArray[0].value;

        const feedbackParentElement = $('#' + feedbackId);

        $.post($replyForm.attr('action'), dataToSend, function (serverData) {
            $('#send-reply-form')[0].reset();
            $(feedbackParentElement).prepend(serverData);
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





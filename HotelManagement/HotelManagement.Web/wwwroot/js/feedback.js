$(function () {
    const $commentForm = $('#send-feedback-form');

    $commentForm.on('submit', function (event) {
        event.preventDefault();
        debugger;
        const dataToSend = $commentForm.serialize();

        $.post($commentForm.attr('action'), dataToSend, function (serverData) {
            $('.comments-area').prepend(serverData);

            const commentCount = $('.single-comment').length
            $('#amount').html(commentCount);
        })
    })
})

// reply ajax;

$(function () {
    $('.comment-list').dblclick(function () {
        var contentPanelId = jQuery(this).attr("id");
        console.log(contentPanelId);
    })
})

//$(function () {
//    const $replyForm = $('#send-reply-form');

//    $replyForm.on('submit', function (event) {
//        event.preventDefault();
//        debugger;
//        const dataToSend = $replyForm.serialize();

//        $.post($replyForm.attr('action'), dataToSend, function (serverData) {
//            $('.comments-list').prepend(serverData);
//        })
//    })
//})

$(".comments-area").click(function () {
    $('html, body').stop().animate({
        scrollTop: $("#comments-amount").offset().top
    }, 1000);
});





$(function () {
    const $commentForm = $('#send-feedback-form');

    $commentForm.on('submit', function (event) {
        event.preventDefault();
        const dataToSend = $commentForm.serialize();

        $.post($commentForm.attr('action'), dataToSend, function (serverData) {
            console.log(serverData);
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

// using this to set the id of the feedback someone is replying to, in the modal, 
        //otherwise the modal will always take the first feedback id.
$(document).on("click", "#reply-button", function () {
    var feedbackId = $(this).data('id');

    $(".modal-body #feedbackparentId").val(feedbackId);
});

// reply ajax;
$(function () {
    const $replyForm = $('#send-reply-form');

    // $(document).on('submit' , '#send-reply-form' , function(event){
    $replyForm.on('submit', function (event) {
        event.preventDefault();
        const dataToSend = $replyForm.serialize();
        // get feedbackParent ID from event;
        const dataArray = $replyForm.serializeArray();
        var feedbackId = dataArray[0].value;

        const feedbackParentElement = $('#' + feedbackId);

        $.post($replyForm.attr('action'), dataToSend, function (serverData) {
            debugger;
            console.log(serverData);
            $('#send-reply-form')[0].reset();
            $(feedbackParentElement).prepend(serverData);
        })
    })
})


//scroll button
jQuery(document).ready(function () {

    var btn = $('#button');

    $(window).scroll(function () {
        if ($(window).scrollTop() > 400) {
            btn.addClass('show');
        } else {
            btn.removeClass('show');
        }
    });

    btn.on('click', function (e) {
        e.preventDefault();
        $('html, body').stop().animate({
            scrollTop: $("#comments-amount").offset().top
        }, 1000);
    });
});





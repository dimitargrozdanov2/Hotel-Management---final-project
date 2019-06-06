$(function () {
    const $commentForm = $('#send-feedback-form');
    console.log($commentForm);

    $commentForm.on('submit', function (event) {
        event.preventDefault();
        debugger;
        const dataToSend = $commentForm.serialize();

        $.post($commentForm.attr('action'), dataToSend, function (serverData) {
            $('.comments-area').prepend(serverData);

            const commentCount = $('.single-comment').length
            $('#amount').html(commentCount);
        })


    }
})
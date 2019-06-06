$(function () {
    const $commentForm = $('.comment-form');

    $commentForm.on('submit', function (event) {
        event.preventDefault();

        const dataToSend = $('.comment-form').serialize();

        $.post($commentForm.attr('action'), dataToSend, function (serverData) {
            debugger;
            $('.comments-area').prepend(serverData);
        })
    })
})
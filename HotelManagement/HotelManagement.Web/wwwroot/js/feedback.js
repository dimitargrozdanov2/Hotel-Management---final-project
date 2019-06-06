$(function () {
    const $commentForm = $('#send-feedback-form');
    console.log($commentForm);

    $commentForm.on('submit', function (event) {
        event.preventDefault();
        debugger;
        const dataToSend = $commentForm.serialize();

        $.post($commentForm.attr('action'), dataToSend, function (serverData) {
            debugger;
            $('.comments-area').append(serverData);
        })
    })
})
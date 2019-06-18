$(function () {
    const $commentForm = $('#send-feedback-form');

    $commentForm.on('submit', function (event) {
        event.preventDefault();
        const dataToSend = $commentForm.serialize();

        $.post($commentForm.attr('action'), dataToSend, function (serverData) {
        })
            .done(function (serverData) {
                if ($('.comments-area').find('#noBusinesses').length) {
                    $('#noBusinesses').hide();
                }
                // used to clear modal input fields.
                $('#send-feedback-form')[0].reset();

                $('.comments-area').prepend(serverData);

                toastr["success"]("You have submitted your feedback successfully!", "Business Feedback")

                const commentCount = $('.feedbackCom').length
                $('#amount').html(commentCount);

                $('.modal').modal('hide'); // used to hide the modal on success.
            })
            .fail(function (dataResponse) {
                toastr["error"](dataResponse.responseJSON.Message, "Failed to submit feedback!")
            })
    });
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

    $replyForm.on('submit', function (event) {
        event.preventDefault();
        const dataToSend = $replyForm.serialize();

        var feedbackParentId = $(this).find('input:hidden').val();
        const feedbackParentElement = $('#reply-button[data-id="' + feedbackParentId + '"]').parent().parent().parent();
        console.log(feedbackParentElement);

        $.post($replyForm.attr('action'), dataToSend)
            .done(function (serverData) {
                toastr["success"]("Reply posted...", "Feedback Reply")
                console.log(serverData);

                $('#send-reply-form')[0].reset();
                $(feedbackParentElement).append(serverData);

                $('.modal').modal('hide'); // used to hide the modal on success.
            })
            .fail(function (dataResponse) {
                toastr["error"](dataResponse.responseJSON.Message, "Failed to submit reply!")
            })
    })
})
//console.log($('.comments-area').children().length)
//console.log($('.comments-area').find('#noBusinesses').length);

// delete MAIN feedback button for moderators
$(document).on("click", "#delete-button", function (event) {
    var id = $(this).attr('data-headCommentId');
    swal({
        title: "Are you sure?",
        text: "Once deleted, only the administration will be able to recover the comment!",
        icon: "warning",
        buttons: true,
        dangerMode: true,
    })
        .then((willDelete) => {
            if (willDelete) {
                $.post("/Business/Business/DeleteFeedback", { "data": id }, function () {
                    $('#delete-button[data-headCommentId="' + id + '"]').parent().parent().parent().remove();
                    if ($('.comments-area').children().length === 0 && $('.comments-area').find('#noBusinesses').length === 0) {
                        if ($('.comments-area').length === 0) {
                            $('#comments-amount').append('<p id="noBusinesses" class="comments-area">This business has no feedback. Be the first to submit one!</p>');
                        } else {
                            $('.comments-area').append('<p id="noBusinesses">This business has no feedback. Be the first to submit one!</p>');
                        }
                    } else if ($('.comments-area').children().length === 1 && $('#noBusinesses').is(':hidden')) {
                        $('#noBusinesses').show();
                    }

                    const commentCount = $('.feedbackCom').length
                    $('#amount').html(commentCount);
                })
                swal("Poof! The comment has been deleted!", {
                    icon: "success",
                });
            } else {
                swal("You decided to keep the comment, good!", {
                    icon: "info",
                });
            }
        });
});

// delete REPLY feedback button for moderators
$(document).on("click", "#delete-reply-button", function (event) {
    var id = $(this).attr('data-headCommentId');

    swal({
        title: "Are you sure?",
        text: "Once deleted, only the administration will be able to recover the comment!",
        icon: "warning",
        buttons: true,
        dangerMode: true,
    })
        .then((willDelete) => {
            if (willDelete) {
                $.post("/Business/Business/DeleteFeedback", { "data": id }, function () {
                    console.log($('*[data-headCommentId="' + id + '"]').parent());
                    var element = $('*[data-headCommentId="' + id + '"]').parent().remove();
                })
                swal("Poof! The comment has been deleted!", {
                    icon: "success",
                });
            } else {
                swal("You decided to keep the comment, good!", {
                    icon: "info",
                });
            }
        });
});

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
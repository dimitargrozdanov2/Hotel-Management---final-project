// Write your JavaScript code.
$('#admindropdown').on('mouseenter', () => {
    $('#admindropdown-menu').show();
});
$('#admindropdown').on('mouseleave', () => {
    $('#admindropdown-menu').hide();
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
        $('html, body').animate({ scrollTop: 0 }, '100'); // could scroll to the top;
    });
});


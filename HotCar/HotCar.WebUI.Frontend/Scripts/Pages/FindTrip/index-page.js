$(function () {
    $('html, body').animate({
        scrollTop: $("#main-content-header").offset().top
    }, 800);

    $("form").bind("keypress", function (e) {
        if (e.keyCode == 13) {
            $("#btnFind").attr('value');
            return false;
        }
    });
});

google.maps.event.addDomListener(window, 'load', function () {
    new google.maps.places.Autocomplete(document.getElementById('from-address'));
    new google.maps.places.Autocomplete(document.getElementById('to-address'));
});
$(document).ready(function () {

    var onMenuClick = function (status, $menuElement, $contentElement) {
        var scrollTimeMs = 1000;
        $menuElement.click(function () {

            $contentElement.slideToggle('slow');
            $('html, body').animate({
                scrollTop: $contentElement.offset().top - 100
            }, scrollTimeMs);

            if (status === false) {
                $menuElement.addClass('selected-menu');
                $menuElement.css('color', 'lightgrey');
                status = true;
            } else {
                $menuElement.removeClass('selected-menu');
                $menuElement.css('color', 'white');
                status = false;
            }
        });
    }

    var isOnSettingsMenu = false;
    var isOnTripsMenu = false;
    var isOnApplMenu = false;
    var isOnCommnetsMenu = false;
    onMenuClick(isOnSettingsMenu, $('#settings-menu'), $('#settings-content'));
    onMenuClick(isOnTripsMenu, $('#trips-menu'), $('#trips-content'));
    onMenuClick(isOnApplMenu, $('#appl-menu'), $('#appl-content'));
    onMenuClick(isOnCommnetsMenu, $('#comments-menu'), $('#comments-content'));
});
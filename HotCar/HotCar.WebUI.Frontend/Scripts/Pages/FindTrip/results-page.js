$(function () {

    $(document).on("click", '.pagination a', function () {
        var page;
        if ($(this).parent("li").attr('id') == 'prev_page') {
            page = parseInt($('#currPage').val()) - 1;
        } else if ($(this).parent("li").attr('id') == 'next_page') {
            page = parseInt($('#currPage').val()) + 1;
        } else {
            page = parseInt($(this).text()) - 1;
        }
        filterData( page );
        return false;
    });

    scrollToTop();

    $('#dp2').datepicker({ dateFormat: "dd/mm/yy", minDate: new Date() });

    $('#dp2').focus(function () {
        $("#hour-slider-range").hide();
        if ($(this).val() != '') {
            $("#dataclear").show();
        }
    });

    $('#dp2').blur(function () {
        $("#hour-slider-range").show();
        if ($(this).val() == '') {
            $("#dataclear").hide();
        }
    })

    $("#hour-slider-range").slider({
        range: true,
        min: 0,
        max: 24,
        values: [0, 24],
        slide: function (event, ui) {
            if (ui.values[0] == ui.values[1]) {
                return false;
            }
            $("#from_hour").text(ui.values[0]);
            $("#to_hour").text(ui.values[1]);
        }
    });

    $("#dataclear").hide();

    $("#dataclear").click(function () {
        $("#dp2").val('');
        $(this).hide();
        filterData();
    });

    $('.filter').change(function () {
        filterData();
    });



    $('#hour-slider-range').on('slidestop', function () {
        filterData();
    });

    function filterData(page) {

        var filtersObj = {};

        if ($('#hide_no_seats').is(":checked")) {
            filtersObj.HideWithNoSeats = true;
        }
        else {
            filtersObj.HideWithNoSeats = false;
        }

        filtersObj.Date = $('#dp2').val();

        filtersObj.FromHour = $('#from_hour').text();

        filtersObj.ToHour = $('#to_hour').text();

        if ($('#withPhotoRadio').is(":checked")) {
            filtersObj.HideWithNoPhoto = true;
        }
        else {
            filtersObj.HideWithNoPhoto = false;
        }

        $('#trip_infos').addClass('text-center');
        $('#trip_infos').html("<img src='../Content/images/ajax-loader[1].gif' />");

        var data = JSON.stringify({ 'page': page, 'filters': filtersObj })

        $.ajax({
            url: '/FindTrip/Results',
            type: "POST",
            contentType: 'application/json',
            data: data,
            success: function (response) {
                scrollToTop();
                $('#trip_infos').removeClass('text-center');
                $('#trip_infos').html(response);
                updateFilterCounters();
            }
        });
    }

    function updateFilterCounters() {
        $('#total_count').text($('#all_trips_count').text());
        $noSeatsCount = $('#noSeatsNum').val();
        $("#no_seats_count").text($noSeatsCount);
        $withPhotoCount = $('#PhotoNum').val();
        $("#with_photo_count").text($withPhotoCount);
        $totalCount = $('#TotalNum').val();
        $("#total_count").text($totalCount);
    }

    function scrollToTop() {
        $('html, body').animate({
            scrollTop: $("#main-content").offset().top
        }, 800);
    }

    $(document).on("click", '.trip-info', function () {
        window.location.href = "/FindTrip/TripDetail/" + $(this).attr('id');
    });
});
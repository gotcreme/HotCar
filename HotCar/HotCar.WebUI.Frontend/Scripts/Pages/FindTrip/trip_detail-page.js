$(function () {

    $("#showMap").attr('title', 'Показати маршрут на карті');

    $('#accordion2').on('shown.bs.collapse', function (e) {
        MapStart();
    });

    $('#toPrevPage').click(function () {
        if (document.referrer.indexOf(window.location.hostname) != -1) {
            parent.history.back();
            return false;
        }
    });

    $('html, body').animate({
        scrollTop: $("#main-content").offset().top
    }, 800);
});


var map;
var directionsService;
var renders = [];
var requests = [];
var routes = [];

function MapStart() {

    routes = [];
    requests = [];

    for (var a = 0; a < renders.length; a++) {
        renders[a].setMap(null);
    }

    renders = [];

    var suggestedRoute = [];

    $('[id^=hiddenWayPoint]').each(function () {
        suggestedRoute.push($(this).val());
    });

    var newRoute = [];

    for (var i = 0, j = 0; i < suggestedRoute.length; i++) {
        if (suggestedRoute[i] != undefined && suggestedRoute[i] != "") {
            newRoute[j++] = suggestedRoute[i];
        }
    }

    if (newRoute.length > 1) {

        var latlng = new google.maps.LatLng(49.396675, 31.92627);

        var options = {
            zoom: 5,
            center: latlng,
            mapTypeId: google.maps.MapTypeId.ROADMAP
        };

        map = new google.maps.Map(document.getElementById("map-canvas"), options);

        directionsService = new google.maps.DirectionsService();

        routes.push({ Points: newRoute });

        ProcessRequests();
    }
}

function ProcessRequests() {

    for (var route = 0; route < routes.length; route++) {
        var wayPoints = [];

        var start, finish;

        var lastpoint;

        var data = routes[route].Points;

        for (var waypoint = 0; waypoint < data.length; waypoint++) {
            if (data[waypoint] === lastpoint) {
                continue;
            }

            lastpoint = data[waypoint];

            wayPoints.push({
                location: data[waypoint],
                stopover: true
            });
        }

        start = (wayPoints.shift()).location;
        finish = wayPoints.pop();

        if (finish === undefined) {
            finish = start;
        } else {
            finish = finish.location;
        }

        var request = {
            origin: start,
            destination: finish,
            waypoints: wayPoints,
            travelMode: google.maps.TravelMode.DRIVING
        };


        requests.push({ "route": route, "request": request });
    }

    ProcessRequest();
}

function ProcessRequest() {

    var i = 0;

    function submitRequest() {
        directionsService.route(requests[i].request, directionResults);
    }

    function directionResults(result, status) {
        if (status == google.maps.DirectionsStatus.OK) {

            renders[i] = new google.maps.DirectionsRenderer(
            {
                suppressMarkers: false,
                suppressInfoWindows: false
            });

            renders[i].setDirections(result);
            renders[i].setMap(map);
        }

        nextRequest();
    }

    function nextRequest() {
        i++;

        if (i >= requests.length) {

            return;
        }

        submitRequest();
    }

    submitRequest();
}























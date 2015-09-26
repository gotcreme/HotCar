var map;
var directionsService;
var renders = [];
var requests = [];
var routes = [];
var suggestedRoute = [];


function InitializeMap() {

    var latlng = new google.maps.LatLng(49.396675, 31.92627);

    var options = {
        zoom: 5,
        center: latlng,
        mapTypeId: google.maps.MapTypeId.ROADMAP
    };

    map = new google.maps.Map(document.getElementById("map-canvas"), options);

    AddAutocomplete();
}

function MapStart() {

    routes = [];
    requests = [];

    for (var a = 0; a < renders.length; a++) {
        renders[a].setMap(null);
    }

    renders = [];

    var newRoute = [];

    for (var i = 0, j = 0; i < suggestedRoute.length; i++) {
        if (suggestedRoute[i] != undefined && suggestedRoute[i] != "") {
            newRoute[j++] = suggestedRoute[i];
        }
    }

    if (newRoute.length > 1) {
        
        directionsService = new google.maps.DirectionsService();

        routes.push({ Points: newRoute });

        AddAutocomplete();

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

function AddAutocomplete() {
    var points = [];

    points.push(document.getElementById("wayPoint0"));
    points.push(document.getElementById("wayPoint1"));
    points.push(document.getElementById("wayPoint2"));
    points.push(document.getElementById("wayPoint3"));
    points.push(document.getElementById("wayPoint4"));
    points.push(document.getElementById("wayPoint5"));
    points.push(document.getElementById("wayPoint6"));
    points.push(document.getElementById("wayPoint7"));


    var autocompletePoints = [];
    for (var a = 0; a < points.length; a++) {
        autocompletePoints[a] = new google.maps.places.Autocomplete(points[a]);
        autocompletePoints[a].bindTo("bounds", map);
    }


    google.maps.event.addListener(autocompletePoints[0], "place_changed", function () {
        suggestedRoute[0] = document.getElementById("wayPoint0").value;
        MapStart();
    });


    google.maps.event.addListener(autocompletePoints[1], "place_changed", function () {
        suggestedRoute[1] = document.getElementById("wayPoint1").value;
        MapStart();
    });

    google.maps.event.addListener(autocompletePoints[2], "place_changed", function () {
        suggestedRoute[2] = document.getElementById("wayPoint2").value;
        MapStart();
    });

    google.maps.event.addListener(autocompletePoints[3], "place_changed", function () {
        suggestedRoute[3] = document.getElementById("wayPoint3").value;
        MapStart();
    });

    google.maps.event.addListener(autocompletePoints[4], "place_changed", function () {
        suggestedRoute[4] = document.getElementById("wayPoint4").value;
        MapStart();
    });

    google.maps.event.addListener(autocompletePoints[5], "place_changed", function () {
        suggestedRoute[5] = document.getElementById("wayPoint5").value;
        MapStart();
    });

    google.maps.event.addListener(autocompletePoints[6], "place_changed", function () {
        suggestedRoute[6] = document.getElementById("wayPoint6").value;
        MapStart();
    });

    google.maps.event.addListener(autocompletePoints[7], "place_changed", function () {
        suggestedRoute[7] = document.getElementById("wayPoint7").value;
        MapStart();
    });
}

google.maps.event.addDomListener(window, "load", function () {
    InitializeMap();
});

function CheckInput() {

    var pointValues = [];

    pointValues[0] = document.getElementById("wayPoint0").value;
    pointValues[1] = document.getElementById("wayPoint1").value;
    pointValues[2] = document.getElementById("wayPoint2").value;
    pointValues[3] = document.getElementById("wayPoint3").value;
    pointValues[4] = document.getElementById("wayPoint4").value;
    pointValues[5] = document.getElementById("wayPoint5").value;
    pointValues[6] = document.getElementById("wayPoint6").value;
    pointValues[7] = document.getElementById("wayPoint7").value;

    var needChange = false;
    for (var a = 0; a < pointValues.length; a++) {
        if (pointValues[a] == undefined || pointValues[a] == "") {
            if (suggestedRoute[a] != undefined && suggestedRoute[a] != "") {
                needChange = true;
            }
            suggestedRoute[a] = undefined;
        }
    }

    if (needChange) {
        MapStart();
    }
}

$(function () {
    $('html, body').animate({
        scrollTop: $("#main").offset().top
    }, 800);


    $("#date").datepicker({ dateFormat: "dd/MM/yy", minDate: new Date() });

    $("form").bind("keypress", function (e) {
        if (e.keyCode == 13) {
            $("#btnFind").attr('value');
            return false;
        }
    });

    $('.date').change(function () {

        var $date = $('#date').val();
        var $hour = $('#hour').val();
        var $minute = $('#minute').val();

        $('#dateTime').val($date + " " + $hour + ":" + $minute);
    });
});



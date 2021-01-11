//$.getJSON("https://ipfind.co/?ip=49.145.79.113&auth=4c4b618d-bcfb-4dd6-b5be-6d899a771fe6", function (result) {
//	console.log('res', result);
//});
var prevLocationId = "";

$(document).ready(function () {

    var userLocation = "";

    // Get Client IP Address
    GetIPinfo();


    //weatherwidget();
    //displayWidget();

    function GetIPinfo() {
        var link = "/Weather/GetIPinfo/";
        $.ajax({
            url: link,
            dataType: 'json',
            type: 'GET',
            async: false,
            success: function (result) {
                GetWeatherByLocation(result.data.city);
                $("#ipAdd").text("Your IP Address: " + result.data.ip_address);
            },
            error: function (xhr) {
                alert("An error occured: " + xhr.status + " " + xhr.statusText);
            }
        });
    }

    $('#submitLocation').click(function (event) {

        var location = $('#inputLocation').val();

        if (location != '') {
            event.preventDefault();
            //reloadJs();
            GetWeatherByLocation(location);
        }
    });

    function GetWeatherByLocation(location) {
        var link = "/Weather/GetWeatherByLocation/";
        $.ajax({
            url: link,
            async: false,
            dataType: 'json',
            type: 'POST',
            data: {
                location: location
            },
            success: function (data) {
                if (data.count > 0) {
                    var locationId = data.list[0].id;
                    var locId = locationId.toString();

                    //if (prevLocationId != locId) {
                    //    prevLocationId = locId;
                    //    console.log("prevLocationId: ", prevLocationId);
                    //deleteScript();
                    //weatherwidget(locId);
                    //}

                    weatherWidget(data);
                } else {
                    console.log("Location Not Found.");
                }

            },
            error: function (xhr) {
                console.log('error: ', xhr);
            }
        });
        //testid = "1717512"
        //displayWidget(testid);
    }

    function weatherWidget(data) {
        urlIcon = "https://openweathermap.org/img/w/";

        var location = data.list[0].name;
        var weatherIcon = urlIcon + data.list[0].weather[0].icon + ".png";
        var temp = Math.floor(data.list[0].main.temp); // + "&#176;C"
        var weather = data.list[0].weather[0].main;

        $('.weather-card-loc').text(location);
        $('.weather-card-icon').attr('src', weatherIcon);
        $('.weather-card-text').text(weather);
        $('.weather-card-temp').text(temp);
    }

    //window.myWidgetParam ? window.myWidgetParam : window.myWidgetParam = [];
    //window.myWidgetParam.push({ id: 12, cityid: '2643743', appid: 'e82076e88954c924a68f7726a88b0c5d', units: 'metric', containerid: 'openweathermap-widget-12', });

    //(function () {
    //    var script = document.createElement('script');
    //    script.async = true; script.charset = "utf-8";
    //    script.src = "//openweathermap.org/themes/openweathermap/assets/vendor/owm/js/weather-widget-generator.js";
    //    var s = document.getElementsByTagName('script')[0];
    //    s.parentNode.insertBefore(script, s);
    //})();

    function weatherbyloc(userLocationId) {
        console.log("weatherwidget: ", userLocationId);
        //var LocationId = "1717512";
        //userLocationId = "7521309";

        window.myWidgetParam = [];
        console.log("myWidgetParam[]: ", window.myWidgetParam);
        //window.myWidgetParam ? window.myWidgetParam : window.myWidgetParam = [];
        //if (prevLocationId != userLocationId) {
        prevLocationId = userLocationId;

        console.log("prevLocationId123: ", prevLocationId);
        window.myWidgetParam.push({ id: 12, cityid: userLocationId, appid: '230e96dd67efd1a2dbf24ba673a1fe2e', units: 'metric', containerid: 'openweathermap-widget-12', });
        console.log("myWidgetParam: ", window.myWidgetParam);


        //displayWidget();
        reloadJs();
        //$("#openweathermap-widget-12").load(function () {
        //    //displayWidget();
        //    alert("weather loaded.");
        //});


        //$('#openweathermap-widget-12').load(window.location.href + " #openweathermap-widget-12");
        //}
    }

    function displayWidget() {

        //weatherwidget(x);

        var script = document.createElement('script');
        //script.async = true;
        script.charset = "utf-8";
        script.src = "//openweathermap.org/themes/openweathermap/assets/vendor/owm/js/weather-widget-generator.js";

        var s = document.getElementsByTagName('script')[0];
        console.log("s-main: ", s);
        s.parentNode.insertBefore(script, s);
    }

    //(function () {
    //    var script = document.createElement('script');
    //    script.async = true;
    //    script.charset = "utf-8";
    //    script.src = "//openweathermap.org/themes/openweathermap/assets/vendor/owm/js/weather-widget-generator.js";

    //    var s = document.getElementsByTagName('script')[0];
    //    s.parentNode.insertBefore(script, s);
    //})();

    function reloadJs() {
        var id = "displayWidget";
        var src = "//openweathermap.org/themes/openweathermap/assets/vendor/owm/js/weather-widget-generator.js";
        //var charset = "utf-8";
        $('script[src="' + src + '"]').remove();
        $('<script>').attr('src', src).appendTo('head');
    }

    function deleteScript() {
        var src = "//openweathermap.org/themes/openweathermap/assets/vendor/owm/js/weather-widget-generator.js";
        //var charset = "utf-8";
        $('script[src="' + src + '"]').remove();
    }
});

//function displayWidget() {
//        var script = document.createElement('script');
//        script.async = true;
//        script.charset = "utf-8";
//        script.src = "//openweathermap.org/themes/openweathermap/assets/vendor/owm/js/weather-widget-generator.js";

//        var s = document.getElementsByTagName('script')[0];
//        s.parentNode.insertBefore(script, s);
//}
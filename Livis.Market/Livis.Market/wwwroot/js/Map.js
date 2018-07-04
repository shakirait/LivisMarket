var GoogleMap = function ($) {
    var self = {
        form: {
            search: '#searchForm',
            searchNearBy: '#searchNearByForm',
            advancedSearch: '#advancedSearch'
        },
        markerObjects: [],
        instance: {},
        mapSettings: {
            Latitude: 35.540832,
            Longitude: 139.339478,
            Zoom: 10,
            MapType: 'ROADMAP',
        },
        localizedLabels: {
            ResultTitle: '検索結果',
            ItemText: '件',
            LocationText: '所在地',
            OpeningText: '開院時間',
            TelephoneText: 'TEL',
        },
        apiKey: 'AIzaSyC7oIWKixRGZKuQFMRoEzQ3rlvoUkJOAtM',
        init: function (apiKey, localizedLabels) {
            if (apiKey != null && apiKey !== '') {
                self.apiKey = apiKey;
            }
 
            self.localizedLabels = $.extend(true, {}, self.localizedLabels, localizedLabels);

            $(document).on('submit', self.form.search, search);
            $(document).on('submit', self.form.searchNearBy, searchNearBy);
            $(document).on('submit', self.form.advancedSearch, advancedSearch);
        },
        initializeMap: function (options) {
            self.mapSettings = $.extend(true, {}, self.mapSettings, options);

            var mapOptions = {
                center: new google.maps.LatLng(self.mapSettings.Latitude, self.mapSettings.Longitude),
                zoom: self.mapSettings.Zoom,
                mapTypeId: getMapType(self.mapSettings.MapType),
                disableDefaultUI: true
            };

            self.instance = new google.maps.Map(document.getElementById("map"), mapOptions);
        }
    }

    function renderMap(markers, moveToFirstMarker = false) {
        if ($('#clinicList').length > 0) {
            $('#clinicList').remove();
        }
        clearMarkersOnMap();
        // drop markers one by one
        $('.map-wrapper').append('<div class="clinic-list-wrapper collapse show" id="clinicList" aria-expanded="true"></div>')
        $('#clinicList').append('<div class="clinic-list-wrap"></div>');
        $('.clinic-list-wrap').append('<div class="clinic-header"><h2><img class="mr-3" src="/Static/img/clinic-icon.svg" alt=""><small>' + self.localizedLabels.ResultTitle + '　' + markers.length + self.localizedLabels.ItemText + '</small></h2></div>');
        $('.clinic-list-wrap').append('<div class="clinic-list"></div>');
        $('.clinic-list-wrap').append('<div class="clinic-footer"><a href="" style="width:90%" class="btn btn-primary btn-block">検索しなおす »</a></div>');

        if (markers.length === 0) return;


        if (moveToFirstMarker) {
            setCenter(markers[0].lat, markers[0].lng);
        }

        var i = 0;
        var interval = setInterval(function () {
            var data = markers[i];
            var myLatlng = new google.maps.LatLng(data.lat, data.lng);

            // initial icon
            var pinImage = new google.maps.MarkerImage("/Static/img/pin_01.svg");

            // marker object for the marker
            var marker = new google.maps.Marker({
                position: myLatlng,
                map: self.instance,
                title: data.title,
                animation: google.maps.Animation.DROP,
                icon: pinImage
            });

            // store in a global array
            var markerIndex = self.markerObjects.push(marker) - 1;

            // click listener on a marker itself
            google.maps.event.addListener(self.markerObjects[markerIndex], 'click', function () {
                var marker = this;
                if (marker.getAnimation() != null) {
                    marker.setAnimation(null);
                } else {
                    marker.setAnimation(google.maps.Animation.BOUNCE);
                }
            });


            // create a row in the overlay table and bind onhover
            var $row = $('<div>')
                .addClass('clinic-item')

                .append('<div class="d-flex justify-content-between"><div class="f-auto"><h4><a data-toggle="collapse" href="#details" aria-expanded="false" aria-controls="details">' + data.title + '</a>' +
                '</h4 > <dl class="dl-small"><dt>所在地</dt><dd>' + data.loc + '</dd><dt>開院時間</dt><dd>' + data.open + '</dd><dt>TEL</dt><dd>' + data.tel + '</dd></dl></div>' +
                '<div class="clinic-image"><img src="/Static/img/image-placeholder_03.jpg" alt=""></div></div>' +
                '<ul class="nav d-flex justify-content-between">' +
                '<li><a href="">さらに詳しく »</a></li></ul>')

                .on('mouseenter', function () {
                    var marker = self.markerObjects[markerIndex];
                    marker.setAnimation(google.maps.Animation.BOUNCE);
                })
                .on('mouseleave', function () {
                    var marker = self.markerObjects[markerIndex];
                    if (marker.getAnimation() != null) {
                        marker.setAnimation(null);
                    }
                });

            $row.appendTo('.clinic-list');

            // continue iteration
            i++;
            if (i === markers.length) {
                clearInterval(interval);
            }
        }, 200);

    }

    function clearMarkersOnMap() {
        for (var i = 0; i < self.markerObjects.length; i++) {
            var marker = self.markerObjects[i];
            marker.setMap(null);
        }

        self.markerObjects = [];
    }

    function getMapType(type) {
        return google.maps.MapTypeId.ROADMAP;
    }

    function setCenter(lat, lng) {
        GoogleMap.instance.setCenter({ lat: lat, lng: lng });
    }

    function getCurrentPositionOnSuccess(position) {
        var latitude = position.coords.latitude;
        var longitude = position.coords.longitude;

        var $form = $(GoogleMap.form.searchNearBy);

        $.ajax({
            type: "POST",
            url: $form.attr("action"),
            data: { latitude, longitude }
        })
            .done(function (items) {
                renderMap(items);
                setCenter(latitude, longitude);

            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                console.log(errorThrown);
            });
    }

    function getCurrentPositionOnFail() {
        alert("Unable to retrieve your location");
    }

    function getCurrentPositionFromApi(onSuccess, onFail) {
        $.ajax({
            type: "POST",
            url: 'https://www.googleapis.com/geolocation/v1/geolocate?key=' + self.apiKey
        })
            .done(function (data) {
                onSuccess({ coords: { latitude: data.location.lat, longitude: data.location.lng } });
            })
            .fail(onFail);

    }

    function search (e) {
        e.preventDefault();
        var $form = $(this);

        $.ajax({
                type: "POST",
                url: $form.attr("action"),
                data: $form.serialize(),
            })
            .done(function (items) {
                renderMap(items, true);
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                console.log(errorThrown);
            });

        return false;
    }

    function searchNearBy (e) {
        e.preventDefault();
        var $form = $(this);

        if (location.protocol === 'https') {
            if (!navigator.geolocation)
                alert("Geolocation is not supported by your browser");

            navigator.geolocation.getCurrentPosition(
                getCurrentPositionOnSuccess,
                getCurrentPositionOnFail);
        } else {
            getCurrentPositionFromApi(
                getCurrentPositionOnSuccess,
                getCurrentPositionOnFail);
        }

        return false;
    }

    function advancedSearch (e) {
        e.preventDefault();
        var $form = $(this);

        var $closeButton = $('[data-toggle]', $form);

        $.ajax({
                type: "POST",
                url: $form.attr("action"),
                data: $form.serialize(),
            })
            .done(function (items) {
                renderMap(items, true);
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                console.log(errorThrown);
            })
            .always(function() {
                $closeButton.click();
            });

        return false;
    }

    return self;
}(jQuery);
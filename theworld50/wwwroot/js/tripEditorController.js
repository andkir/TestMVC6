(function() {
    "use strict";

    angular.module("app-trips")
        .controller("tripsEditorController", tripsEditorController);

    function tripsEditorController($routeParams, $http) {
        var vm = this;

        vm.tripName = $routeParams.tripName;
        vm.stops = [];
        vm.errorMessage = "";
        vm.isBusy = true;
        var url = "/api/trips/" + vm.tripName + "/stops";

        $http.get(url)
            .then(function(response) {
                angular.copy(response.data, vm.stops);
                _showMap(vm.stops);
                },
            function() {
                vm.errorMessage = "Fail to load stops for trip.";
            })
            .finally(function() {
                vm.isBusy = false;
            });

        vm.addStop = function() {
            $http.post(url, vm.newStop)
            .then(function (response) {
                 vm.stops.push(response.data);
                 _showMap(vm.stops);
                 vm.newStop.name = {};
            },
            function () {
                vm.errorMessage = "Fail to add new stops for trip.";
            })
            .finally(function () {
                vm.isBusy = false; 
            });
        }
    }



    function _showMap(stops) {
        if (stops && stops.length > 0) {

            var mapStops = _.map(stops, function(item) {
                return {
                    lat: item.latitude,
                    long: item.longitude,
                    info: item.name
                }
            });

            travelMap.createMap({
                stops: mapStops,
                selector: "#map",
                currentStop: 1,
                initialZoom: 3
            });

        }
    }
})();
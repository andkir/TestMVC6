(function () {

    "use strict";

    var artistControllers = angular.module("artistControllers", []);

    artistControllers.controller("ListController", ListController);
    artistControllers.controller("DetailsController", DetailsController);

    function ListController($scope, $http) {
        var vm = this;
        $http.get("Data").success(function (data) {
            vm.artists = data;
            vm.artistOrder = 'name';
        }).error(function (er) {
            throw er;
        });
    }

    function DetailsController($scope, $http, $routeParams) {
        var vm = this;
        var currItemId = Number($routeParams.itemId);
        $http.get("Data").success(function (data) {
            vm.artist = data[currItemId];

            if (currItemId > 0) {
                vm.prevItem = currItemId - 1;
            } else {
                vm.prevItem = data.length - 1;
            }
            if (currItemId < data.length - 1) {
                vm.nextItem = currItemId + 1;
            } else {
                vm.nextItem = 0;
            }
        }).error(function (er) {
            throw er;
        });
    }
}
)();
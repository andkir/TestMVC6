var MyApp = angular.module('Myapp', ['ngRoute', 'artistControllers']);

MyApp.config([
    '$routeProvider', function($routeProvider) {
        $routeProvider
            .when('/list',
            {
                templateUrl: '/views/artistsList.html',
                controller: 'ListController',
                controllerAs: "vm"
            })
            .when('/details/:itemId',
            {
                templateUrl: '/views/details.html',
                controller: 'DetailsController',
                controllerAs: "vm"
            })
            .otherwise({
                redirectTo: '/list'
            });
    }
]);
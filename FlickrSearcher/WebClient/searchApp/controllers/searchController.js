(function(app) {

    var searchController = function ($scope) {

        $scope.number = 4 + 5;

    }

    app.controller("searchController", ["$scope", searchController]);

}(angular.module("searchApp")));
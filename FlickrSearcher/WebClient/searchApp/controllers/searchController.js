(function(app) {

    var searchController = function ($scope, photoService) {

        var onError = function () { }

        var onPhotosFound = function (response) {
            $scope.photos = response.data;
        }

        $scope.searchText = "";
        var init = function() {
            var randomPage = Math.floor(Math.random() * (42 - 1)) + 1;

            photoService
                .searchPhotos("search", randomPage)
                .then(onPhotosFound, onError);
        }

        $scope.makeSearch = function() {
            photoService
                .searchPhotos($scope.searchText, 1)
                .then(onPhotosFound, onError);
        }

        init();
    }

    app.controller("searchController", ["$scope", "photoService", searchController]);

}(angular.module("searchApp")));
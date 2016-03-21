(function(app) {

    var searchController = function ($scope, photoService) {
        $scope.searchText = "";
        $scope.isLoading = true;
        $scope.detailsShown = false;
        $scope.selectedPhoto = null;

        var onError = function () { $scope.isLoading = false; }

        var onPhotosFound = function (response) {
            $scope.photos = response.data;
            $scope.isLoading = false;
        }
        var onDetailsLoaded = function (response) {
            $scope.selectedPhoto.details = response.data;
            $scope.isLoading = false;

            updateModalScroll();
        }

        var init = function() {
            var randomPage = Math.floor(Math.random() * (42 - 1)) + 1;

            photoService
                .searchPhotos("London", randomPage)
                .then(onPhotosFound, onError);
        }

        $scope.makeSearch = function () {
            $scope.isLoading = true;
            photoService
                .searchPhotos($scope.searchText, 1)
                .then(onPhotosFound, onError);
        }

        $scope.openDetails = function(photoId, imageUrl) {

            $("html").getNiceScroll().locked = true;
            $scope.isLoading = true;
            $scope.detailsShown = true;
            $scope.selectedPhoto = {};
            $scope.selectedPhoto.large_image_url = imageUrl;
            
            $('photo-details').removeClass("invisible");
            updateModalScroll();
            

            photoService
                .loadDetails(photoId)
                .then(onDetailsLoaded, onError);
        }


        $scope.closeDetails = function() {
            $scope.detailsShown = false;

            fadeOutPhotoDetails();

            window.setTimeout(function () {
                $scope.selectedPhoto.large_image_url = null;
                $scope.selectedPhoto = null;
                $scope.$apply();
            }, 1000);
        }

        init();
    }

    app.controller("searchController", ["$scope", "photoService", searchController]);

}(angular.module("searchApp")));
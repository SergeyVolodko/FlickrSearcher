(function(app) {

    var searchController = function ($scope, photoService) {
        $scope.searchText = "";
        $scope.isLoading = true;
        $scope.detailsShown = false;
        $scope.selectedPhoto = null;

        var onError = function () { $scope.isLoading = false; }

        var onPhotosFound = function (response) {
            if (response.data && response.data.length > 0) {
                $scope.photos = response.data;
            }
            else {
                alert('Sorry, no photos found for the request: ' + $scope.searchText);
            }
            
            $scope.isLoading = false;
        }
        var onDetailsLoaded = function (response) {
            $scope.selectedPhoto.details = response.data;
            $scope.isLoading = false;
        }

        //////
        //  Init
        //////
        var init = function() {
            var randomPage = Math.floor(Math.random() * (42 - 1)) + 1;

            photoService
                .searchPhotos("London", randomPage)
                .then(onPhotosFound, onError);
        }

        //////
        //  makeSearch
        //////
        $scope.makeSearch = function () {
            if ($scope.searchText === "") {
                return;
            }
            $scope.isLoading = true;
            photoService
                .searchPhotos($scope.searchText, 1)
                .then(onPhotosFound, onError);
        }

        //////
        //  openDetails
        //////
        $scope.openDetails = function(photoId, imageUrl) {
            $scope.isLoading = true;
            startFadeInPhotoDetails();
            
            $scope.detailsShown = true;
            
            $scope.selectedPhoto = {};
            $scope.selectedPhoto.large_image_url = imageUrl;

            photoService
                .loadDetails(photoId)
                .then(onDetailsLoaded, onError);
        }

        //////
        //  closeDetails
        //////
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
(function(app) {

    var searchController = function ($scope, photoService) {
        $scope.searchText = "";
        $scope.currentPage = 1;
        $scope.isLoading = true;
        $scope.detailsShown = false;
        $scope.selectedPhoto = null;
        $scope.photos = [];

        var onError = function () { $scope.isLoading = false; }

        var onPhotosFound = function (response) {
            if (response.data && response.data.length > 0) {
                for (var i = 0; i < response.data.length; i++) {
                    $scope.photos.push(response.data[i]);
                }
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
                .searchPhotos("Netherlands", randomPage)
                .then(onPhotosFound, onError);
        }

        //////
        //  makeSearch
        //////
        $scope.makeSearch = function () {
            if ($scope.searchText === "") {
                return;
            }
            $scope.photos = [];
            $scope.currentPage = 1;
            $scope.isLoading = true;
            photoService
                .searchPhotos($scope.searchText, 1)
                .then(onPhotosFound, onError);
        }

        //////
        //  loadNextPage
        //////
        $scope.loadNextPage = function () {
            if ($scope.searchText === ""
                && $scope.currentPage === 1) {
                $scope.searchText = "Netherlands";
            }
            $scope.isLoading = true;
            $scope.currentPage += 1;
            photoService
                .searchPhotos($scope.searchText, $scope.currentPage)
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
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
            $("html").getNiceScroll().lock();
            

            photoService
                .loadDetails(photoId)
                .then(onDetailsLoaded, onError);
        }


        $scope.closeDetails = function() {
            $scope.detailsShown = false;
            removeModalScroll();
            

            $('photo-details').animate({ 'left': '-105%' }, { duration: 400, queue: false }).delay(300).fadeOut(400);
            $('photo-details').animate({ 'right': '105%' }, { duration: 400, queue: false });
            

            setTimeout(function () {
                $('photo-details').attr('style', function (i, style) {
                    return style = "";
                });
            }, 1000);
            $("html").getNiceScroll().locked = false;

            window.setTimeout(clearSelectedPhoto, 1000);
        }

        function clearSelectedPhoto() {
            $scope.selectedPhoto.large_image_url = null;
            $scope.selectedPhoto = null;
            $scope.$apply();
        }


        init();
    }

    app.controller("searchController", ["$scope", "photoService", searchController]);

}(angular.module("searchApp")));
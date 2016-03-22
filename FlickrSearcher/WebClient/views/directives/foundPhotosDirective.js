angular.module('searchApp')
    .directive('foundPhotos', function () {
        return {
            restrict: 'E',
            templateUrl: 'views/found-photos.html'
        }
    });
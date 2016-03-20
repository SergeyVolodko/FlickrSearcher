angular.module('searchApp')
    .directive('photoDetails', function () {
        return {
            restrict: 'E',
            templateUrl: 'views/photo-details.html'
        }
    });
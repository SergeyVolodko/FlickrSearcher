angular.module('searchApp')
    .directive('searchBar', function () {
        return {
            restrict: 'E',
            templateUrl: 'views/search-bar.html'
        }
    });
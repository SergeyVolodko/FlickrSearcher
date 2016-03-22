angular.module('searchApp')
    .directive('spinner', function () {
        return {
            restrict: 'E',
            templateUrl: 'views/spinner.html'
        }
    });
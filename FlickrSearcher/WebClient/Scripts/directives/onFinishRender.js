angular.module('searchApp')
    .directive('onFinishRender', ['$timeout', '$parse', function ($timeout, $parse) {
        return {
            restrict: 'A',
            link: function (scope, element, attr) {
                console.log(window);
                $timeout(function () {
                    window.assignOpenModalLogic();
                });
            }
        }
    }])
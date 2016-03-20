angular.module('searchApp')
    .directive('onFinishRender', ['$timeout', '$parse', function ($timeout, $parse) {
        return {
            restrict: 'A',
            link: function () {
                $timeout(function () {
                    
                    //window.assignOpenModalLogic();
                });
            }
        }
    }])
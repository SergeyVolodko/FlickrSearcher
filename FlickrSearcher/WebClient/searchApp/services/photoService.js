(function(app) {

    var baseUrl = "http://localhost:62276/api/";

    var photoService = function($http) {

        var photoFactory = {};

        photoFactory.searchPhotos = function(text, page) {
            return $http({
                url: baseUrl + "search",
                method: "GET",
                params: { text: text, page: page }
            });
        }

        photoFactory.loadDetails = function (photoId) {
            return $http({
                url: baseUrl + "photoDetails/" + photoId,
                method: "GET"
            });
        }


        return photoFactory;
    }

    app.service("photoService", ["$http", photoService]);

}(angular.module("searchApp")));
angular.module('Directives').directive('photos', ['viewData', 'photoService', function (viewData, photoService) {

    var _link = function ($scope, element, attrs) {

        $scope.$watch(function () { return viewData.postId; }, function () {
            if (viewData.postId) {
                $scope.photos = photoService.query({ postid: viewData.postId});
            }
        });
    };


    return {
        link: _link,
        restrict: 'A',
        templateUrl: 'Scripts/app/views/directives/photos.html',
        replace: false
    };
}]);
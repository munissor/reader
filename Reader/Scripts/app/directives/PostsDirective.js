angular.module('Directives').directive('posts', ['viewData', 'postService', function (viewData, postService) {

    var _link = function ($scope, element, attrs) {

        $scope.$watch(function() { return viewData.feedId; }, function () {
            if(viewData.feedId){
                $scope.posts = postService.query({ feedId: viewData.feedId, page: 0, pageSize: 50 });
            }
        });

        $scope.formatTags = function (post) {
            var t = post.Tags || [];
            return t.join(', ');
        };

        $scope.selectPost = function (post) {
            viewData.postId = post.Id;
        };
    };


    return {
        link: _link,
        restrict: 'A',
        templateUrl: 'Scripts/app/views/directives/posts.html',
        replace: false
    };
}]);
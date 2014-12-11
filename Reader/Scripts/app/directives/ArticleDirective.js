angular.module('Directives').directive('article', ['$rootScope', '$sce', 'viewData', 'enums', 'events', 'articleService', function ($rootScope, $sce, viewData, enums, events, articleService) {

    var _link = function ($scope, element, attrs) {

        $scope.$watch(function () { return viewData.articleId; }, function () {
            if (viewData.articleId) {
                $scope.article = null;

                articleService.get({ id: viewData.articleId },
                    function (article, headers) {
                        $scope.article = article;
                    },
                    function (httpError) {
                        $rootScope.$broadcast(events.notification.show, { type: enums.notificationTypes.error, title: "Load article", text: "An error occurred" });
                    }
               );
            }
        });
    };


    return {
        link: _link,
        scope: {},
        restrict: 'A',
        templateUrl: 'Scripts/app/views/directives/article.html',
        replace: false
    };
}]);
angular.module('Directives').directive('article', ['$sce', 'viewData', 'articleService', function ($sce, viewData, articleService) {

    var _link = function ($scope, element, attrs) {

        $scope.$watch(function () { return viewData.articleId; }, function () {
            if (viewData.articleId) {
                $scope.article = null;

                articleService.get({ id: viewData.articleId }, function (article, headers) {
                    $scope.article = article;
                    //$scope.article.Content = $sce.trustAsHtml($scope.article.Content);
                });
            }
        });
    };


    return {
        link: _link,
        restrict: 'A',
        templateUrl: 'Scripts/app/views/directives/article.html',
        replace: false
    };
}]);
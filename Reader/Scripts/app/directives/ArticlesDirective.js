angular.module('Directives').directive('articles', ['viewData', 'articleService', function (viewData, articleService) {

    var _link = function ($scope, element, attrs) {

        var isLoading = false;

        $scope.articles = [];

        $scope.$watch(function () { return viewData.subscriptionId; }, function () {
            $scope.articles = [];
            $scope.loadNextPage();
        });

        $scope.$watch(function () { return viewData.filter; }, function () {
            $scope.articles = [];
            $scope.loadNextPage();
        });

        $scope.loadNextPage = function () {
            if (!isLoading) {
                if (viewData.subscriptionId !== null) {
                    var aid = '';
                    if ($scope.articles.length > 0) {
                        aid = $scope.articles[$scope.articles.length - 1].Id;
                    }
                    isLoading = true;

                    articleService.query({ subscriptionId: viewData.subscriptionId, articleId: aid, count: 50, filter: viewData.filter }, function (data, responseHeader) {
                        Array.prototype.push.apply($scope.articles, data);
                        isLoading = false;
                    });
                }
            }
        };

        $scope.formatCategories = function (article) {
            var t = $.map(article.Categories || [], function (c, i) { return c.Name; });
            return t.join(', ');
        };

        $scope.formatAuthors = function (article) {
            var t = $.map(article.Authors || [], function (a, i) { return a.Name; });
            return t.join(', ');
        };

        $scope.selectArticle = function (article) {

            // TODO: delay marking as read with a timeout
            //article.Read = true;
            //article.update({ id: post.Id }, post);

            viewData.articleId = article.Id;
        };

        $scope.toggleRead = function (article) {
            article.Read = !article.Read;
            articleService.save({ id: article.Id }, article);
        };

        $scope.toggleStarred = function (article) {
            article.Starred = !article.Starred;
            articleService.save({ id: article.Id }, article);
        };
    };


    return {
        link: _link,
        restrict: 'A',
        templateUrl: 'Scripts/app/views/directives/articles.html',
        replace: false
    };
}]);
angular.module('Directives').directive('subscriptions', ['viewData', 'subscriptionService', function (viewData, subscriptionService) {

    var _link = function ($scope, element, attrs) {

        $scope.adding = false;
        
        $scope.addModel = null;

        $scope.subscriptions = [];

        function reloadFeeds() {

            $scope.subscriptions = [];

            subscriptionService.query(function (data, responseHeader) {
                $scope.subscriptions.push({ Title: 'All', Id: '' });

                Array.prototype.push.apply($scope.subscriptions, data);
            });
        };
        
        $scope.selectSubscription = function (subscription) {
            viewData.subscriptionId = subscription.Id;
        }

        $scope.openAdd = function () {
            $scope.adding = true;
            $scope.addModel = {
                Url: ""
            };
        }

        $scope.closeAdd = function () {
            $scope.adding = false;
            $scope.addModel = null;
        }

        $scope.add = function () {
            subscriptionService.save($scope.addModel, function (u, putResponseHeaders) {
                $scope.closeAdd();
                reloadFeeds();
            });
        }

        reloadFeeds();
    };
  

    return {
        link: _link,
        restrict: 'A',
        templateUrl: 'Scripts/app/views/directives/subscriptions.html',
        replace: true
    };
}]);
angular.module('Directives').directive('subscriptions', ['viewData', 'enums', 'subscriptionService', function (viewData, enums, subscriptionService) {

    var _link = function ($scope, element, attrs) {

        $scope.adding = false;
        $scope.maintenance = false;
        $scope.addModel = null;

        $scope.subscriptions = [];

        $scope.filters = enums.filters;
        $scope.filter = enums.filters[0];

        function reloadFeeds() {

            $scope.subscriptions = [];

            subscriptionService.query(function (data, responseHeader) {
                $scope.subscriptions.push({ Title: 'All', Id: '' });

                Array.prototype.push.apply($scope.subscriptions, data);
            });
        }

        $scope.$watch(function () { return $scope.filter; }, function () {
            viewData.filter = $scope.filter;
        });

      
        
        $scope.selectSubscription = function (subscription) {
            viewData.subscriptionId = subscription.Id;
        };

        $scope.openAdd = function () {
            $scope.adding = true;
            $scope.addModel = {
                Url: ""
            };
        };

        $scope.closeAdd = function () {
            $scope.adding = false;
            $scope.addModel = null;
        };

        $scope.add = function () {
            subscriptionService.save($scope.addModel, function (u, putResponseHeaders) {
                $scope.closeAdd();
                reloadFeeds();
            });
        };

        $scope.toggleMaintenance = function () {
            $scope.maintenance = !$scope.maintenance;
        };

        $scope.removeSubscription = function (subscription) {
            subscriptionService.remove({ id: subscription.Id }, function () {
                // TODO: remove the single feed
                reloadFeeds();
                //$scope.subscriptions.remove(subscription);
            });
        };

        reloadFeeds();
    };
  

    return {
        link: _link,
        restrict: 'A',
        templateUrl: 'Scripts/app/views/directives/subscriptions.html',
        replace: true
    };
}]);
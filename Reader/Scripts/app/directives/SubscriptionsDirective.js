angular.module('Directives').directive('subscriptions', ['$rootScope', 'viewData', 'enums', 'events', 'subscriptionService', function ($rootScope, viewData, enums, events, subscriptionService) {

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
            subscriptionService.save($scope.addModel,
                function (u, putResponseHeaders) {
                    $scope.closeAdd();
                    reloadFeeds();
                    $rootScope.$broadcast(events.notification.show, { title: "Subscribe", text: "Subscribed to the feed!" });
                },
                function (httpError) {
                    if (httpError.status == 304) {
                        $rootScope.$broadcast(events.notification.show, { title: "Subscribe", text: "You are already subscribed to the feed!" });
                    }
                    else {
                        $rootScope.$broadcast(events.notification.show, { type: enums.notificationTypes.error, title: "Subscribe", text: "An error occurred" });
                    }
                });
        };

        $scope.toggleMaintenance = function () {
            $scope.maintenance = !$scope.maintenance;
        };

        $scope.removeSubscription = function (subscription) {
            subscriptionService.remove({ id: subscription.Id },
                function () {
                    reloadFeeds();
                    $rootScope.$broadcast(events.notification.show, { title: "Unsubscribe", text: "Unsubscribed from the feed!" });
                },
                function (httpError) {
                    $rootScope.$broadcast(events.notification.show, { type: enums.notificationTypes.error, title: "Unsubscribe", text: "An error occurred" });
                });
        };

        $scope.isSelected = function(subscription) {
            return subscription.Id == viewData.subscriptionId;
        };
        
        reloadFeeds();
    };
  

    return {
        link: _link,
        scope: {},
        restrict: 'A',
        templateUrl: 'Scripts/app/views/directives/subscriptions.html',
        replace: true
    };
}]);
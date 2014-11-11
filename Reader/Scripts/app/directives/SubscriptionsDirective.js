angular.module('Directives').directive('subscriptions', ['viewData', 'subscriptionService', function (viewData, subscriptionService) {

    var _link = function ($scope, element, attrs) {
        $scope.subscriptions = subscriptionService.query();

        $scope.selectSubscription = function (subscription) {
            viewData.subscriptionId = subscription.Id;
        }
    };
  

    return {
        link: _link,
        restrict: 'A',
        templateUrl: 'Scripts/app/views/directives/subscriptions.html',
        replace: true
    };
}]);
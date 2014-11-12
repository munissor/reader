angular.module('Directives').directive('subscriptions', ['viewData', 'subscriptionService', function (viewData, subscriptionService) {

    var _link = function ($scope, element, attrs) {

        $scope.adding = false;
        
        $scope.addModel = null;

        $scope.subscriptions = subscriptionService.query();

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
            subscriptionService.save($scope.addModel);
        }
    };
  

    return {
        link: _link,
        restrict: 'A',
        templateUrl: 'Scripts/app/views/directives/subscriptions.html',
        replace: true
    };
}]);
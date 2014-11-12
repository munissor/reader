angular.module('Services').service('subscriptionService', ['$resource', function ($resource) {
    return $resource('/api/subscription/:id', { id: '@id' });
}]);
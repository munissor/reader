angular.module('Services').service('articleService', ['$resource', function ($resource) {
    return $resource('/api/article/:id', { id: '@id' });
}]);
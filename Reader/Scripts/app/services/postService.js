angular.module('Services').service('postService', ['$resource', function ($resource) {
    return $resource('/api/posts/:id', { id: '@id' });
}]);
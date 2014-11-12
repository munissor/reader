angular.module('Services').service('photoService', ['$resource', function ($resource) {
    return $resource('/api/photos/:id', { id: '@id' });
}]);
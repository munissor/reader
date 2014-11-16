var app = angular.module('reader', ['ngRoute', 'ngResource', 'ngSanitize', 'Controllers', 'Services', 'Directives', 'Utils']);
angular.module('Utils', []);
angular.module('Services', ['Utils']);
angular.module('Controllers', ['Services', 'Utils']);
angular.module('Directives', ['Services', 'Utils']);


app.config(['$routeProvider', function ($routeProvider) {
    $routeProvider
        .when('/', {
            templateUrl: 'Scripts/app/views/home.html',
            controller: 'HomeController'
        })
      .otherwise({
          redirectTo: '/'
      });
}]);

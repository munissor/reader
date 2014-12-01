angular.module('Directives').directive('notification', ['$rootScope', 'events', 'enums', 'toastr', function ($rootScope, events, enums, toastr) {

	var _template = '<div></div>';

	var _link = function($scope, element, attrs) {


		toastr.options = {
			"closeButton": true
		};



		$rootScope.$on(events.notification.show, function(event, args) {
			if (!args.type) {
				args.type = enums.notificationTypes.info;
			}
			toastr[args.type](args.text, args.title);
		});
	};

	return {
		link: _link,
		restrict: 'A',
		template: _template,
		replace: false
	};
}]);
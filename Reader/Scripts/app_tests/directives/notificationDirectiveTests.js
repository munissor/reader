describe('directive: pagination', function() {
    var _rootScope, _scope, _events, _enums, _toastr, _element;

    beforeEach(module('Directives'));

    beforeEach(inject(function ($rootScope, $compile, $templateCache, events, enums, toastr) {
        _rootScope = $rootScope;
        _scope = $rootScope.$new();
        _events = events;
        _enums = enums;
        _toastr = toastr;
        
        _element = '<notification></notification>';
        _element = $compile(_element)(_scope);
        _scope.$digest();

        spyOn(_toastr, 'info').and.callThrough()();
        spyOn(_toastr, 'success').and.callThrough();
        spyOn(_toastr, 'warning').and.callThrough();
        spyOn(_toastr, 'error').and.callThrough();
    }));

    it('should show the notification info when notification.show is fired without type', function () {
        _rootScope.$broadcast(_events.notification.show, { type: null, title: 'Hello', text: 'Hello world' });
        expect(_toastr.info).toHaveBeenCalled();
    });
    it('should show the notification info when notification.show is fired ', function () {
        _rootScope.$broadcast(_events.notification.show, { type: _enums.notificationTypes.info, title: 'Hello', text: 'Hello world' });
        expect(_toastr.info).toHaveBeenCalled();
    });
    it('should show the notification success when notification.show is fired ', function () {
        _rootScope.$broadcast(_events.notification.show, { type: _enums.notificationTypes.success, title: 'Hello', text: 'Hello world' });
        expect(_toastr.success).toHaveBeenCalled();
    });
    it('should show the notification warning when notification.show is fired ', function () {
        _rootScope.$broadcast(_events.notification.show, { type: _enums.notificationTypes.warning, title: 'Hello', text: 'Hello world' });
        expect(_toastr.warning).toHaveBeenCalled();
    });
    it('should show the notification error when notification.show is fired ', function () {
        _rootScope.$broadcast(_events.notification.show, { type: _enums.notificationTypes.error, title: 'Hello', text: 'Hello world' });
        expect(_toastr.error).toHaveBeenCalled();
    });

    
});
describe('directive: articles', function () {
    var _rootScope, _scope, _events, _enums, _element, _mockArticleService, _mockViewData;

    beforeEach(module('Directives'));

    beforeEach(function () {

        var mockArticleService = {
            query: function (args, success) {
                var aid = args.articleId || 1;
                var count = args.count;

                var articles = [];
                for (var i = 0; i < count; i++) {
                    articles.push({ Id: aid + i, Read: false, Starred: false /* .. */ });
                }
                
                success(articles);
            },
            get: function () { },
            save: function () { },
            remove: function () { }
        },

        mockViewData = {
            subscriptionId: null,
            articleId: null
        };

        module(function ($provide) {
            $provide.value('articleService', mockArticleService);
            $provide.value('viewData', mockViewData);
        });

    });

    beforeEach(inject(function ($rootScope, $compile, $templateCache, events, enums, articleService, viewData) {
        $templateCache.put("Scripts/app/views/directives/articles.html", '<div></div>');
        _rootScope = $rootScope;
        _scope = $rootScope.$new();
        _events = events;
        _enums = enums;
        _mockArticleService = articleService;
        _mockViewData = viewData;
        
        _element = '<div data-articles></div>';
        _element = $compile(_element)(_scope);
        _scope.$digest();
    }));

    it('should change the viewData.articleId when selecting a new article', function () {
        _scope.selectArticle({ Id: 123 });
        expect(_mockViewData.articleId).toBe(123);
    });

    it('should format categories correctly', function () {
        var categories = _scope.formatCategories({ Categories: [{ Name: 'a' }, { Name: 'b' }] });
        expect(categories).toBe('a, b');
    });

    it('should format null categories correctly', function () {
        var categories = _scope.formatCategories({ Categories: null });
        expect(categories).toBe('');
    });

    it('should format authors correctly', function () {
        var categories = _scope.formatAuthors({ Authors: [{ Name: 'a' }, { Name: 'b' }] });
        expect(categories).toBe('a, b');
    });

    it('should format null authors correctly', function () {
        var categories = _scope.formatAuthors({ Authors: null });
        expect(categories).toBe('');
    });

    it('should save an unread article as read if I call toggleRead', function () {
        var saveSpy = spyOn(_mockArticleService, 'save').and.callThrough();
        var article = { Id: 123, Read: false };
        _scope.toggleRead(article);

        expect(saveSpy).toHaveBeenCalledWith({ id: 123 }, { Id: 123, Read: true }, jasmine.any(Function), jasmine.any(Function));
        
    });

    it('should save an read article as unread if I call toggleRead', function () {
        var saveSpy = spyOn(_mockArticleService, 'save').and.callThrough();
        var article = { Id: 123, Read: true };
        _scope.toggleRead(article);

        expect(saveSpy).toHaveBeenCalledWith({ id: 123 }, { Id: 123, Read: false }, jasmine.any(Function), jasmine.any(Function));

    });

    it('should save an unstarred article as starred if I call toggleStarred', function () {
        var saveSpy = spyOn(_mockArticleService, 'save').and.callThrough();
        var article = { Id: 123, Starred: false };
        _scope.toggleStarred(article);

        expect(saveSpy).toHaveBeenCalledWith({ id: 123 }, { Id: 123, Starred: true }, jasmine.any(Function), jasmine.any(Function));

    });

    it('should save an starred article as unstarred if I call toggleStarred', function () {
        var saveSpy = spyOn(_mockArticleService, 'save').and.callThrough();
        var article = { Id: 123, Starred: true };
        _scope.toggleStarred(article);

        expect(saveSpy).toHaveBeenCalledWith({ id: 123 }, { Id: 123, Starred: false }, jasmine.any(Function), jasmine.any(Function));

    });

    it('should not load articles if the subscription id is missing', function () {
        var querySpy = spyOn(_mockArticleService, 'query').and.callThrough();
        _mockViewData.subscriptionId = null;
        _scope.loadNextPage();
        expect(querySpy).not.toHaveBeenCalled();

    });


    it('should load articles if the subscription id is available', function () {
        var querySpy = spyOn(_mockArticleService, 'query').and.callThrough();
        _mockViewData.subscriptionId = 123;
        _scope.loadNextPage();
        expect(querySpy).toHaveBeenCalled();

    });

});
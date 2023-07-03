using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using GenHTTP.Api.Content;
using GenHTTP.Api.Content.Templating;
using GenHTTP.Api.Content.Websites;
using GenHTTP.Api.Protocol;

using GenHTTP.Modules.IO;
using GenHTTP.Modules.Layouting;
using GenHTTP.Modules.Scriban;

namespace GenHTTP.Themes.AdminLTE
{

    public class AdminLteTheme : ITheme
    {
        private readonly string? _Title;

        private readonly bool _EnableFullscreen;

        private readonly Func<IRequest, IHandler, ValueTask<UserProfile?>>? _UserProfile;

        private readonly Func<IRequest, IHandler, ValueTask<string?>>? _FooterLeft, _FooterRight, _Sidebar, _Notifications;

        private readonly Func<IRequest, IHandler, ValueTask<SearchBox?>>? _SearchBox;

        private readonly Func<IRequest, IHandler, ValueTask<MenuSearchBox?>>? _MenuSearchBox;

        private readonly IMenuProvider? _HeaderMenu;

        #region Supporting data structures

        public class ThemeModel
        {

            public string? Title { get; }

            public bool HasLogo { get; }

            public bool EnableFullscreen { get; }

            public UserProfile? UserProfile { get; }

            public string? FooterLeft { get; }

            public string? FooterRight { get; }

            public string? Sidebar { get; }

            public SearchBox? SearchBox { get; }

            public MenuSearchBox? MenuSearchBox { get; }

            public List<ContentElement>? HeaderMenu { get; }

            public string? Notifications { get; }

            public ThemeModel(string? title, bool hasLogo, bool enableFullscreen, UserProfile? userProfile, 
                              string? footerLeft, string? footerRight, string? sidebar,
                              SearchBox? searchBox, MenuSearchBox? menuSearchBox,
                              List<ContentElement>? headerMenu, string? notifications)
            {
                Title = title;

                HasLogo = hasLogo;
                EnableFullscreen = enableFullscreen;
                UserProfile = userProfile;

                FooterLeft = footerLeft;
                FooterRight = footerRight;

                Sidebar = sidebar;
                SearchBox = searchBox;
                MenuSearchBox = menuSearchBox;

                HeaderMenu = headerMenu;
                Notifications = notifications;
            }

        }

        #endregion

        #region Get-/Setters

        public List<Script> Scripts
        {
            get
            {
                return new List<Script>
                {
                    GetScript("jquery.min.js"), GetScript("bootstrap.bundle.min.js"), GetScript("adminlte.min.js")
                };
            }
        }

        public List<Style> Styles
        {
            get
            {
                return new List<Style>
                {
                    GetStyle("fontawesome.min.css") , GetStyle("adminlte.min.css")
                };
            }
        }

        public IHandlerBuilder? Resources { get; }

        public IRenderer<ErrorModel> ErrorHandler { get; }

        public IRenderer<WebsiteModel> Renderer { get; }

        private bool HasLogo { get; }

        #endregion

        #region Initialization

        public AdminLteTheme(string? title, IHandlerBuilder? logo, bool enableFullscreen, Func<IRequest, IHandler, ValueTask<UserProfile?>>? userProfile,
            Func<IRequest, IHandler, ValueTask<string?>>? footerLeft, Func<IRequest, IHandler, ValueTask<string?>>? footerRight,
            Func<IRequest, IHandler, ValueTask<string?>>? sidebar, Func<IRequest, IHandler, ValueTask<SearchBox?>>? searchBox,
            Func<IRequest, IHandler, ValueTask<MenuSearchBox?>>? menuSearchBox, Func<IRequest, IHandler, ValueTask<string?>>? notifications, 
            IMenuProvider? headerMenu)
        {
            _Title = title;
            _EnableFullscreen= enableFullscreen;

            var resources = Layout.Create()
                                  .Add(Modules.IO.Resources.From(ResourceTree.FromAssembly("AdminLTE.resources")));

            HasLogo = (logo != null);

            if (logo != null)
            {
                resources.Add("logo.png", logo);
            }

            _UserProfile = userProfile;

            _FooterLeft = footerLeft;
            _FooterRight = footerRight;

            _Sidebar = sidebar;
            _SearchBox = searchBox;
            _MenuSearchBox = menuSearchBox;

            _HeaderMenu = headerMenu;
            _Notifications = notifications;

            Resources = resources;

            ErrorHandler = ModScriban.Template<ErrorModel>(Resource.FromAssembly("Error.html")).Build();

            Renderer = ModScriban.Template<WebsiteModel>(Resource.FromAssembly("Template.html")).Build();
        }

        #endregion

        #region Functionality

        public async ValueTask<object?> GetModelAsync(IRequest request, IHandler handler)
        {
            var userProfile = (_UserProfile != null) ? await _UserProfile(request, handler) : null;

            var footerLeft = (_FooterLeft != null) ? await _FooterLeft(request, handler) : null;
            var footerRight = (_FooterRight != null) ? await _FooterRight(request, handler) : null;

            var sidebar = (_Sidebar != null) ? await _Sidebar(request, handler) : null;
            var searchBox = (_SearchBox != null) ? await _SearchBox(request, handler) : null;
            var menuSearchBox = (_MenuSearchBox != null) ? await _MenuSearchBox(request, handler) : null;

            var headerMenu = (_HeaderMenu != null) ? await _HeaderMenu.GetMenuAsync(request, handler) : null;
            var notifications = (_Notifications != null) ? await _Notifications(request, handler) : null;

            return new ThemeModel(_Title, HasLogo, _EnableFullscreen, userProfile, footerLeft, footerRight, sidebar, searchBox, menuSearchBox, headerMenu, notifications);
        }

        private static Script GetScript(string name)
        {
            return new Script(name, false, Resource.FromAssembly($"scripts.{name}").Build());
        }

        private static Style GetStyle(string name)
        {
            return new Style(name, Resource.FromAssembly($"styles.{name}").Build());
        }

        #endregion

    }

}

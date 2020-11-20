using System;
using System.Collections.Generic;

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

        private readonly Func<IRequest, IHandler, UserProfile?>? _UserProfile;

        private readonly Func<IRequest, IHandler, string?>? _FooterLeft, _FooterRight, _Sidebar, _Notifications;

        private readonly Func<IRequest, IHandler, SearchBox?>? _SearchBox;

        private readonly IMenuProvider? _HeaderMenu;

        #region Supporting data structures

        public class ThemeModel
        {

            public string? Title { get; }

            public bool HasLogo { get; }

            public UserProfile? UserProfile { get; }

            public string? FooterLeft { get; }

            public string? FooterRight { get; }

            public string? Sidebar { get; }

            public SearchBox? SearchBox { get; }

            public List<ContentElement>? HeaderMenu { get; }

            public string? Notifications { get; }

            public ThemeModel(string? title, bool hasLogo, UserProfile? userProfile, 
                              string? footerLeft, string? footerRight, string? sidebar,
                              SearchBox? searchBox, List<ContentElement>? headerMenu,
                              string? notifications)
            {
                Title = title;

                HasLogo = hasLogo;
                UserProfile = userProfile;

                FooterLeft = footerLeft;
                FooterRight = footerRight;

                Sidebar = sidebar;
                SearchBox = searchBox;

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
                    GetStyle("fa-all.min.css") , GetStyle("adminlte.min.css")
                };
            }
        }

        public IHandlerBuilder? Resources { get; }

        public IRenderer<ErrorModel> ErrorHandler { get; }

        public IRenderer<WebsiteModel> Renderer { get; }

        private bool HasLogo { get; }

        #endregion

        #region Initialization

        public AdminLteTheme(string? title, IHandlerBuilder? logo, Func<IRequest, IHandler, UserProfile?>? userProfile,
            Func<IRequest, IHandler, string?>? footerLeft, Func<IRequest, IHandler, string?>? footerRight,
            Func<IRequest, IHandler, string?>? sidebar, Func<IRequest, IHandler, SearchBox?>? searchBox,
            Func<IRequest, IHandler, string?>? notifications, IMenuProvider? headerMenu)
        {
            _Title = title;

            var resources = Layout.Create()
                                  .Fallback(Modules.IO.Resources.From(ResourceTree.FromAssembly("AdminLTE.resources")));

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

            _HeaderMenu = headerMenu;
            _Notifications = notifications;

            Resources = resources;

            ErrorHandler = ModScriban.Template<ErrorModel>(Resource.FromAssembly("Error.html")).Build();

            Renderer = ModScriban.Template<WebsiteModel>(Resource.FromAssembly("Template.html")).Build();
        }

        #endregion

        #region Functionality

        public object? GetModel(IRequest request, IHandler handler)
        {
            var userProfile = (_UserProfile != null) ? _UserProfile(request, handler) : null;

            var footerLeft = (_FooterLeft != null) ? _FooterLeft(request, handler) : null;
            var footerRight = (_FooterRight != null) ? _FooterRight(request, handler) : null;

            var sidebar = (_Sidebar != null) ? _Sidebar(request, handler) : null;
            var searchBox = (_SearchBox != null) ? _SearchBox(request, handler) : null;

            var headerMenu = _HeaderMenu?.GetMenu(request, handler);
            var notifications = (_Notifications != null) ? _Notifications(request, handler) : null;

            return new ThemeModel(_Title, HasLogo, userProfile, footerLeft, footerRight, sidebar, searchBox, headerMenu, notifications);
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

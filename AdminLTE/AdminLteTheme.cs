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

        private readonly Func<IRequest, IHandler, string?>? _FooterLeft, _FooterRight;

        #region Supporting data structures

        public class ThemeModel
        {

            public string? Title { get; }

            public bool HasLogo { get; }

            public UserProfile? UserProfile { get; }

            public string? FooterLeft { get; }

            public string? FooterRight { get; }

            public ThemeModel(string? title, bool hasLogo, UserProfile? userProfile, 
                              string? footerLeft, string? footerRight)
            {
                Title = title;

                HasLogo = hasLogo;
                UserProfile = userProfile;

                FooterLeft = footerLeft;
                FooterRight = footerRight;
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
            Func<IRequest, IHandler, string?>? footerLeft, Func<IRequest, IHandler, string?>? footerRight)
        {
            _Title = title;

            var resources = Layout.Create()
                                  .Fallback(Static.Resources("AdminLTE.resources"));

            HasLogo = (logo != null);

            if (logo != null)
            {
                resources.Add("logo.png", logo);
            }

            _UserProfile = userProfile;

            _FooterLeft = footerLeft;
            _FooterRight = footerRight;

            Resources = resources;

            ErrorHandler = ModScriban.Template<ErrorModel>(Data.FromResource("Error.html")).Build();

            Renderer = ModScriban.Template<WebsiteModel>(Data.FromResource("Template.html")).Build();
        }

        #endregion

        #region Functionality

        public object? GetModel(IRequest request, IHandler handler)
        {
            var userProfile = (_UserProfile != null) ? _UserProfile(request, handler) : null;

            var footerLeft = (_FooterLeft != null) ? _FooterLeft(request, handler) : null;
            var footerRight = (_FooterRight != null) ? _FooterRight(request, handler) : null;

            return new ThemeModel(_Title, HasLogo, userProfile, footerLeft, footerRight);
        }

        private static Script GetScript(string name)
        {
            return new Script(name, false, Data.FromResource($"scripts.{name}").Build());
        }

        private static Style GetStyle(string name)
        {
            return new Style(name, Data.FromResource($"styles.{name}").Build());
        }

        #endregion

    }

}

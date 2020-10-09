using GenHTTP.Api.Content;
using GenHTTP.Api.Content.Templating;
using GenHTTP.Api.Content.Websites;
using GenHTTP.Api.Protocol;
using GenHTTP.Modules.IO;
using GenHTTP.Modules.Layouting;
using GenHTTP.Modules.Scriban;
using System.Collections.Generic;

namespace GenHTTP.Themes.AdminLTE
{

    public class AdminLteTheme : ITheme
    {
        private readonly string? _Title;

        #region Supporting data structures

        public class ThemeModel
        {

            public string? Title { get; }

            public bool HasLogo { get; }

            public ThemeModel(string? title, bool hasLogo)
            {
                Title = title;

                HasLogo = hasLogo;
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

        public AdminLteTheme(string? title, IHandlerBuilder? logo)
        {
            _Title = title;

            var resources = Layout.Create()
                                  .Fallback(Static.Resources("AdminLTE.resources"));

            HasLogo = (logo != null);

            if (logo != null)
            {
                resources.Add("logo.png", logo);
            }

            Resources = resources;

            ErrorHandler = ModScriban.Template<ErrorModel>(Data.FromResource("Error.html")).Build();

            Renderer = ModScriban.Template<WebsiteModel>(Data.FromResource("Template.html")).Build();
        }

        #endregion

        #region Functionality

        public object? GetModel(IRequest request, IHandler handler)
        {
            return new ThemeModel(_Title, HasLogo);
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

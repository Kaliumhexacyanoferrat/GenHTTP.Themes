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

        #region Supporting data structures

        public class ThemeModel
        {

            public string? Title { get; }

            public ThemeModel(string? title)
            {
                Title = title;
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

                };
            }
        }

        public List<Style> Styles
        {
            get
            {
                return new List<Style>
                {

                };
            }
        }

        public IHandlerBuilder? Resources { get; }

        public IRenderer<ErrorModel> ErrorHandler { get; }

        public IRenderer<WebsiteModel> Renderer { get; }

        #endregion

        #region Initialization

        public AdminLteTheme(string? title)
        {
            _Title = title;

            var resources = Layout.Create()
                                  .Fallback(Static.Resources("AdminLTE.resources"));

            Resources = resources;

            ErrorHandler = ModScriban.Template<ErrorModel>(Data.FromResource("Error.html")).Build();

            Renderer = ModScriban.Template<WebsiteModel>(Data.FromResource("Template.html")).Build();
        }

        #endregion

        #region Functionality

        public object? GetModel(IRequest request, IHandler handler)
        {
            return new ThemeModel(_Title);
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

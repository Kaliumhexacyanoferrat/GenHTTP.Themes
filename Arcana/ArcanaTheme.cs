using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using GenHTTP.Api.Content;
using GenHTTP.Api.Content.Templating;
using GenHTTP.Api.Content.Websites;
using GenHTTP.Api.Protocol;

using GenHTTP.Modules.IO;
using GenHTTP.Modules.Scriban;

namespace GenHTTP.Themes.Arcana
{

    public class ArcanaTheme : ITheme
    {
        private readonly string? _Title, _Copyright;

        private readonly Func<IRequest, IHandler, ValueTask<string?>>? _Footer;

        #region Supporting data structures

        public class ThemeModel
        {

            public string? Title { get; }

            public string? Copyright { get; }

            public string? Footer { get; }

            public ThemeModel(string? title, string? copyright, string? footer)
            {
                Title = title;
                Copyright = copyright;

                Footer = footer;
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
                    GetScript("jquery.js"), GetScript("jquery.dropotron.js"),
                    GetScript("browser.js"), GetScript("breakpoints.js"),
                    GetScript("util.js"), GetScript("main.js")
                };
            }
        }

        public List<Style> Styles
        {
            get { return new List<Style> { GetStyle("fontawesome.css"), GetStyle("main.css") }; }
        }

        public IHandlerBuilder? Resources { get; }

        public IRenderer<ErrorModel> ErrorHandler { get; }

        public IRenderer<WebsiteModel> Renderer { get; }

        #endregion

        #region Initialization

        public ArcanaTheme(string? title, string? copyright, Func<IRequest, IHandler, ValueTask<string?>>? footer)
        {
            _Title = title;
            _Copyright = copyright;

            _Footer = footer;

            Resources = Modules.IO.Resources.From(ResourceTree.FromAssembly("Arcana.resources"));

            ErrorHandler = ModScriban.Template<ErrorModel>(Resource.FromAssembly("Error.html")).Build();

            Renderer = ModScriban.Template<WebsiteModel>(Resource.FromAssembly("Template.html")).Build();
        }

        #endregion

        #region Functionality

        private static Script GetScript(string name)
        {
            return new Script(name, false, Resource.FromAssembly($"scripts.{name}").Build());
        }

        private static Style GetStyle(string name)
        {
            return new Style(name, Resource.FromAssembly($"styles.{name}").Build());
        }

        public async ValueTask<object?> GetModelAsync(IRequest request, IHandler handler)
        {
            var footer = (_Footer != null) ? await _Footer(request, handler) : null;

            return new ThemeModel(_Title, _Copyright, footer);
        }

        #endregion

    }

}

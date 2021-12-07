using System.Collections.Generic;
using System.Threading.Tasks;

using GenHTTP.Api.Content;
using GenHTTP.Api.Content.IO;
using GenHTTP.Api.Content.Templating;
using GenHTTP.Api.Content.Websites;
using GenHTTP.Api.Protocol;

using GenHTTP.Modules.IO;
using GenHTTP.Modules.Scriban;
using GenHTTP.Modules.Layouting;

namespace GenHTTP.Themes.Dimension
{

    public class DimensionTheme : ITheme
    {
        private readonly string? _Title, _Copyright;

        private readonly string _Icon;

        private readonly List<SinglePageSection> _Sections;

        #region Supporting data structures

        public class ThemeModel
        {

            public string? Title { get; }

            public string? Copyright { get; }

            public string Icon { get; }

            public List<SinglePageSection> Sections { get; }

            public ThemeModel(string? title, string? copyright, string icon, List<SinglePageSection> sections)
            {
                Title = title;
                Copyright = copyright;
                Icon = icon;
                Sections = sections;
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
                    GetScript("breakpoints.min.js"), GetScript("browser.min.js"),
                    GetScript("jquery.min.js"), GetScript("main.js"),
                    GetScript("util.js")
                };
            }
        }

        public List<Style> Styles
        {
            get { return new List<Style> { GetStyle("fontawesome-all.min.css"), GetStyle("main.css") /*, GetStyle("noscript.css")*/ }; }
        }

        public IHandlerBuilder? Resources { get; }

        public IRenderer<ErrorModel> ErrorHandler { get; }

        public IRenderer<WebsiteModel> Renderer { get; }

        #endregion

        #region Initialization

        public DimensionTheme(string? title, string? copyright, string icon, List<SinglePageSection> sections, IResource? background)
        {
            _Title = title;
            _Copyright = copyright;

            _Icon = icon;

            _Sections = sections;

            var resources = Layout.Create()
                                  .Fallback(Modules.IO.Resources.From(ResourceTree.FromAssembly("Dimension.resources")));

            if (background != null)
            {
                resources.Add("background.jpg", Download.From(background).Type(ContentType.ImageJpg));
            }

            Resources = resources;

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

        public ValueTask<object?> GetModelAsync(IRequest request, IHandler handler)
        {
            return new(new ThemeModel(_Title, _Copyright, _Icon, _Sections));
        }

        #endregion

    }

}

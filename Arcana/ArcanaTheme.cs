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
        private readonly string? _Title, _Copyright, _Footer1Title, _Footer2Title;

        private readonly IMenuProvider? _FooterMenu1, _FooterMenu2;

        #region Supporting data structures

        public class ThemeModel
        {

            public string? Title { get; }

            public string? Copyright { get; }

            public string? Title1 { get; }

            public List<ContentElement>? Footer1 { get; }

            public string? Title2 { get; }

            public List<ContentElement>? Footer2 { get; }

            public ThemeModel(string? title, string? copyright, string? footer1Title, List<ContentElement>? footer1, string? footer2Title, List<ContentElement>? footer2)
            {
                Title = title;
                Copyright = copyright;

                Footer1 = footer1;
                Title1 = footer1Title;

                Footer2 = footer2;
                Title2 = footer2Title;
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

        public ArcanaTheme(string? title, string? copyright, string? footer1Title, IMenuProvider? footerMenu1, string? footer2Title, IMenuProvider? footerMenu2)
        {
            _Title = title;
            _Copyright = copyright;

            _FooterMenu1 = footerMenu1;
            _Footer1Title = footer1Title;

            _FooterMenu2 = footerMenu2;
            _Footer2Title = footer2Title;

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

        public ValueTask<object?> GetModelAsync(IRequest request, IHandler handler)
        {
            var footer1 = _FooterMenu1?.GetMenu(request, handler);
            var footer2 = _FooterMenu2?.GetMenu(request, handler);

            return new ValueTask<object?>(new ThemeModel(_Title, _Copyright, _Footer1Title, footer1, _Footer2Title, footer2));
        }

        #endregion

    }

}

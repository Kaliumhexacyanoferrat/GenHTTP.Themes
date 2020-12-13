using System.Collections.Generic;
using System.Threading.Tasks;

using GenHTTP.Api.Content;
using GenHTTP.Api.Content.IO;
using GenHTTP.Api.Content.Templating;
using GenHTTP.Api.Content.Websites;
using GenHTTP.Api.Protocol;

using GenHTTP.Modules.IO;
using GenHTTP.Modules.Layouting;
using GenHTTP.Modules.Scriban;

namespace GenHTTP.Themes.Lorahost
{

    public class LorahostTheme : ITheme
    {
        private readonly string? _Copyright, _Title, _Subtitle, _Action, _ActionTitle;

        #region Supporting data structures

        public class ThemeModel
        {

            public string? Copyright { get; }

            public string? Title { get; }

            public string? Subtitle { get; }

            public string? Action { get; }

            public string? ActionTitle { get; }

            public ThemeModel(string? copyright, string? title, string? subtitle, string? action, string? actionTitle)
            {
                Copyright = copyright;
                Title = title;
                Subtitle = subtitle;
                Action = action;
                ActionTitle = actionTitle;
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
                    GetScript("jquery.js"), GetScript("jquery.ajaxchimp.js"), GetScript("mail-script.js"),
                    GetScript("owl.carousel.js"), GetScript("main.js"), GetScript("bootstrap.js")
                };
            }
        }

        public List<Style> Styles
        {
            get
            {
                return new List<Style>
                {
                    GetStyle("bootstrap.css"), GetStyle("fontawesome.css"),
                    GetStyle("owl.carousel.css"), GetStyle("owl.theme.default.css"),
                    GetStyle("themify.css"), GetStyle("style.css"),
                    GetStyle("additions.css")
                };
            }
        }

        public IHandlerBuilder? Resources { get; }

        public IRenderer<ErrorModel> ErrorHandler { get; }

        public IRenderer<WebsiteModel> Renderer { get; }

        #endregion

        #region Initialization

        public LorahostTheme(IResource? header, string? copyright, string? title, string? subtitle, string? action, string? actionTitle)
        {
            _Copyright = copyright;
            _Title = title;
            _Subtitle = subtitle;
            _Action = action;
            _ActionTitle = actionTitle;

            var resources = Layout.Create()
                                  .Fallback(Modules.IO.Resources.From(ResourceTree.FromAssembly("Lorahost.resources")));

            if (header != null)
            {
                resources.Add("header.jpg", Download.From(header).Type(ContentType.ImageJpg));
            }

            Resources = resources;

            ErrorHandler = ModScriban.Template<ErrorModel>(Resource.FromAssembly("Error.html")).Build();

            Renderer = ModScriban.Template<WebsiteModel>(Resource.FromAssembly("Template.html")).Build();
        }

        #endregion

        #region Functionality

        public ValueTask<object?> GetModelAsync(IRequest request, IHandler handler)
        {
            return new ValueTask<object?>(new ThemeModel(_Copyright, _Title, _Subtitle, _Action, _ActionTitle));
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

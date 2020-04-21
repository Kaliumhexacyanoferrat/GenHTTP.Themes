using System.Collections.Generic;

using GenHTTP.Api.Content;
using GenHTTP.Api.Content.Templating;
using GenHTTP.Api.Content.Websites;
using GenHTTP.Api.Protocol;

using GenHTTP.Modules.Core;
using GenHTTP.Modules.Scriban;

namespace GenHTTP.Modules.Themes.Lorahost
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
                    GetStyle("themify.css"), GetStyle("style.css")
                };
            }
        }

        public IHandlerBuilder? Resources { get; }

        public IRenderer<ErrorModel> ErrorHandler { get; }

        public IRenderer<WebsiteModel> Renderer { get; }

        #endregion

        #region Initialization

        public LorahostTheme(IResourceProvider? header, string? copyright, string? title, string? subtitle, string? action, string? actionTitle)
        {
            _Copyright = copyright;
            _Title = title;
            _Subtitle = subtitle;
            _Action = action;
            _ActionTitle = actionTitle;

            var resources = Layout.Create()
                                  .Fallback(Static.Resources("Lorahost.resources"));

            if (header != null)
            {
                resources.Add("header.jpg", Download.From(header).Type(ContentType.ImageJpg));
            }

            Resources = resources;

            ErrorHandler = ModScriban.Template<ErrorModel>(Data.FromResource("Error.html")).Build();

            Renderer = ModScriban.Template<WebsiteModel>(Data.FromResource("Template.html")).Build();
        }

        #endregion

        #region Functionality

        public object? GetModel(IRequest request, IHandler handler)
        {
            return new ThemeModel(_Copyright, _Title, _Subtitle, _Action, _ActionTitle);
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

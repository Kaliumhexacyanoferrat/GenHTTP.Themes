using System.Collections.Generic;

using GenHTTP.Api.Modules.Websites;
using GenHTTP.Api.Routing;
using GenHTTP.Core.Hosting;

using GenHTTP.Modules.Core;
using GenHTTP.Modules.Scriban;
using GenHTTP.Modules.Themes.Arcana;
using GenHTTP.Modules.Themes.Lorahost;

namespace GenHTTP.Themes.Demo
{

    public static class Program
    {
        public static int Main(string[] args)
        {
            return new ServerHost().Port(8080)
                                   .Console()
#if DEBUG
                                   .Development()
#endif
                                   .Router(Setup())
                                   .Run();
        }

        private static IRouterBuilder Setup()
        {
            var content = Layout.Create()
                                .Add("index", Page.From("Home", "This is the home page"), index: true)
                                .Add("content", Page.From("Content", "This is additional content"));

            var main = Layout.Create()
                             .Add("index", ModScriban.Page(Data.FromResource("Index.html")), index: true);

            foreach (var entry in GetThemes(content.Build()))
            {
                var website = Website.Create()
                                     .Theme(entry.Theme)
                                     .Content(content);

                main.Add(entry.Name, website);
            }

            return main;
        }

        private static IEnumerable<(string Name, ITheme Theme)> GetThemes(IRouter content)
        {
            yield return ("arcana", GetArcana(content));
            yield return ("lorahost", GetLorahost(content));
        }

        private static ITheme GetArcana(IRouter content)
        {
            return new ArcanaBuilder().Title("Arcana Theme")
                                      .Copyright("Copyright 2020")
                                      .Footer1("Footer 1", Menu.From(content))
                                      .Footer2("Footer 2", Menu.From(content))
                                      .Build();
        }

        private static ITheme GetLorahost(IRouter content)
        {
            return new LorahostBuilder().Title("Lorahost Theme")
                                        .Subtitle("Yet another theme")
                                        .Copyright("Copyright 2020")
                                        .Action("content", "Link to content!")
                                        .Header(Data.FromResource("header.jpg"))
                                        .Build();
        }

    }

}

using System.Collections.Generic;

using GenHTTP.Api.Content;
using GenHTTP.Api.Content.Websites;
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
                                   .Defaults()
                                   .Console()
#if DEBUG
                                   .Development()
#endif
                                   .Handler(Setup())
                                   .Run();
        }

        private static IHandlerBuilder Setup()
        {
            var content = Layout.Create()
                                .Index(Page.From("Home", "This is the home page"))
                                .Add("content", Page.From("Content", "This is additional content"));

            var main = Layout.Create()
                             .Index(ModScriban.Page(Data.FromResource("Index.html")).Title("GenHTTP Themes"));

            foreach (var entry in GetThemes())
            {
                var website = Website.Create()
                                     .Theme(entry.Theme)
                                     .Content(content);

                main.Add(entry.Name, website);
            }

            return main;
        }

        private static IEnumerable<(string Name, ITheme Theme)> GetThemes()
        {
            yield return ("arcana", GetArcana());
            yield return ("lorahost", GetLorahost());
        }

        private static ITheme GetArcana()
        {
            var menu = Menu.From("{website}");

            return new ArcanaBuilder().Title("Arcana Theme")
                                      .Copyright("Copyright 2020")
                                      .Footer1("Footer 1", menu)
                                      .Footer2("Footer 2", menu)
                                      .Build();
        }

        private static ITheme GetLorahost()
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

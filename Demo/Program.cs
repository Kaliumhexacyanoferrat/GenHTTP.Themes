using System.Collections.Generic;

using GenHTTP.Api.Content;
using GenHTTP.Api.Content.Websites;

using GenHTTP.Engine;

using GenHTTP.Modules.IO;
using GenHTTP.Modules.Scriban;
using GenHTTP.Modules.Practices;
using GenHTTP.Modules.Layouting;
using GenHTTP.Modules.Websites;
using GenHTTP.Modules.Placeholders;
using GenHTTP.Modules.Core;

using GenHTTP.Modules.Themes.Arcana;
using GenHTTP.Modules.Themes.Lorahost;
using GenHTTP.Themes.AdminLTE;

namespace GenHTTP.Themes.Demo
{

    public static class Program
    {
        public static int Main(string[] args)
        {
            
            return Host.Create()
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
            var index = Page.From("Home", "This is the home page")
                            .Description("Home Page");

            var additional = Page.From("Content", "This is additional content")
                                 .Description("Additional Content");

            var content = Layout.Create()
                                .Add("one", additional)
                                .Add("two", additional)
                                .Add("three", additional);

            var root = Layout.Create()
                                .Index(index)
                                .Add("content", content)
                                .Add("other", additional);

            var main = Layout.Create()
                             .Index(ModScriban.Page(Data.FromResource("Index.html")).Title("GenHTTP Themes"));

            var menu = Menu.Empty()
                           .Add("{website}", "Home")
                           .Add("content/", "Content", new List<(string, string)> { ("one/", "One"), ("two/", "Two"), ("three/", "Three") })
                           .Add("other", "Other");
                        
            foreach (var entry in GetThemes())
            {
                var website = Website.Create()
                                     .Theme(entry.Theme)
                                     .Menu(menu)
                                     .Content(root);

                main.Add(entry.Name, website);
            }

            return main;
        }

        private static IEnumerable<(string Name, ITheme Theme)> GetThemes()
        {
            yield return ("admin-lte", GetAdminLTE());
            yield return ("arcana", GetArcana());
            yield return ("lorahost", GetLorahost());
        }

        private static ITheme GetAdminLTE()
        {
            return new AdminLteBuilder().Title("AdminLTE Theme")
                                        .Logo(Download.FromResource("logo.png"))
                                        .Build();
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

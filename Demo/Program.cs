using System.Collections.Generic;

using GenHTTP.Api.Content;
using GenHTTP.Api.Content.Websites;
using GenHTTP.Api.Content.Templating;

using GenHTTP.Engine;

using GenHTTP.Modules.IO;
using GenHTTP.Modules.Scriban;
using GenHTTP.Modules.Practices;
using GenHTTP.Modules.Layouting;
using GenHTTP.Modules.Websites;
using GenHTTP.Modules.Placeholders;

using GenHTTP.Themes.AdminLTE;
using GenHTTP.Themes.Arcana;
using GenHTTP.Themes.Lorahost;
using GenHTTP.Themes.Dimension;

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

            var error = ModScriban.Page(Resource.FromString("This is a {{ rendering.error }}!"))
                                  .Description("Error page");

            var content = Layout.Create()
                                .Add("one", additional)
                                .Add("two", additional)
                                .Add("error", error)
                                .Index(additional);

            var root = Layout.Create()
                                .Index(index)
                                .Add("content", content)
                                .Add("other", additional);

            var main = Layout.Create()
                             .Add("avatar.png", Download.From(Resource.FromAssembly("avatar.png")))
                             .Index(ModScriban.Page(Resource.FromAssembly("Index.html")).Title("GenHTTP Themes"));

            var menu = Menu.Empty()
                           .Add("{website}", "Home")
                           .Add("content/", "Content", new List<(string, string)> { ("one/", "One"), ("two/", "Two"), ("error/", "Error") })
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
            yield return ("dimension", GetDimension());
            yield return ("lorahost", GetLorahost());
        }

        private static ITheme GetAdminLTE()
        {
            var menu = Menu.Empty()
                           .Add("{website}/", "Home");

            var notifications = ModScriban.Template<BasicModel>(Resource.FromAssembly("Notifications.html"))
                                          .Build();

            return new AdminLteBuilder().Title("AdminLTE Theme")
                                        .Logo(Download.From(Resource.FromAssembly("logo.png")))
                                        .UserProfile((r, h) => new UserProfile("Some User", "/avatar.png", ""))
                                        .FooterLeft((r, h) => "Footer text on the left ...")
                                        .FooterRight((r, h) => "... and on the right (template by <a href=\"https://adminlte.io\" target=\"blank\">AdminLTE.io</a>)")
                                        .Sidebar((r, h) => "<h5>Sidebar Content</h5><p>This content is placed on the sidebar. Awesome.</p>")
                                        .Search((r, h) => new SearchBox(""))
                                        .Header(menu)
                                        .Notifications(async (r, h) => await notifications.RenderAsync(new BasicModel(r, h)))
                                        .Build();
        }

        private static ITheme GetArcana()
        {
            var menu = Menu.From("{website}");

            return new ArcanaBuilder().Title("Arcana Theme")
                                      .Copyright("Copyright 2021")
                                      .Footer((_, __) => "<h3>Custom Footer</h3>")
                                      .Build();
        }

        private static ITheme GetDimension()
        {
            var menu = Menu.From("{website}");

            return new DimensionBuilder().Title("Dimension Theme")
                                         .Copyright("Copyright 2021")
                                         .Icon("gem")
                                         .Background(Resource.FromAssembly("header.jpg"))
                                         .Section("section-1", "Section 1", "This is additional content")
                                         .Section("section-2", "Section 2", "This is additional content")
                                         .Section("section-3", "Section 3", "This is additional content")
                                         .Build();
        }

        private static ITheme GetLorahost()
        {
            return new LorahostBuilder().Title("Lorahost Theme")
                                        .Subtitle("Yet another theme")
                                        .Copyright("Copyright 2021")
                                        .Action("content", "Link to content!")
                                        .Header(Resource.FromAssembly("header.jpg"))
                                        .Build();
        }

    }

}

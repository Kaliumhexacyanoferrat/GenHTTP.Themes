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
using System.Threading.Tasks;

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
                                .Add("three", additional)
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
            var menu = Menu.Empty()
                           .Add("{website}/", "Home");

            var notifications = ModScriban.Template<IBaseModel>(Resource.FromAssembly("Notifications.html"))
                                          .Build();

            return new AdminLteBuilder().Title("AdminLTE Theme")
                                        .Logo(Download.From(Resource.FromAssembly("logo.png")))
                                        .UserProfile((r, h) => new UserProfile("Some User", "/avatar.png", ""))
                                        .FooterLeft((r, h) => "Footer text on the left ...")
                                        .FooterRight((r, h) => "... and on the right (template by <a href=\"https://adminlte.io\" target=\"blank\">AdminLTE.io</a>)")
                                        .Sidebar((r, h) => "<h5>Sidebar Content</h5><p>This content is placed on the sidebar. Awesome.</p>")
                                        .Search((r, h) => new SearchBox(""))
                                        .Header(menu)
                                        .Notifications((r, h) => notifications.RenderAsync(new ViewModel(r, h)).Result)
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
                                        .Header(Resource.FromAssembly("header.jpg"))
                                        .Build();
        }

    }

}

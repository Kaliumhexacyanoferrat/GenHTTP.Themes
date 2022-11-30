using System.Collections.Generic;
using System.Threading.Tasks;

using GenHTTP.Api.Content;
using GenHTTP.Api.Content.Templating;
using GenHTTP.Api.Content.Websites;
using GenHTTP.Api.Protocol;

using GenHTTP.Modules.IO;
using GenHTTP.Modules.Scriban;

namespace GenHTTP.Themes.NoTheme
{

    public class NoThemeInstance : ITheme
    {

        #region Get-/Setters

        public List<Script> Scripts
        {
            get { return new(); }
        }

        public List<Style> Styles
        {
            get { return new(); }
        }

        public IRenderer<ErrorModel> ErrorHandler { get; }

        public IRenderer<WebsiteModel> Renderer { get; }

        public IHandlerBuilder? Resources => null;

        #endregion

        #region Initialization

        public NoThemeInstance()
        {
            ErrorHandler = ModScriban.Template<ErrorModel>(Resource.FromAssembly("Error.html")).Build();

            Renderer = ModScriban.Template<WebsiteModel>(Resource.FromAssembly("Template.html")).Build();
        }

        #endregion

        #region Functionality

        public ValueTask<object?> GetModelAsync(IRequest request, IHandler handler) => new();

        #endregion

    }

}

using GenHTTP.Api.Content;
using GenHTTP.Api.Content.Websites;

namespace GenHTTP.Themes.AdminLTE
{

    public class AdminLteBuilder : IThemeBuilder<AdminLteTheme>
    {
        private string? _Title;

        private IHandlerBuilder? _Logo;

        #region Functionality

        public AdminLteBuilder Title(string title)
        {
            _Title = title;
            return this;
        }

        /// <summary>
        /// Sets the logo of the application which will be shown
        /// in the upper left corner of rendered web pages.
        /// </summary>
        /// <param name="provider">A handler which will return a PNG logo to be displayed</param>
        /// <example>
        /// Logo(Download.FromFile("./images/mylogo.png"))
        /// </example>
        public AdminLteBuilder Logo(IHandlerBuilder provider)
        {
            _Logo = provider;
            return this;
        }

        public ITheme Build()
        {
            return new AdminLteTheme(_Title, _Logo);
        }

        #endregion

    }

}

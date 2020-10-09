using GenHTTP.Api.Content;
using GenHTTP.Api.Content.Templating;
using GenHTTP.Api.Content.Websites;
using GenHTTP.Api.Protocol;
using System;

namespace GenHTTP.Themes.AdminLTE
{

    public class AdminLteBuilder : IThemeBuilder<AdminLteTheme>
    {
        private string? _Title;

        private IHandlerBuilder? _Logo;

        private Func<IRequest, IHandler, UserProfile?>? _UserProfile;

        private Func<IRequest, IHandler, string?>? _FooterLeft, _FooterRight;

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

        public AdminLteBuilder UserProfile(Func<IRequest, IHandler, UserProfile?> provider)
        {
            _UserProfile = provider;
            return this;
        }

        public AdminLteBuilder FooterLeft(Func<IRequest, IHandler, string?> provider)
        {
            _FooterLeft = provider;
            return this;
        }

        public AdminLteBuilder FooterRight(Func<IRequest, IHandler, string?> provider)
        {
            _FooterRight = provider;
            return this;
        }

        public ITheme Build()
        {
            return new AdminLteTheme(_Title, _Logo, _UserProfile, _FooterLeft, _FooterRight);
        }

        #endregion

    }

}

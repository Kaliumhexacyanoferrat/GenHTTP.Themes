using System;

using GenHTTP.Api.Content;
using GenHTTP.Api.Content.Websites;
using GenHTTP.Api.Infrastructure;
using GenHTTP.Api.Protocol;

namespace GenHTTP.Themes.AdminLTE
{

    public class AdminLteBuilder : IThemeBuilder<AdminLteTheme>
    {
        private string? _Title;

        private IHandlerBuilder? _Logo;

        private Func<IRequest, IHandler, UserProfile?>? _UserProfile;

        private Func<IRequest, IHandler, string?>? _FooterLeft, _FooterRight, _Sidebar, _Notifications;

        private Func<IRequest, IHandler, SearchBox?>? _SearchBox;

        private IBuilder<IMenuProvider>? _HeaderMenu;

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

        /// <summary>
        /// Sets the handler which will be invoked to get the
        /// user profile to be rendered on the left side.
        /// </summary>
        public AdminLteBuilder UserProfile(Func<IRequest, IHandler, UserProfile?> provider)
        {
            _UserProfile = provider;
            return this;
        }

        /// <summary>
        /// Sets the handler which will be invoked to get the
        /// footer HTML to be rendered on the left side.
        /// </summary>
        public AdminLteBuilder FooterLeft(Func<IRequest, IHandler, string?> provider)
        {
            _FooterLeft = provider;
            return this;
        }

        /// <summary>
        /// Sets the handler which will be invoked to get the
        /// footer HTML to be rendered on the right side.
        /// </summary>
        public AdminLteBuilder FooterRight(Func<IRequest, IHandler, string?> provider)
        {
            _FooterRight = provider;
            return this;
        }

        /// <summary>
        /// Sets the handler which will be invoked to get the
        /// sidebar to be rendered on the right side.
        /// </summary>
        public AdminLteBuilder Sidebar(Func<IRequest, IHandler, string?> provider)
        {
            _Sidebar = provider;
            return this;
        }

        /// <summary>
        /// Sets the handler which will be invoked to get the
        /// search box to be rendered on the left side.
        /// </summary>
        public AdminLteBuilder Search(Func<IRequest, IHandler, SearchBox?> provider)
        {
            _SearchBox = provider;
            return this;
        }

        /// <summary>
        /// Sets the menu provider for the header menu.
        /// </summary>
        public AdminLteBuilder Header(IBuilder<IMenuProvider> menu)
        {
            _HeaderMenu = menu;
            return this;
        }

        /// <summary>
        /// Sets the handler to be invoked to render the notifications
        /// area in the upper right corner of your app.
        /// </summary>
        public AdminLteBuilder Notifications(Func<IRequest, IHandler, string?> notifications)
        {
            _Notifications = notifications;
            return this;
        }

        public ITheme Build()
        {
            return new AdminLteTheme(_Title, _Logo, _UserProfile, _FooterLeft, _FooterRight,
                _Sidebar, _SearchBox, _Notifications, _HeaderMenu?.Build());
        }

        #endregion

    }

}

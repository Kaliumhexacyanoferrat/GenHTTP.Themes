using System;
using System.Threading.Tasks;

using GenHTTP.Api.Content;
using GenHTTP.Api.Content.Websites;
using GenHTTP.Api.Protocol;

namespace GenHTTP.Themes.Arcana
{

    public class ArcanaBuilder : IThemeBuilder<ArcanaTheme>
    {
        private string? _Title, _Copyright;

        private Func<IRequest, IHandler, ValueTask<string?>>? _Footer;

        #region Functionality

        public ArcanaBuilder Title(string title)
        {
            _Title = title;
            return this;
        }

        public ArcanaBuilder Copyright(string copyright)
        {
            _Copyright = copyright;
            return this;
        }

        public ArcanaBuilder Footer(Func<IRequest, IHandler, ValueTask<string?>> provider)
        {
            _Footer = provider;
            return this;
        }

        public ArcanaBuilder Footer(Func<IRequest, IHandler, string?> provider)
        {
            _Footer = (r, h) => new ValueTask<string?>(provider(r, h));
            return this;
        }

        public ArcanaTheme Build()
        {
            return new ArcanaTheme(_Title, _Copyright, _Footer);
        }

        #endregion

    }

}

using GenHTTP.Api.Content.IO;
using GenHTTP.Api.Content.Websites;
using GenHTTP.Api.Infrastructure;

namespace GenHTTP.Themes.Lorahost
{

    public class LorahostBuilder : IThemeBuilder<LorahostTheme>
    {
        private string? _Copyright, _Title, _Subtitle, _Action, _ActionTitle;

        private IResource? _Header;

        #region Functionality

        public LorahostBuilder Copyright(string copyright)
        {
            _Copyright = copyright;
            return this;
        }

        public LorahostBuilder Title(string title)
        {
            _Title = title;
            return this;
        }

        public LorahostBuilder Subtitle(string subtitle)
        {
            _Subtitle = subtitle;
            return this;
        }

        public LorahostBuilder Action(string path, string text)
        {
            _Action = path;
            _ActionTitle = text;
            return this;
        }

        public LorahostBuilder Header(IBuilder<IResource> headerProvider) => Header(headerProvider.Build());

        public LorahostBuilder Header(IResource headerProvider)
        {
            _Header = headerProvider;
            return this;
        }

        public ITheme Build()
        {
            return new LorahostTheme(_Header, _Copyright, _Title, _Subtitle, _Action, _ActionTitle);
        }

        #endregion

    }

}

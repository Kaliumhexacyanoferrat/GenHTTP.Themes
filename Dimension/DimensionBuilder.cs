using GenHTTP.Api.Content.IO;
using GenHTTP.Api.Content.Websites;
using GenHTTP.Api.Infrastructure;

namespace GenHTTP.Themes.Dimension
{

    public class DimensionBuilder : IThemeBuilder<DimensionBuilder>
    {
        private string? _Title, _Copyright;

        private IResource? _Background;

        #region Functionality

        public DimensionBuilder Title(string title)
        {
            _Title = title;
            return this;
        }

        public DimensionBuilder Copyright(string copyright)
        {
            _Copyright = copyright;
            return this;
        }

        public DimensionBuilder Background(IBuilder<IResource> background) => Background(background.Build());

        public DimensionBuilder Background(IResource background)
        {
            _Background = background;
            return this;
        }

        public ITheme Build()
        {
            return new DimensionTheme(_Title, _Copyright, _Background);
        }

        #endregion

    }

}

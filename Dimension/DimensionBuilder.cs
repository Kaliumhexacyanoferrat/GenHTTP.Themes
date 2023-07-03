using System.Collections.Generic;

using GenHTTP.Api.Content.IO;
using GenHTTP.Api.Content.Websites;
using GenHTTP.Api.Infrastructure;

namespace GenHTTP.Themes.Dimension
{

    public class DimensionBuilder : IThemeBuilder<DimensionTheme>
    {
        private string? _Title, _Copyright;

        private string _Icon = "gem";

        private IResource? _Background;

        private readonly List<SinglePageSection> _Sections = new();

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

        public DimensionBuilder Section(string id, string title, string content)
        {
            _Sections.Add(new(id, title, content));
            return this;
        }

        public DimensionBuilder Icon(string icon)
        {
            _Icon = icon;
            return this;
        }

        public DimensionTheme Build()
        {
            return new DimensionTheme(_Title, _Copyright, _Icon, _Sections, _Background);
        }

        #endregion

    }

}

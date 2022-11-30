using GenHTTP.Api.Content.Websites;

namespace GenHTTP.Themes.NoTheme
{

    public class NoThemeBuilder : IThemeBuilder<NoThemeBuilder>
    {

        #region Functionality

        public ITheme Build()
        {
            return new NoThemeInstance();
        }

        #endregion

    }

}

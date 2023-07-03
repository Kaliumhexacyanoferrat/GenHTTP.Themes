using GenHTTP.Api.Content.Websites;

namespace GenHTTP.Themes.NoTheme
{

    public class NoThemeBuilder : IThemeBuilder<NoThemeInstance>
    {

        #region Functionality

        public NoThemeInstance Build()
        {
            return new NoThemeInstance();
        }

        #endregion

    }

}

using GenHTTP.Api.Content.Websites;

namespace GenHTTP.Themes.AdminLTE
{

    public class AdminLteBuilder : IThemeBuilder<AdminLteTheme>
    {
        private string? _Title;

        #region Functionality

        public AdminLteBuilder Title(string title)
        {
            _Title = title;
            return this;
        }

        public ITheme Build()
        {
            return new AdminLteTheme(_Title);
        }

        #endregion

    }

}

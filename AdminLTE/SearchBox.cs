namespace GenHTTP.Themes.AdminLTE
{

    public class SearchBox
    {

        #region Get-/Setters

        /// <summary>
        /// The route to be invoked if the user enters a search query.
        /// </summary>
        public string Action { get; }

        /// <summary>
        /// The method to be used to handle the search query ("get" or "post").
        /// </summary>
        public string Method { get; }

        /// <summary>
        /// The placeholder to be shown if no text is entered in the search box.
        /// </summary>
        public string Placeholder { get; }

        #endregion

        #region Initialization

        public SearchBox(string action, string method = "get", string placeholder = "Search")
        {
            Action = action;
            Method = method;
            Placeholder = placeholder;
        }

        #endregion

    }

}

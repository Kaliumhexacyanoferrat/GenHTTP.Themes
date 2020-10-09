using System;
using System.Collections.Generic;
using System.Text;

namespace GenHTTP.Themes.AdminLTE
{
    
    public class UserProfile
    {

        #region Get-/Setters

        /// <summary>
        /// The display name of the user to be shown.
        /// </summary>
        public string Username { get; }

        /// <summary>
        /// The route to the avatar of the user.
        /// </summary>
        public string Avatar { get; }

        /// <summary>
        /// The route to be opened when the user clicks
        /// on the user profile.
        /// </summary>
        public string Action { get; }

        #endregion

        #region Initialization

        public UserProfile(string username, string avatar, string action)
        {
            Username = username;
            Avatar = avatar;
            Action = action;
        }

        #endregion

    }

}

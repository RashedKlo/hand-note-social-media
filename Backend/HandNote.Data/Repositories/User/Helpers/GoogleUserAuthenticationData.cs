using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HandNote.Data.Models;

namespace HandNote.Data.Repositories.User.Helpers
{
    public class GoogleUserAuthenticationData
    {

        public Models.User? User { get; set; }
        public Models.UserSession? UserSession { get; set; }
        public string AccessToken { get; set; }
        public bool IsExistingUser { get; set; }
        public GoogleUserAuthenticationData(Models.User user, UserSession userSession, string AccessToken, bool IsExistingUser)
        {
            this.User = user;
            this.UserSession = userSession;
            this.AccessToken = AccessToken;
            this.IsExistingUser = IsExistingUser;
        }
    }
}

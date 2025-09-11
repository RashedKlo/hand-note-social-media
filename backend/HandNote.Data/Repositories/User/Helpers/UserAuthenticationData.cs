using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HandNote.Data.Models;

namespace HandNote.Data.Repositories.User.Helpers
{
    public class UserAuthenticationData
    {
        public Models.User? User { get; set; }
        public Models.UserSession? UserSession { get; set; }
        public string AccessToken { get; set; }
        public UserAuthenticationData(Models.User user, UserSession userSession, string AccessToken)
        {
            this.User = user;
            this.UserSession = userSession;
            this.AccessToken = AccessToken;
        }
    }
}
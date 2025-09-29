using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HandNote.Data.DTOs.UserPrivacySettings.RequestBodies
{
        public class UserPrivacySettingsUpdateRequestBody
    {

        [RegularExpression("^(public|friends|only_me)$", ErrorMessage = "DefaultPostAudience must be one of: public, friends, only_me.")]
        public string? DefaultPostAudience { get; set; }

        [RegularExpression("^(public|friends|only_me)$", ErrorMessage = "ProfileVisibility must be one of: public, friends, only_me.")]
        public string? ProfileVisibility { get; set; }

        [RegularExpression("^(public|friends|only_me)$", ErrorMessage = "FriendListVisibility must be one of: public, friends, only_me.")]
        public string? FriendListVisibility { get; set; }

        [RegularExpression("^(public|friends_of_friends)$", ErrorMessage = "FriendRequestFrom must be one of: public, friends_of_friends.")]
        public string? FriendRequestFrom { get; set; }

        [RegularExpression("^(public|friends_of_friends|friends)$", ErrorMessage = "MessageFrom must be one of: public, friends_of_friends, friends.")]
        public string? MessageFrom { get; set; }
    }
}
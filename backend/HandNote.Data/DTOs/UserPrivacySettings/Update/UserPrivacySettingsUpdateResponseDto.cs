using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HandNote.Data.DTOs.UserPrivacySettings.Update
{
     public class UserPrivacySettingsUpdateResponseDto
    {
        public int UserId { get; set; }

        public string? DefaultPostAudience { get; set; }

        public string? ProfileVisibility { get; set; }

        public string? FriendListVisibility { get; set; }

        public string? FriendRequestFrom { get; set; }

        public string? MessageFrom { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public string? Username { get; set; }

        public string? Email { get; set; }
    }
}
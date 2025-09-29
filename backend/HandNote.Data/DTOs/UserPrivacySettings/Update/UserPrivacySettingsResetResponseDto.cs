using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HandNote.Data.DTOs.UserPrivacySettings.Update
{
  public class UserPrivacySettingsResetResponseDto
    {
        public int UserId { get; set; }

        public string? DefaultPostAudience { get; set; }

        public string? ProfileVisibility { get; set; }

        public string? FriendListVisibility { get; set; }

        public string? FriendRequestFrom { get; set; }

        public string? MessageFrom { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public string? Username { get; set; }


    }
}
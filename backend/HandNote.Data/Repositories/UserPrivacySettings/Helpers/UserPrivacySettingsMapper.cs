using System;
using HandNote.Data.DTOs.UserPrivacySettings.Update;
using HandNote.Data.Helpers;
using Microsoft.Data.SqlClient;

namespace HandNote.Data.Repositories.UserPrivacySettings.Helpers
{
    public static class UserPrivacySettingsMapper
    {
        public static UserPrivacySettingsResetResponseDto MapResetResponseFromReader(SqlDataReader reader)
        {
            return new UserPrivacySettingsResetResponseDto
            {
                UserId = reader.GetValueOrDefault<int>("UserId"),
                DefaultPostAudience = reader.GetValueOrDefault<string>("DefaultPostAudience"),
                ProfileVisibility = reader.GetValueOrDefault<string>("ProfileVisibility"),
                FriendListVisibility = reader.GetValueOrDefault<string>("FriendListVisibility"),
                FriendRequestFrom = reader.GetValueOrDefault<string>("FriendRequestFrom"),
                MessageFrom = reader.GetValueOrDefault<string>("MessageFrom"),
                UpdatedAt = reader.GetValueOrDefault<DateTime?>("UpdatedAt"),
                Username = reader.GetValueOrDefault<string>("Username"),
            };
        }

        public static UserPrivacySettingsUpdateResponseDto MapUpdateResponseFromReader(SqlDataReader reader)
        {
            return new UserPrivacySettingsUpdateResponseDto
            {
                UserId = reader.GetValueOrDefault<int>("UserId"),
                DefaultPostAudience = reader.GetValueOrDefault<string>("DefaultPostAudience"),
                ProfileVisibility = reader.GetValueOrDefault<string>("ProfileVisibility"),
                FriendListVisibility = reader.GetValueOrDefault<string>("FriendListVisibility"),
                FriendRequestFrom = reader.GetValueOrDefault<string>("FriendRequestFrom"),
                MessageFrom = reader.GetValueOrDefault<string>("MessageFrom"),
                CreatedAt = reader.GetValueOrDefault<DateTime?>("CreatedAt"),
                UpdatedAt = reader.GetValueOrDefault<DateTime?>("UpdatedAt"),
                Username = reader.GetValueOrDefault<string>("Username"),
                Email = reader.GetValueOrDefault<string>("Email"),
            };
        }
    }
}
using System;
using System.Data;
using HandNote.Data.DTOs.User;
using HandNote.Data.Helpers;
using Microsoft.Data.SqlClient;

namespace HandNote.Data.Repositories.User.Helpers
{
    public static class UserMapper
    {
        public static Models.User MapUserFromReader(SqlDataReader reader)
        {
            return new Models.User
            {
                UserId = reader.GetValue<int>("user_id"),
                UserName = reader.GetValue<string>("username"),
                Email = reader.GetValue<string>("email"),
                MobileNumber = reader.GetValueOrDefault<string>("mobile_number"),
                PasswordHash = reader.GetValueOrDefault<string>("password_hash"),
                FirstName = reader.GetValue<string>("first_name"),
                LastName = reader.GetValue<string>("last_name"),
                BirthDate = reader.GetValueOrDefault<DateTime>("birth_date"),
                GenderPreference = reader.GetValueOrDefault<string>("gender_preference"),
                AboutMe = reader.GetValueOrDefault<string>("about_me"),
                CityId = reader.GetValue<int>("city_id"),
                WorksAt = reader.GetValueOrDefault<string>("works_at"),
                StudiedAt = reader.GetValueOrDefault<string>("studied_at"),
                RelationshipStatus = reader.GetValue<string>("relationship_status"),
                ProfilePhotoUrl = reader.GetValueOrDefault<string>("profile_photo_url"),
                IsVerified = reader.GetValue<bool>("is_verified"),
                IsActive = reader.GetValue<bool>("is_active"),
                ProfileCompleted = reader.GetValue<bool>("profile_completed"),
                EmailConfirmed = reader.GetValue<bool>("email_confirmed"),
                MobileConfirmed = reader.GetValue<bool>("mobile_confirmed"),
                AccountPrivacy = reader.GetValue<string>("account_privacy"),
                CreatedDate = reader.GetValue<DateTime>("created_date"),
                LastUpdated = reader.GetValue<DateTime>("last_updated")
            };
        }
        public static Models.UserSession MapUserSessionFromReader(SqlDataReader reader)
        {
            return new Models.UserSession
            {
                SessionId = reader.GetValue<int>("SessionId"),
                UserId = reader.GetValue<int>("user_id"),
                RefreshToken = reader.GetValue<string>("RefreshToken"),
                ExpiresAt = DateTime.UtcNow.AddDays(30),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Provider = null
            };
        }
    }
}
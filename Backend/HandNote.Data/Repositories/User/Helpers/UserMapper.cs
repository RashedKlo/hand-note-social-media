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
                UserId = reader.GetValueOrDefault<int>("UserId"),
                UserName = reader.GetValue<string>("UserName"),
                Email = reader.GetValueOrDefault<string>("Email"),
                MobileNumber = reader.GetValueOrDefault<string>("MobileNumber"),
                PasswordHash = reader.GetValueOrDefault<string>("PasswordHash"),
                FirstName = reader.GetValueOrDefault<string>("FirstName"),
                LastName = reader.GetValueOrDefault<string>("LastName"),
                BirthDate = reader.GetValueOrDefault<DateTime>("BirthDate"),
                GenderPreference = reader.GetValueOrDefault<string>("GenderPreference"),
                AboutMe = reader.GetValueOrDefault<string>("AboutMe"),
                CityId = reader.GetValueOrDefault<int>("CityId"),
                WorksAt = reader.GetValueOrDefault<string>("WorksAt"),
                StudiedAt = reader.GetValueOrDefault<string>("StudiedAt"),
                RelationshipStatus = reader.GetValueOrDefault<string>("RelationshipStatus"),
                ProfilePhotoUrl = reader.GetValueOrDefault<string>("ProfilePhotoUrl"),
                IsVerified = reader.GetValueOrDefault<bool>("IsVerified"),
                IsActive = reader.GetValueOrDefault<bool>("IsActive"),
                ProfileCompleted = reader.GetValueOrDefault<bool>("ProfileCompleted"),
                EmailConfirmed = reader.GetValueOrDefault<bool>("EmailConfirmed"),
                MobileConfirmed = reader.GetValueOrDefault<bool>("MobileConfirmed"),
                AccountPrivacy = reader.GetValueOrDefault<string>("AccountPrivacy"),
                CreatedDate = reader.GetValueOrDefault<DateTime>("CreatedDate"),
                LastUpdated = reader.GetValueOrDefault<DateTime>("LastUpdated")
            };
        }
        public static Models.UserSession MapUserSessionFromReader(SqlDataReader reader)
        {
            return new Models.UserSession
            {
                SessionId = reader.GetValueOrDefault<int>("SessionId"),
                UserId = reader.GetValueOrDefault<int>("UserId"),
                RefreshToken = reader.GetValueOrDefault<string>("RefreshToken"),
                ExpiresAt = DateTime.UtcNow.AddDays(30),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Provider = null
            };
        }
    }
}
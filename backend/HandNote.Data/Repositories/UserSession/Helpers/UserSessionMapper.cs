using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HandNote.Data.DTOs.UserSession.Query;
using Microsoft.Data.SqlClient;
using HandNote.Data.Helpers;
using HandNote.Data.DTOs.UserSession.Logout;

namespace HandNote.Data.Repositories.UserSession.Helpers
{
    public class UserSessionMapper
    {

        public static ActiveSessionDto MapActiveSessionFromReader(SqlDataReader reader)
        {
            return new ActiveSessionDto
            {
                SessionId = reader.GetValueOrDefault<int>("SessionId"),
                RefreshToken = reader.GetValueOrDefault<string>("RefreshToken"),
                ExpiresAt = reader.GetValueOrDefault<DateTime?>("ExpiresAt"),
                CreatedAt = reader.GetValueOrDefault<DateTime?>("CreatedAt"),
                UpdatedAt = reader.GetValueOrDefault<DateTime?>("UpdatedAt"),
                Provider = reader.GetValueOrDefault<string>("Provider"),
                MinutesUntilExpiry = reader.GetValueOrDefault<int?>("MinutesUntilExpiry"),
                SessionAgeHours = reader.GetValueOrDefault<int?>("SessionAgeHours"),
                TotalActiveSessions = reader.GetValueOrDefault<int>("TotalActiveSessions")
            };
        }
        public static LogoutUserResponseDto MapLogoutUserResponseFromReader(SqlDataReader reader)
        {
            return new LogoutUserResponseDto
            {
                UserId = reader.GetValueOrDefault<int>("UserId"),
                UserName = reader.GetValueOrDefault<string>("UserName"),
                Email = reader.GetValueOrDefault<string>("Email"),
                SessionId = reader.GetValueOrDefault<int>("SessionId"),
            };
        }
        public static LogoutAllSessionsResponseDto MapLogoutAllSessionsResponseFromReader(SqlDataReader reader)
        {
            return new LogoutAllSessionsResponseDto
            {
                UserId = reader.GetValueOrDefault<int?>("UserId"),
                UserName = reader.GetValueOrDefault<string>("UserName"),
                Email = reader.GetValueOrDefault<string>("Email"),
                SessionsLoggedOut = reader.GetValueOrDefault<int>("SessionsLoggedOut"),
            };
        }

    }
}
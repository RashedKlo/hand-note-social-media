using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Data.SqlClient;
using HandNote.Data.Settings;
using HandNote.Data.Repositories.User.Helpers;
namespace HandNote.Data.Repositories.User.Queries
{
    public class GetUserByIdQuery
    {
        public static async Task<Models.User?> ExecuteAsync(int userId, ILogger<UserRepository> logger)
        {
            logger.LogDebug("Getting user by ID: {UserId}", userId);

            const string sql = @"
                SELECT user_id, username, email, mobile_number, password_hash, first_name, last_name,
                       birth_date, gender_preference, about_me, city_id, works_at, studied_at,
                       relationship_status, profile_photo_url, is_verified, is_active, profile_completed,
                       email_confirmed, mobile_confirmed, account_privacy, created_date, last_updated
                FROM users 
                WHERE user_id = @UserId AND is_active = 1";

            try
            {
                using var connection = new SqlConnection(DBSettings.connectionString);
                await connection.OpenAsync();

                using var command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@UserId", userId);

                using var reader = await command.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                {
                    var user = UserMapper.MapUserFromReader(reader);
                    logger.LogDebug("User found with ID: {UserId}", userId);
                    return user;
                }

                logger.LogDebug("User not found with ID: {UserId}", userId);
                return null;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error getting user by ID: {UserId}", userId);
                throw new InvalidOperationException("Failed to retrieve user.", ex);
            }
        }
    }
}
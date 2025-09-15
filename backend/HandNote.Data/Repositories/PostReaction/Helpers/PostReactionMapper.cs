using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HandNote.Data.DTOs.PostReaction.Create;
using HandNote.Data.DTOs.PostReaction.Delete;
using HandNote.Data.Helpers;
using Microsoft.Data.SqlClient;

namespace HandNote.Data.Repositories.PostReaction.Helpers
{
    public class PostReactionMapper
    {
        /// <summary>
        /// Maps SqlDataReader result to PostReactionCreateResponseDto
        /// Used for sp_AddPostReaction stored procedure results
        /// </summary>
        /// <param name="reader">SqlDataReader containing the stored procedure result</param>
        /// <returns>Mapped PostReactionCreateResponseDto</returns>
        public static PostReactionCreateResponseDto MapCreateResponseFromReader(SqlDataReader reader)
        {
            if (reader == null)
                throw new ArgumentNullException(nameof(reader));

            return new PostReactionCreateResponseDto
            {
                ReactionId = reader.GetValue<int>("ReactionId"),
                PostId = reader.GetValue<int>("PostId"),
                UserId = reader.GetValue<int>("UserId"),
                ReactionType = reader.GetValue<string>("ReactionType"),
                CreatedAt = reader.GetValueOrDefault<DateTime>("CreatedAt"),
                UpdatedAt = reader.GetValueOrDefault<DateTime>("UpdatedAt"),

            };
        }
        /// <summary>
        /// Maps SqlDataReader result to PostReactionDeleteResponseDto
        /// Used for sp_RemovePostReaction stored procedure results
        /// </summary>
        /// <param name="reader">SqlDataReader containing the stored procedure result</param>
        /// <returns>Mapped PostReactionDeleteResponseDto</returns>
        public static PostReactionDeleteResponseDto MapDeleteResponseFromReader(SqlDataReader reader)
        {
            if (reader == null)
                throw new ArgumentNullException(nameof(reader));

            return new PostReactionDeleteResponseDto
            {
                ReactionId = reader.GetValue<int>("ReactionId"),
                PostId = reader.GetValue<int>("PostId"),
                UserId = reader.GetValue<int>("UserId"),
                RemovedReactionType = reader.GetValue<string>("RemovedReactionType"),
                RemovedAt = reader.GetValueOrDefault<DateTime>("RemovedAt"),
            };
        }
    }
}
using System;
using HandNote.Data.DTOs.CommentReaction;
using HandNote.Data.DTOs.CommentReaction.Create;
using HandNote.Data.DTOs.CommentReaction.Delete;
using HandNote.Data.Helpers;
using Microsoft.Data.SqlClient;

namespace HandNote.Data.Repositories.CommentReaction.Helpers
{
    public static class CommentReactionMapper
    {
        public static CommentReactionCreateResponseDto MapCreateResponseFromReader(SqlDataReader reader)
        {

            return new CommentReactionCreateResponseDto
            {
                ReactionId = reader.GetValueOrDefault<int>("ReactionId"),
                CommentId = reader.GetValueOrDefault<int>("CommentId"),
                UserId = reader.GetValueOrDefault<int>("UserId"),
                ReactionType = reader.GetValueOrDefault<string>("ReactionType"),
                IsUpdate = reader.GetValueOrDefault<bool>("IsUpdate"),
                CreatedAt = reader.GetValueOrDefault<DateTime>("CreatedAt"),
                UpdatedAt = reader.GetValueOrDefault<DateTime>("UpdatedAt"),
            };
        }

        public static CommentReactionDeleteResponseDto MapDeleteResponseFromReader(SqlDataReader reader)
        {
            if (reader == null)
                throw new ArgumentNullException(nameof(reader));

            return new CommentReactionDeleteResponseDto
            {
                ReactionId = reader.GetValueOrDefault<int>("ReactionId"),
                CommentId = reader.GetValueOrDefault<int>("CommentId"),
                UserId = reader.GetValueOrDefault<int>("UserId"),
                RemovedReactionType = reader.GetValueOrDefault<string>("RemovedReactionType"),
                RemovedAt = reader.GetValueOrDefault<DateTime>("RemovedAt"),
            };
        }
    }
}

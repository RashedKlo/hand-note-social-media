using System;
using Microsoft.Data.SqlClient;
using HandNote.Data.Helpers;
using HandNote.Data.DTOs.Comment.Create;
using HandNote.Data.DTOs.Comment.Delete;
using HandNote.Data.DTOs.Comment.Update;
using HandNote.Data.DTOs.Comment.Queries;

namespace HandNote.Data.Repositories.Comment.Helpers
{
    public static class CommentMapper
    {
        public static CommentCreateResponseDto MapCreateResponseFromReader(SqlDataReader reader)
        {
            if (reader == null)
                throw new ArgumentNullException(nameof(reader));

            return new CommentCreateResponseDto
            {
                CommentId = reader.GetValue<int>("CommentId"),
                PostId = reader.GetValueOrDefault<int>("PostId"),
                UserId = reader.GetValueOrDefault<int>("UserId"),
                ParentCommentId = reader.GetValueOrDefault<int?>("ParentCommentId"),
                Content = reader.GetValue<string>("Content"),
                MediaFile = reader.GetValueOrDefault<string>("MediaFile"),
                IsEdited = reader.GetValueOrDefault<bool>("IsEdited"),
                EditedAt = reader.GetValueOrDefault<DateTime?>("EditedAt"),
                IsDeleted = reader.GetValueOrDefault<bool>("IsDeleted"),
                IsPinned = reader.GetValueOrDefault<bool>("IsPinned"),
                CreatedAt = reader.GetValueOrDefault<DateTime?>("CreatedAt"),
                UpdatedAt = reader.GetValueOrDefault<DateTime?>("UpdatedAt"),
                DeletedAt = reader.GetValueOrDefault<DateTime?>("DeletedAt"),

            };
        }

        public static CommentUpdateResponseDto MapUpdateResponseFromReader(SqlDataReader reader)
        {
            if (reader == null)
                throw new ArgumentNullException(nameof(reader));

            return new CommentUpdateResponseDto
            {
                CommentId = reader.GetValueOrDefault<int>("CommentId"),
                UserId = reader.GetValueOrDefault<int>("UserId"),
                Content = reader.GetValue<string>("Content"),
                MediaFile = reader.GetValueOrDefault<string>("MediaFile"),
                IsEdited = reader.GetValueOrDefault<bool?>("IsEdited"),
                EditedAt = reader.GetValueOrDefault<DateTime?>("EditedAt"),
                UpdatedAt = reader.GetValueOrDefault<DateTime?>("UpdatedAt"),

            };
        }

        public static CommentDeleteResponseDto MapDeleteResponseFromReader(SqlDataReader reader)
        {
            if (reader == null)
                throw new ArgumentNullException(nameof(reader));

            return new CommentDeleteResponseDto
            {
                CommentId = reader.GetValueOrDefault<int>("CommentId"),
                UserId = reader.GetValueOrDefault<int>("UserId"),
                IsDeleted = reader.GetValueOrDefault<bool?>("IsDeleted"),
                DeletedAt = reader.GetValueOrDefault<DateTime?>("DeletedAt"),
                UpdatedAt = reader.GetValueOrDefault<DateTime?>("UpdatedAt"),

            };
        }

        public static CommentExistenceResponseDto MapExistenceResponseFromReader(SqlDataReader reader)
        {
            if (reader == null)
                throw new ArgumentNullException(nameof(reader));

            return new CommentExistenceResponseDto
            {
                CommentId = reader.GetValueOrDefault<int>("CommentId"),
                CommentExists = reader.GetValueOrDefault<bool?>("CommentExists"),
                IsDeleted = reader.GetValueOrDefault<bool?>("IsDeleted"),

            };
        }
    }
}
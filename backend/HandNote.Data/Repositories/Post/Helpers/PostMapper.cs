using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HandNote.Data.Helpers;
using Microsoft.Data.SqlClient;

namespace HandNote.Data.Repositories.Post.Helpers
{
    public static class PostMapper
    {
        public static Models.Post? MapPostFromReader(SqlDataReader reader)
        {
            var postId = reader.GetValueOrDefault<int>("PostId");
            if (postId is default(int))
            {
                return null;
            }

            return new Models.Post
            {
                PostId = postId,
                UserId = reader.GetValueOrDefault<int>("UserId"),
                Content = reader.GetValueOrDefault<string>("Content"),
                PostType = reader.GetValueOrDefault<string>("PostType") ?? "text",
                SharedPostId = reader.GetValueOrDefault<int>("SharedPostId"),
                HasMedia = reader.GetValueOrDefault<bool>("HasMedia"),
                IsEdited = reader.GetValueOrDefault<bool>("IsEdited"),
                EditedAt = reader.GetValueOrDefault<DateTime>("EditedAt"),
                ViewsCount = reader.GetValueOrDefault<int>("ViewsCount"),
                IsDeleted = reader.GetValueOrDefault<bool>("IsDeleted"),
                AllowsComments = reader.GetValueOrDefault<bool>("AllowsComments"),
                AllowsShares = reader.GetValueOrDefault<bool>("AllowsShares"),
                CreatedAt = reader.GetValueOrDefault<DateTime>("CreatedAt"),
                UpdatedAt = reader.GetValueOrDefault<DateTime>("UpdatedAt"),
                DeletedAt = reader.GetValueOrDefault<DateTime>("DeletedAt")
            };
        }

    }
}
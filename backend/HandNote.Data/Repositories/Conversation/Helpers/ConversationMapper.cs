using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HandNote.Data.DTOs.Conversation.Create;
using HandNote.Data.DTOs.Conversation.Query;
using HandNote.Data.Helpers;
using Microsoft.Data.SqlClient;

namespace HandNote.Data.Repositories.Conversation.Helpers
{
    public class ConversationMapper
    {
        public static ConversationCreateResponseDto MapCreateResponseFromReader(SqlDataReader reader)
        {
            return new ConversationCreateResponseDto
            {
                ConversationId = reader.GetValueOrDefault<int>("ConversationId"),
                User1Id = reader.GetValueOrDefault<int>("User1Id"),
                User2Id = reader.GetValueOrDefault<int>("User2Id"),
                CreatedBy = reader.GetValueOrDefault<int>("CreatedBy"),
                CreatedAt = reader.GetValueOrDefault<DateTime>("CreatedAt"),
                UpdatedAt = reader.GetValueOrDefault<DateTime>("UpdatedAt"),
                LastActivity = reader.GetValueOrDefault<DateTime>("LastActivity"),
                IsActive = reader.GetValueOrDefault<bool>("IsActive"),
                FriendUsername = reader.GetValueOrDefault<string>("FriendUsername") ?? string.Empty,
                FriendFirstName = reader.GetValueOrDefault<string>("FriendFirstName") ?? string.Empty,
                FriendProfilePicture = reader.GetValueOrDefault<string>("FriendProfilePicture")
            };
        }
        public static ConversationExistenceResponseDto MapExistenceResponseFromReader(SqlDataReader reader)
        {
            return new ConversationExistenceResponseDto
            {
                ConversationId = reader.GetValueOrDefault<int>("ConversationId"),
                UserId = reader.GetValueOrDefault<int>("UserId"),
                ConversationExists = reader.GetValueOrDefault<bool>("ConversationExists"),
                IsUserPartOfConversation = reader.GetValueOrDefault<bool>("IsUserPartOfConversation")
            };
        }

    }
}
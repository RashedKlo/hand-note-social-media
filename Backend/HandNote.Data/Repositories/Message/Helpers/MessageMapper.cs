using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HandNote.Data.DTOs.Message.Add;
using HandNote.Data.DTOs.Message.Get;
using HandNote.Data.Helpers;
using Microsoft.Data.SqlClient;

namespace HandNote.Data.Repositories.Message.Helpers
{
    public class MessageMapper
    {
        public static MessagesGetResponseDto MapMessageFromReader(SqlDataReader reader)
        {
            return new MessagesGetResponseDto
            {
                MessageId = reader.GetValueOrDefault<int>("MessageId"),
                ConversationId = reader.GetValueOrDefault<int>("ConversationId"),
                SenderId = reader.GetValueOrDefault<int>("SenderId"),
                SenderUsername = reader.GetValueOrDefault<string>("SenderUsername") ?? string.Empty,
                SenderFirstName = reader.GetValueOrDefault<string>("SenderFirstName") ?? string.Empty,
                SenderProfilePicture = reader.GetValueOrDefault<string>("SenderProfilePicture"),
                Content = reader.GetValueOrDefault<string>("Content") ?? string.Empty,
                MessageType = reader.GetValueOrDefault<string>("MessageType") ?? string.Empty,
                SentAt = reader.GetValueOrDefault<DateTime>("SentAt"),
                EditedAt = reader.GetValueOrDefault<DateTime?>("EditedAt"),
                IsDeleted = reader.GetValueOrDefault<bool>("IsDeleted"),
                IsRead = reader.GetValueOrDefault<bool>("IsRead"),
                ReadAt = reader.GetValueOrDefault<DateTime>("ReadAt"),
                MediaFile = reader.GetValueOrDefault<string>("MediaFile")
            };
        }
        public static MessageAddResponseDto MapMessageCreationFromReader(SqlDataReader reader)
        {
            return new MessageAddResponseDto
            {
                MessageId = reader.GetValueOrDefault<int>("MessageId"),
                ConversationId = reader.GetValueOrDefault<int>("ConversationId"),
                SenderId = reader.GetValueOrDefault<int>("SenderId"),
                SenderUsername = reader.GetValueOrDefault<string>("SenderUsername") ?? string.Empty,
                SenderFirstName = reader.GetValueOrDefault<string>("SenderFirstName") ?? string.Empty,
                SenderProfilePicture = reader.GetValueOrDefault<string>("SenderProfilePicture"),
                Content = reader.GetValueOrDefault<string>("Content") ?? string.Empty,
                MessageType = reader.GetValueOrDefault<string>("MessageType") ?? string.Empty,
                SentAt = reader.GetValueOrDefault<DateTime>("SentAt"),
                EditedAt = reader.GetValueOrDefault<DateTime?>("EditedAt"),
                IsDeleted = reader.GetValueOrDefault<bool>("IsDeleted"),
                IsRead = reader.GetValueOrDefault<bool>("IsRead"),
                ReadAt = reader.GetValueOrDefault<DateTime?>("ReadAt"),
                MediaFile = reader.GetValueOrDefault<string>("MediaFile")
            };
        }

        public static MessagesReadResponseDto MapReadResponseFromReader(SqlDataReader reader)
        {
            return new MessagesReadResponseDto
            {
                ConversationId = reader.GetValueOrDefault<int>("ConversationId"),
                UserId = reader.GetValueOrDefault<int>("UserId"),
                MessagesMarkedAsRead = reader.GetValueOrDefault<int>("MessagesMarkedAsRead")
            };
        }
    }
}
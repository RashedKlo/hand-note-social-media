using System;
using Microsoft.Data.SqlClient;
using HandNote.Data.Helpers;
using HandNote.Data.DTOs.Friendship.Queries;
using HandNote.Data.DTOs.Friendship.Update;
using HandNote.Data.DTOs.Friendship.Create;

namespace HandNote.Data.Repositories.Friendship.Helpers
{
    public static class FriendshipMapper
    {
        public static FriendshipCreateResponseDto MapCreateResponseFromReader(SqlDataReader reader)
        {
            if (reader == null)
                throw new ArgumentNullException(nameof(reader));

            return new FriendshipCreateResponseDto
            {
                FriendshipId = reader.GetValue<int>("FriendshipId"),
                RequesterId = reader.GetValueOrDefault<int>("RequesterId"),
                AddresseeId = reader.GetValueOrDefault<int>("AddresseeId"),
                FriendshipStatus = reader.GetValue<string>("FriendshipStatus"),
                CreatedAt = reader.GetValueOrDefault<DateTime>("CreatedAt"),
                UpdatedAt = reader.GetValueOrDefault<DateTime>("UpdatedAt"),

            };
        }

        public static FriendshipUpdateResponseDto MapUpdateResponseFromReader(SqlDataReader reader)
        {
            if (reader == null)
                throw new ArgumentNullException(nameof(reader));

            return new FriendshipUpdateResponseDto
            {
                FriendshipId = reader.GetValueOrDefault<int>("FriendshipId"),
                RequesterId = reader.GetValueOrDefault<int>("RequesterId"),
                AddresseeId = reader.GetValueOrDefault<int>("AddresseeId"),
                FriendshipStatus = reader.GetValue<string>("FriendshipStatus"),
                UpdatedAt = reader.GetValueOrDefault<DateTime>("UpdatedAt"),

            };
        }

        public static FriendshipExistenceResponseDto MapExistenceResponseFromReader(SqlDataReader reader)
        {
            if (reader == null)
                throw new ArgumentNullException(nameof(reader));

            return new FriendshipExistenceResponseDto
            {
                FriendshipId = reader.GetValueOrDefault<int>("FriendshipId"),
                UserId1 = reader.GetValueOrDefault<int>("UserId1"),
                UserId2 = reader.GetValueOrDefault<int>("UserId2"),
                FriendshipExists = reader.GetValueOrDefault<bool>("FriendshipExists"),
                FriendshipStatus = reader.GetValue<string>("FriendshipStatus"),

            };
        }

    }
}


using HandNote.Data.Models;

namespace HandNote.Data.Repositories.Friendship.Helpers
{


    public static class FriendshipUtils
    {
        /// <summary>
        /// Converts a FriendshipStatus enum to its string representation
        /// (lowercase, matching DB values).
        /// </summary>
        public static string GetStatus(EnFriendshipStatus status)
        {
            return status.ToString().ToLower();
        }

        /// <summary>
        /// Converts a string from DB to FriendshipStatus enum safely.
        /// </summary>
        public static EnFriendshipStatus ParseStatus(string status)
        {
            if (Enum.TryParse<EnFriendshipStatus>(status, true, out var parsed))
            {
                return parsed;
            }

            throw new ArgumentException($"Invalid friendship status: {status}");
        }
    }

}
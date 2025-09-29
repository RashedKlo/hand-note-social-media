using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HandNote.Data.DTOs.Friendship.Queries
{
   public class SearchUserFriendsResponseDto
    {
        public List<FriendDto> Friends { get; set; } = new List<FriendDto>();
        public int TotalCount { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public string Status { get; set; } = string.Empty;
        public string? ErrorMessage { get; set; }
        public int? ErrorNumber { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HandNote.Data.Models;

namespace HandNote.Data.DTOs.Friendship.Queries
{
 public class GetFriendRequestsResponseDto
    {
        public List<FriendRequestDto> FriendRequests { get; set; } = new List<FriendRequestDto>();
        public int TotalCount { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public string Status { get; set; } = string.Empty;
        public string? ErrorMessage { get; set; }
        public int? ErrorNumber { get; set; }
    }
}
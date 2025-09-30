using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HandNote.Data.DTOs.UserSession.Query
{
    public class ActiveSessionDto
    {
         public int SessionId { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? ExpiresAt { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? Provider { get; set; }
        public int? MinutesUntilExpiry { get; set; }
        public int? SessionAgeHours { get; set; }
        public int TotalActiveSessions { get; set; }
    }
}
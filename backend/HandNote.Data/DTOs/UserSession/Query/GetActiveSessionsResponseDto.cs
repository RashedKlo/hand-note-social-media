using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HandNote.Data.DTOs.UserSession.Query
{
  public class GetActiveSessionsResponseDto
    {
        public List<ActiveSessionDto> Sessions { get; set; } = new List<ActiveSessionDto>();
    
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HandNote.Data.DTOs.Friendship.Create
{
 // Friendship Create Request DTO
public class FriendshipCreateRequestDto
{
    [Required(ErrorMessage = "RequesterId is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "RequesterId must be a positive integer.")]
    public int RequesterId { get; set; }

    [Required(ErrorMessage = "AddresseeId is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "AddresseeId must be a positive integer.")]
    public int AddresseeId { get; set; }
}
}
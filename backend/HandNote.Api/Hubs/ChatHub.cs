// ============================
// CHATHUB WITH FRIENDS MANAGEMENT
// ============================
using HandNote.Data.DTOs.Message.Add;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;

namespace HandNote.API.Hubs
{
    public class ChatHub : Hub
    {
        private readonly ILogger<ChatHub> _logger;

        // Track online users (userId -> when they came online)
        private static readonly ConcurrentDictionary<int, DateTime> _onlineUsers = new();

        public ChatHub(ILogger<ChatHub> logger) => _logger = logger;

        public override async Task OnConnectedAsync()
        {
            if (!int.TryParse(Context.GetHttpContext()?.Request.Query["userId"], out var userId))
                return;

            // ESSENTIAL: Add to user's personal group for direct messaging
            // This allows anyone to send messages to "User_{userId}" group
            await Groups.AddToGroupAsync(Context.ConnectionId, $"User_{userId}");

            // Track user as online
            _onlineUsers[userId] = DateTime.UtcNow;

            _logger.LogInformation("User {UserId} connected", userId);

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            if (!int.TryParse(Context.GetHttpContext()?.Request.Query["userId"], out var userId))
                return;

            // Remove from online users
            _onlineUsers.TryRemove(userId, out _);

            _logger.LogInformation("User {UserId} disconnected", userId);

            await base.OnDisconnectedAsync(exception);
        }

        // Send message from one user to another (works regardless of friendship)
        public async Task SendMessage(MessageAddResponseDto message, int recipientId)
        {
            try
            {
                // Send directly to recipient's personal group
                await Clients.Group($"User_{recipientId}")
                    .SendAsync("ReceiveMessage", message);

                _logger.LogDebug("Message sent to {RecipientId}", recipientId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending message to {RecipientId}", recipientId);
                await Clients.Caller.SendAsync("MessageError", "Failed to send message");
            }
        }
    }

}
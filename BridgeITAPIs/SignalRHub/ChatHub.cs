using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;

namespace BridgeITAPIs.SignalRHub;

public class ChatHub: Hub
{
    private static readonly ConcurrentDictionary<string, string> _userConnections = new();

    public override async Task OnConnectedAsync()
    {
        var httpContext = Context.GetHttpContext();
        string userId = httpContext?.Request.Query["userId"].ToString() ?? Context.ConnectionId;
        _userConnections[Context.ConnectionId] = userId;

        Console.WriteLine($"Client connected: {Context.ConnectionId}, userId: {userId}");
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        _userConnections.TryRemove(Context.ConnectionId, out var userName);
        Console.WriteLine($"Client disconnected: {Context.ConnectionId}, Username: {userName}");
        await base.OnDisconnectedAsync(exception);
    }

    public async Task SendMessageToUser(string recipientUserId, string senderUserId, string message, DateTime timeStamp)
    {
        string messageId = $"{senderUserId}-{recipientUserId}-{DateTime.UtcNow.Ticks}";
      //  await _userRepository.InsertMessage(messageId, senderUserId, recipientUserId, encryptedMessage);

        var recipientConnectionId = _userConnections.FirstOrDefault(x => x.Value == recipientUserId).Key;
        if (recipientConnectionId != null)
        {
            Console.WriteLine($"Sending message from {senderUserId} to {recipientUserId} with connection ID {recipientConnectionId}");
            await Clients.Client(recipientConnectionId).SendAsync("ReceiveMessage", senderUserId, message, timeStamp);
        }
        else
        {
            Console.WriteLine($"No connection found for recipient: {recipientUserId}");
        }
    }

   
}
using Microsoft.AspNetCore.SignalR;

namespace BridgeITAPIs.SignalRHub;

public class ChatHub: Hub
{
    public async Task SendMessage(Guid userId, string message)
    {
        string userIdString = userId.ToString();

        await Clients.User(userIdString).SendAsync("ReceiveMessage", message);
    }

    public async Task SendMessageToGroup(string groupName, string message)
    {
        await Clients.Groups(groupName).SendAsync("ReceiveMessage", message);
    }
    
    public async Task JoinGroup(string groupName)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
    }
    
    public async Task LeaveGroup(string groupName)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
    }
}
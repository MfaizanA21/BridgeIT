using BridgeIT.API.SignalRHub;
using Microsoft.AspNetCore.SignalR;

namespace BridgeIT.API.Helper;

public class ChatService
{
    private readonly IHubContext<ChatHub> _chatHubContext;
    
    public ChatService(IHubContext<ChatHub> chatHubContext)
    {
        _chatHubContext = chatHubContext;
    }

    public async Task SendMessageToExpert(Guid studentId, Guid expertId, string message)
    {
        string stdId = studentId.ToString();
        string expId = expertId.ToString();
        
        await _chatHubContext.Clients.User(expId).SendAsync("ReceiveMessage", message);
        await _chatHubContext.Clients.User(stdId).SendAsync("ReceiveMessage", message);
    }
    
    public async Task SendMessageToGroup(string groupName, string message)
    {
        await _chatHubContext.Clients.Group(groupName).SendAsync("ReceiveMessage", message);
    }
}
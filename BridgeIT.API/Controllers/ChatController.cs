using BridgeIT.API.DTOs.ChatDTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BridgeIT.Domain.Models;
using BridgeIT.Infrastructure;

namespace BridgeIT.API.Controllers;

[Route("api/chats")]
[ApiController]
public class ChatController : Controller
{
    private readonly ChatService _chatService;
    private readonly BridgeItContext _dbContext;
    
    public ChatController(ChatService chatService, BridgeItContext dbContext)
    {
        _chatService = chatService;
        _dbContext = dbContext;
    }

    [HttpPost("send-message")]
    public async Task<IActionResult> SendMessage([FromBody] SendMessageDTO messageDto)
    {
        if (string.IsNullOrWhiteSpace(messageDto.Message))
        {
            return BadRequest(new { Message = "Invalid message data." });
        }

        var message = new Message
        {
            Id = Guid.NewGuid(),
            SenderId = messageDto.StudentId,
            RecipientId = messageDto.ExpertId,
            Content = messageDto.Message,
            TimeSent = DateTime.UtcNow
        };
        
        await _dbContext.Messages.AddAsync(message);
        await _dbContext.SaveChangesAsync();
        
        await _chatService.SendMessageToExpert(messageDto.StudentId, messageDto.ExpertId, messageDto.Message);

        return Ok(new { Message = "Message Sent successfully!" });
    }

    [HttpGet("message-history/{stdUId}/{expUId}")]
    public async Task<IActionResult> GetMessageHistory(Guid stdUId, Guid expUId)
    {
        var messages = await _dbContext.Messages
            .Where(m => (m.SenderId == stdUId && m.RecipientId == expUId) || (m.SenderId == expUId && m.RecipientId == stdUId))
            .OrderBy(m => m.TimeSent)
            .ToListAsync();

        return Ok(messages);
    }
}
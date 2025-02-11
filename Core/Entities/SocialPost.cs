using System.Reflection;
using Core.Abstractions;
using Core.Events;
namespace Core.Entities;

public sealed class SocialPost(int id, int boutiqueId, string username, string comment, DateTime timestamp) : Entity
{
    public int Id { get; private set; } = id;   
    public int BoutiqueId { get; private set; } = boutiqueId;
    public string Username { get; private set; } = username;
    public string Comment { get; private set; } = comment;
    public DateTime Timestamp { get; private set; } = timestamp;
}
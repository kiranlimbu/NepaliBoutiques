using Application.Abstractions;

namespace Application.Features.SocialPosts.Commands;

public record DeleteSocialPostCommand(int PostId) : ICommand;



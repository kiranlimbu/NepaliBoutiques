using Application.Abstractions;

namespace Application.Features.Boutiques.Commands;

public record DeleteBoutiqueCommand(int Id) : ICommand;

using Mediator;

namespace SnappFood.DotNetSampleProject.Core.ApplicationServices.Commands;

public sealed class RegisterBikerCommand : IRequest<long>
{
    public string FirstName { get; init; } = null!;
    public string LastName { get; init; } = null!;
}

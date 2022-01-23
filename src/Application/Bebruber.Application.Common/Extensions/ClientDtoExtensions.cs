using Bebruber.Common.Dto;
using Bebruber.Domain.Entities;

namespace Bebruber.Application.Common.Extensions;

public static class ClientDtoExtensions
{
    public static ClientDto ToDto(this Client client)
        => new ClientDto(client.Name.ToString(), client.Rating.Value);
}
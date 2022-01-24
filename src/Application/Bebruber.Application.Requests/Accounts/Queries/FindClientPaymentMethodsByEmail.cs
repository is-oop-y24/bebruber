using MediatR;

namespace Bebruber.Application.Requests.Accounts.Queries;

public class FindClientPaymentMethodsByEmail
{
    public record Command(string Email) : IRequest<Response>;

    public record Response(List<string> PaymentMethods);
}
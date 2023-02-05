using AuthJwtApi.Application.Domain;

namespace AuthJwtApi.Application.Ports.In;

public interface ITokenServicePortIn
{
    string GenerateToken(Customer user);
}
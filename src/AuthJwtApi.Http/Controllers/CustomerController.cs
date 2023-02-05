using AuthJwtApi.Application.Domain;
using AuthJwtApi.Application.Ports.In;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthJwtApi.Http.Controllers;

[ApiController]
[Route("/v1/customers")]
public class CustomerController : ControllerBase
{
    private static List<Customer> customers = new List<Customer>();
    private readonly ITokenServicePortIn _tokenService;
    private readonly ICustomerServicePortIn _customerServicePortIn;

    public CustomerController(ITokenServicePortIn tokenService, ICustomerServicePortIn customerServicePortIn)
    {
        _tokenService = tokenService;
        _customerServicePortIn = customerServicePortIn;
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<Customer> GetCustomer(string id)
    {
        return await FindCustomer(id);
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<dynamic> PostCustomer(Customer model)
    {
        await _customerServicePortIn.SaveCustomerAsync(model);
        return model;
    }

    [HttpPost("/auth/token")]
    [AllowAnonymous]
    public async Task<dynamic> AuthenticatePost(string id)
    {
        Customer? user = await FindCustomer(id);

        if (user is null)
            return "Error, customer not found.";

        var token = _tokenService.GenerateToken(user);

        return new
        {
            token
        };
    }

    private async Task<Customer> FindCustomer(string id)
    {
        return await _customerServicePortIn.GetCustomerAsync(id);
    }
}
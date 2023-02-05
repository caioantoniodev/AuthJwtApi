namespace AuthJwtApi.Application.Domain;

public class Customer
{
    public string Id { get; private set; }  
    public string Username { get; private set; }  
    public string Password { get; private set; }

    protected Customer()
    {
    }

    public Customer(string username, string password)
    {
        Id = Guid.NewGuid().ToString();
        Username = username;
        Password = password;
    }
}
namespace Infinion.Domain.Results;
public class LoginResult
{
    public bool Succeeded { get; set; }
    public string? ErrorMessage { get; set; }
    public string? Token { get; set; }
}

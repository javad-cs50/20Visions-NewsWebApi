namespace NewsWebSiteApi.Application.Helper;

public static class PasswordService
{
    public static string HashPassword(string plainPassword)
    {
        var passWordHash = BCrypt.Net.BCrypt.HashPassword(plainPassword);
        return passWordHash;
    }
    public static bool VerifyPassword (string plainPassword , string hashPassword) 
    {
        return BCrypt.Net.BCrypt.Verify(plainPassword,hashPassword);
    }


}

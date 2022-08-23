using BCrypt.Net;

namespace ToDoList.security;

public class PasswordHandler
{
    public string HashPassword(string pw)
    {
        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(pw, SaltRevision.Revision2A);
        return hashedPassword;
    }

    public bool PasswordMatch(string pw, string hashedPW)
    {
        bool validPassword = BCrypt.Net.BCrypt.Verify(pw, hashedPW);
        return validPassword;
    }
}
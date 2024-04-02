namespace TwoDrive.AuthUtils;

using System;

public class ValidateToken
{
    public static bool Validate(string tokenString)
    {
        // Validate token
        using var db = new TwoDriveContext();

        // Find token by value
        var tokensFound = db.Token.Where(token => token.Value == tokenString);

        if (tokensFound.Count() == 0)
        {
            return false;
        }

        var tokenEntity = tokensFound.First();

        if (tokenEntity == null)
        {
            return false;
        }

        if (tokenEntity.date_created!.Value.AddDays(1) < DateTime.Now)
        {
            return false;
        }

        return true;
    }
}
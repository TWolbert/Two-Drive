namespace TwoDrive.AuthUtils;

using System;

public class TokenToUserId
{
    public static long Id(string tokenString)
    {
        // Validate token
        using var db = new TwoDriveContext();

        var tokenEntity = db.Token.Where(token => token.Value == tokenString).First();

        if (tokenEntity == null)
        {
            return -1;
        }

        return tokenEntity.UserId;
    }
}
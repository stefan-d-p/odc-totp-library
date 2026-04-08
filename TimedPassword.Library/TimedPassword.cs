using System.Security.Cryptography;
using System.Text;

namespace Without.Systems.TimedPassword;

public class TimedPassword : ITimedPassword
{
    /// <summary>
    /// Verifies a one-time password with a given secret
    /// </summary>
    /// <param name="code">one-time password</param>
    /// <param name="secret">Shared secret as plain text</param>
    /// <param name="digits">Number of digits: 4 or 6</param>
    /// <param name="period">Time step in seconds. Default 30.</param>
    /// <param name="allowedDrift">Clock skew drift</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public bool Verify(
        string code,
        string secret,
        int digits = 6,
        int period = 30,
        int allowedDrift = 1)
    {
        if(digits != 4 && digits != 6)
            throw new ArgumentException("Digits must be 4 digits or 6 digits");
        
        if(string.IsNullOrEmpty(code))
            return false;
        
        long unixTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        long currentCounter = unixTime / period;
        
        for(long counter = currentCounter - allowedDrift;
            counter <= currentCounter + allowedDrift;
            counter++)
        {
            string expectedTotp = GenerateTotpForCounter(secret, counter, digits);
            if (expectedTotp == code)
                return true;
        }
        
        return false;
    }

    /// <summary>
    /// Generate a TOTP using a plain-text shared secret
    /// </summary>
    /// <param name="secret">Shared secret as plain text</param>
    /// <param name="digits">Number of digits: 4 or 6</param>
    /// <param name="period">Time step in seconds. Default 30</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public string Generate(
        string secret,
        int digits = 6,
        int period = 30)
    {
        if(digits != 4 && digits != 6)
            throw new ArgumentException("Digits must be 4 digits or 6 digits");
        
        long unixTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        long counter = unixTime / period;
        
        return GenerateTotpForCounter(secret, counter, digits);
    }

    private string GenerateTotpForCounter(
        string secret,
        long counter,
        int digits)
    {
        byte[] key = Encoding.UTF8.GetBytes(secret);

        byte[] counterBytes = new byte[8];
        for (int i = 7; i >= 0; i--)
        {
            counterBytes[i] = (byte)(counter & 0xFF);
            counter >>= 8;
        }
        
        using var hmac = new HMACSHA1(key);
        byte[] hash = hmac.ComputeHash(counterBytes);

        int offset = hash[^1] & 0x0F;
        int binaryCode =
            ((hash[offset] & 0x7F) << 24) |
            ((hash[offset + 1] & 0xFF) << 16) |
            ((hash[offset + 2] & 0xFF) << 8) |
            (hash[offset + 3] & 0xFF);
        
        int otp = binaryCode % (int)Math.Pow(10, digits);
        return otp.ToString().PadLeft(digits, '0');
    }
}
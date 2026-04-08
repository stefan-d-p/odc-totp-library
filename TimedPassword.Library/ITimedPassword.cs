using OutSystems.ExternalLibraries.SDK;

namespace Without.Systems.TimedPassword
{
    [OSInterface(
        Name = "TimedPassword",
        Description = "Time-based one-time password generator and verifier",
        IconResourceName = "Without.Systems.TimedPassword.Resources.TOTPExt.png")]
    public interface ITimedPassword
    {
        [OSAction(
            Description = "Verify a TOTP using a plain-text shared secret",
            ReturnName = "IsValid",
            ReturnType = OSDataType.Boolean,
            IconResourceName = "Without.Systems.TimedPassword.Resources.TOTPExt.png")]
        bool Verify(
            [OSParameter(
                Description = "One-time password to verify",
                DataType = OSDataType.Text)]
            string code,
            [OSParameter(
                Description = "Shared secret",
                DataType = OSDataType.Text)]
            string secret,
            [OSParameter(
                Description = "Number of digits: 4 or 6 digits.",
                DataType = OSDataType.Integer)]
            int digits = 6,
            [OSParameter(
                Description = "Time step in seconds. Default 30.",
                DataType = OSDataType.Integer)]
            int period = 30,
            [OSParameter(
                Description = "Clock skew drift",
                DataType = OSDataType.Integer)]
            int allowedDrift = 1);

        [OSAction(
            Description = "Generate a TOTP using a plain text shared secret",
            ReturnName = "Code",
            ReturnType = OSDataType.Text,
            IconResourceName = "Without.Systems.TimedPassword.Resources.TOTPExt.png")]
        string Generate(
            [OSParameter(
                Description = "Shared secret",
                DataType = OSDataType.Text)]
            string secret,
            [OSParameter(
                Description = "Number of digits: 4 or 6 digits.",
                DataType = OSDataType.Integer)]
            int digits = 6,
            [OSParameter(
                Description = "Time step in seconds. Default 30.",
                DataType = OSDataType.Integer)]
            int period = 30);
    }
}
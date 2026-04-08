# ODC TOTP Library

This is an OutSystems Developer Cloud (ODC) external logic library that provides functionality for generating and verifying Time-based One-Time Passwords (TOTP) as per RFC 6238.

## Overview

The library implements TOTP using HMAC-SHA1 for hashing and supports 4 or 6 digit codes with configurable time steps. It allows for clock skew tolerance during verification.

## Features

- Generate TOTP codes from a shared secret
- Verify TOTP codes with configurable drift allowance
- Supports 4 or 6 digit codes
- Configurable time step (default 30 seconds)
- Compatible with OutSystems ODC external libraries

## Usage

### In OutSystems ODC

1. Import the library into your ODC environment or install it from Forge.
2. Use the `TimedPassword` actions in your logic flows.

### API Reference

#### Generate

Generates a TOTP code for the current time.

**Parameters:**
- `secret` (Text): The shared secret as plain text
- `digits` (Integer, optional): Number of digits in the code (4 or 6, default 6)
- `period` (Integer, optional): Time step in seconds (default 30)

**Returns:** The TOTP code as a string

#### Verify

Verifies a provided TOTP code against the shared secret.

**Parameters:**
- `code` (Text): The one-time password to verify
- `secret` (Text): The shared secret as plain text
- `digits` (Integer, optional): Number of digits in the code (4 or 6, default 6)
- `period` (Integer, optional): Time step in seconds (default 30)
- `allowedDrift` (Integer, optional): Number of time steps to allow for clock skew (default 1)

**Returns:** Boolean indicating if the code is valid

## Example

To generate a TOTP code:

```
string secret = "JBSWY3DPEHPK3PXP";
string code = TimedPassword.Generate(secret, 6, 30);
```

To verify a TOTP code:

```
bool isValid = TimedPassword.Verify("123456", secret, 6, 30, 1);
```

## Building and Testing

The library is built using .NET 8. To build:

```
dotnet build
```

To run tests:

```
dotnet test
```

## License

[Specify license if applicable]

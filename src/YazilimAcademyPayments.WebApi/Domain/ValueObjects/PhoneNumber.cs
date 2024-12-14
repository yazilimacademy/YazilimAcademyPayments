using System.Text.RegularExpressions;

namespace YazilimAcademyPayments.WebApi.Domain.ValueObjects;

public sealed record PhoneNumber
{
    // E.164 validation pattern:
    // Allows optional '+' sign, then country code not starting with 0,
    // followed by up to 14 digits. Total length up to 15 digits max.
    // Example: +491234567890 (German), +905531234567 (Turkey)
    private const string E164Pattern = @"^\+?[1-9]\d{1,14}$";

    public string Value { get; init; }

    public PhoneNumber(string value)
    {
        if (!IsValid(value))
            throw new ArgumentException("Invalid phone number format.");

        Value = value;
    }

    private static bool IsValid(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return false;

        return Regex.IsMatch(value, E164Pattern);
    }

    public static implicit operator string(PhoneNumber phoneNumber) => phoneNumber.Value;

    public static implicit operator PhoneNumber(string value) => new(value);
}

namespace CuddlerDev.Utils;

public static class PhoneUtil
{
    public static string? ToPhone(string? phoneNumber)
    {
        if (!string.IsNullOrEmpty(phoneNumber) && phoneNumber.Length == 10)
        {
            var int64 = Convert.ToInt64(phoneNumber);

            return int64 == 0
                ? "(000) 000-0000"
                : $"{int64:(###) ###-####}";
        }

        return phoneNumber;
    }
}
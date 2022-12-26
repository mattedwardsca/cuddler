namespace Cuddler.Core.Data;

public enum EDataType
{
    /**
         * True or false, on or off.
         */
    Boolean,

    /**
         * A day on the calendar.
         */
    Date,
    Time,
    Month,
    Datetime,

    /**
         * A fractional numberOverride like 2.5 or 98.0.
         */
    Currency,

    /**
         * A whole numberOverride like 12 or 75.
         */
    Integer,

    /**
         * Free-form text.
         */
    Text,
    Password,
    Search,
    Url,
    Email,
    Telephone,

    /**
         * An instant in time as milliseconds since January 1, 1970 00:00:00.000 GMT.
         */
    Timestamp,
    Decimal
}
using System.Globalization;

namespace WebApplication2
{
    public static class Conventions
    {
        public static int ToInt(this object? value)
        {
            var retVal = 0;

            if (value == null || value == DBNull.Value) return retVal;
            if (value is bool)
                if (Convert.ToBoolean(value, CultureInfo.InvariantCulture))
                    return 1;
            var numberToParse = value.ToString();

            if (int.TryParse(numberToParse, out retVal))
                return retVal;
            return retVal;
        }
    }
}

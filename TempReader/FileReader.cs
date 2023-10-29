using System.IO;

namespace TempReader
{
    public class FileReader : IReader
    {
        /// <summary>
        /// Temperature thermal zone file
        /// </summary>
        public const string THERMAL_ZONE_FILE = "/sys/class/thermal/thermal_zone0/temp";

        /// <summary>
        /// Thermal zone data multiplier
        /// </summary>
        public const float THERMAL_ZONE_MULT = 1000.0f;

        public float Read(TempScale scale)
        {
            return scale switch
            {
                TempScale.Celsius => ReadCelsius(),
                TempScale.Farenheit => ReadFarenheit(),
                TempScale.Kelvin => ReadKelvin(),
                _ => float.NaN
            };
        }

        /// <summary>
        /// Reads Celsius temperature
        /// </summary>
        private float ReadCelsius()
        {
            var tempValue = GetFloatTemperature();
            if (tempValue is float.NaN) return tempValue;
            return (tempValue / THERMAL_ZONE_MULT);
        }

        /// <summary>
        /// Reads Farenheit temperature
        /// </summary>
        private float ReadFarenheit()
        {
            // Get the temp in Celsius
            var temp = ReadCelsius();
            if (temp is float.NaN) return temp;

            // Perform conversion
            return (temp * (9.0f / 5.0f) + 32.0f);
        }

        /// <summary>
        /// Reads Kelvin temperature
        /// </summary>
        private float ReadKelvin()
        {
            // Get celsius temperature
            var temp = ReadCelsius();
            if (temp is float.NaN) return temp;

            // Perform conversion
            return (-(IReader.ABSOLUTE_ZERO_TEMP) + temp);
        }

        /// <summary>
        /// Returns the temperature as a floating point value.
        /// By default, this value is Unparsed Celsius, which means
        /// that there is no floating point in the number represented.
        /// It must be added lated by dividing by 1000
        /// </summary>
        private float GetFloatTemperature()
        {
            var temperatureString = GetPlainTextTemperature();

            if (!float.TryParse(temperatureString, out float result))
            {
                return float.NaN;
            }

            return result;
        }

        /// <summary>
        /// Returns plain text temperature to caller
        /// </summary>
        private string GetPlainTextTemperature()
        {
            try
            {
                if (!File.Exists(THERMAL_ZONE_FILE))
                    throw new FileNotFoundException($"Thermal zone {THERMAL_ZONE_FILE} is not present!");

                var temp = File.ReadAllText(THERMAL_ZONE_FILE);

                return temp;
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Error.WriteLine(e.Message);
                Console.ResetColor();
                return "NaN";
            }
        }
    }
}

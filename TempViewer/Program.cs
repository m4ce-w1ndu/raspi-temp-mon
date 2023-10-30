using TempReader;
using CommandLine;

namespace TempViewer
{
    internal class Options
    {
        [Option('c', "celsius", Required = false, HelpText = "Show output in Celsius")]
        public bool Celsius { get; private set; }

        [Option('f', "farenheit", Required = false, HelpText = "Show output in Farenheit")]
        public bool Farenheit { get; private set; }

        [Option('k', "kelvin", Required = false, HelpText = "Show output in Kelvin")]
        public bool Kelvin { get; private set; }

        public Options() { }

        public TempScale GetOptionScale()
        {
            return Celsius ? TempScale.Celsius :
                   Farenheit ? TempScale.Farenheit :
                   Kelvin ? TempScale.Kelvin : TempScale.Celsius;
        }
    }

    internal class Program
    {
        /// <summary>
        /// Low temperature threshold in Celsius
        /// </summary>
        const float LOW_TEMP_THRESHOLD_C = 0.0f;

        /// <summary>
        /// Low temperature threshold in Farenheit
        /// </summary>
        const float LOW_TEMP_THRESHOLD_F = (LOW_TEMP_THRESHOLD_C * 9.0f / 5.0f) + 32.0f;

        /// <summary>
        /// Low temperature threshold in Kelvin
        /// </summary>
        const float LOW_TEMP_THRESHOLD_K = -(ITempReader.ABSOLUTE_ZERO_TEMP) + LOW_TEMP_THRESHOLD_C;

        /// <summary>
        /// Mid temperature threshold in Celsius
        /// </summary>
        const float MID_TEMP_THRESHOLD_C = 45.0f;

        /// <summary>
        /// Mid temperature threshold in Farenheit
        /// </summary>
        const float MID_TEMP_THRESHOLD_F = (MID_TEMP_THRESHOLD_C * 9.0f / 5.0f) + 32.0f;

        /// <summary>
        /// Mid temperature threshold in Kelvin
        /// </summary>
        const float MID_TEMP_THRESHOLD_K = -(ITempReader.ABSOLUTE_ZERO_TEMP + MID_TEMP_THRESHOLD_C);

        /// <summary>
        /// High temperature threshold in Celsius
        /// </summary>
        const float HIGH_TEMP_THRESHOLD_C = 65.0f;

        /// <summary>
        /// High temperature threshold in Farenheit
        /// </summary>
        const float HIGH_TEMP_THRESHOLD_F = (HIGH_TEMP_THRESHOLD_C * 9.0f / 5.0f) + 32.0f;

        /// <summary>
        /// High temperature threshold in Kelvin
        /// </summary>
        const float HIGH_TEMP_THRESHOLD_K = (-ITempReader.ABSOLUTE_ZERO_TEMP + HIGH_TEMP_THRESHOLD_C);

        static void Main(string[] args)
        {
            var reader = new FileTempReader();

            Parser.Default.ParseArguments<Options>(args)
                  .WithParsed(options =>
                  {
                      var scale = options.GetOptionScale();
                      PrintTemp(reader.Read(scale), scale);
                  })
                  .WithNotParsed(errors =>
                  {
                      Console.Error.WriteLine("invalid argument");
                  });
        }

        /// <summary>
        /// Prints the temperature with the correct scale and color
        /// based on current temperature reading
        /// </summary>
        static void PrintTemp(float temp, TempScale scale)
        {
            Console.Write("Current CPU temperature: ");

            var tempStr = TempString(temp, scale);

            if (IsLowTemp(temp, scale))
                Console.ForegroundColor = ConsoleColor.Green;
            if (IsMidTemp(temp, scale))
                Console.ForegroundColor = ConsoleColor.Yellow;
            if (IsHighTemp(temp, scale))
                Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine(tempStr);
            Console.ResetColor();
        }

        /// <summary>
        /// Returns a formatted temperature string
        /// </summary>
        static string TempString(float temp, TempScale scale)
        {
            return scale switch
            {
                TempScale.Celsius => $"{temp} °C",
                TempScale.Farenheit => $"{temp} °F",
                TempScale.Kelvin => $"{temp} °K",
                _ => temp.ToString()
            };
        }

        /// <summary>
        /// Checks low temperature thresholds
        /// </summary>
        static bool IsLowTemp(float temp, TempScale scale)
        {
            return ((temp >= LOW_TEMP_THRESHOLD_C && temp < MID_TEMP_THRESHOLD_C) && scale == TempScale.Celsius) ||
                   ((temp >= LOW_TEMP_THRESHOLD_F && temp < MID_TEMP_THRESHOLD_F) && scale == TempScale.Farenheit) ||
                   ((temp >= LOW_TEMP_THRESHOLD_K && temp < MID_TEMP_THRESHOLD_K) && scale == TempScale.Kelvin);
        }


        /// <summary>
        /// Checks medium temperature thresholds
        /// </summary>
        static bool IsMidTemp(float temp, TempScale scale)
        {
            return ((temp >= MID_TEMP_THRESHOLD_C && temp < HIGH_TEMP_THRESHOLD_C) && scale == TempScale.Celsius) ||
                   ((temp >= MID_TEMP_THRESHOLD_F && temp < HIGH_TEMP_THRESHOLD_F) && scale == TempScale.Farenheit) ||
                   ((temp >= MID_TEMP_THRESHOLD_K && temp < HIGH_TEMP_THRESHOLD_K) && scale == TempScale.Kelvin);
        }

        /// <summary>
        /// Checks high temperature thresholds
        /// </summary>
        static bool IsHighTemp(float temp, TempScale scale)
        {
            return (temp >= HIGH_TEMP_THRESHOLD_C && scale == TempScale.Celsius) ||
                   (temp >= HIGH_TEMP_THRESHOLD_F && scale == TempScale.Farenheit) ||
                   (temp >= HIGH_TEMP_THRESHOLD_K && scale == TempScale.Kelvin);
        }
    }
}
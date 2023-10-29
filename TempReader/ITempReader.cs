namespace TempReader
{
    /// <summary>
    /// Available temperature scales
    /// </summary>
    public enum TempScale
    {
        Celsius,
        Farenheit,
        Kelvin
    }

    /// <summary>
    /// Generic temperature reader interface
    /// </summary>
    public interface ITempReader
    {
        /// <summary>
        /// Absolute zero temperature for Kelvin conversion
        /// </summary>
        public const float ABSOLUTE_ZERO_TEMP = -273.15f;

        /// <summary>
        /// Returns a temperature in the given input scale
        /// </summary>
        /// <param name="scale">Desired temperature scale</param>
        /// <returns>Floating point value representing temperature in the chosen scale</returns>
        float Read(TempScale scale);
    }
}
namespace TempReader
{
    public interface IReader
    {
        /// <summary>
        /// Reads the temperature in Celsius
        /// </summary>
        /// <returns>Floating point Celsius value</returns>
        float ReadCelsius();

        /// <summary>
        /// Reads the temperature in Farenheit
        /// </summary>
        /// <returns>Floating point Farenheit value</returns>
        float ReadFarenheit();

        /// <summary>
        /// Reads the temperature in Kelvin
        /// </summary>
        /// <returns>Floating point Kelvin value</returns>
        float ReadKelvin();
    }
}
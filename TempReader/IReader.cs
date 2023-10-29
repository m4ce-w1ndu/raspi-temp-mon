﻿namespace TempReader
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
    public interface IReader
    {
        /// <summary>
        /// Returns a temperature in the given input scale
        /// </summary>
        /// <param name="scale">Desired temperature scale</param>
        /// <returns>Floating point value representing temperature in the chosen scale</returns>
        float Read(TempScale scale);
    }
}
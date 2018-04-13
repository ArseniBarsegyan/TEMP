using System.Collections.Generic;

namespace BLL.Services
{
    /// <summary>
    /// Test Client service that provide data
    /// </summary>
    public interface IClientService
    {
        /// <summary>
        /// Returns list of string values
        /// </summary>
        /// <returns></returns>
        IList<string> TestData { get; }
        /// <summary>
        /// Returns value by number
        /// </summary>
        /// <param name="id">number of value</param>
        /// <returns></returns>
        string GetValue(int id);

        /// <summary>
        /// Add new value to list of strings
        /// </summary>
        /// <param name="value">value to be added</param>
        void AddValue(string value);

        /// <summary>
        /// Delete value from list of strings
        /// </summary>
        /// <param name="value">value to be deleted</param>
        void RemoveValue(string value);
    }
}
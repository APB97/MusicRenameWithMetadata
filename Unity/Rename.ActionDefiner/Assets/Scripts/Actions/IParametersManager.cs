using System.Collections.Generic;

namespace Actions
{
    public interface IParametersManager
    {
        /// <summary>
        /// Add parameter template instance for UI, optionally setting its entered value.
        /// </summary>
        /// <param name="parameter">Text to set for parameter template instance, or null of not necessary.</param>
        void AddParam(string parameter = null);

        /// <summary>
        /// Clears parameters UI layout.
        /// </summary>
        void ClearParams();

        /// <summary>
        /// Removes parameters from visual layout and returns list of their values.
        /// </summary>
        /// <returns>List of parameters from removed visual representation.</returns>
        List<string> PopAllParameters();
    }
}
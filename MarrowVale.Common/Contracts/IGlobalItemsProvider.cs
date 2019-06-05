using System;
using System.Collections.Generic;
using System.Text;

namespace MarrowVale.Common.Contracts
{
    public interface IGlobalItemsProvider
    {
        /// <summary>
        /// This will be used to house global properties
        /// </summary>
        int WindowWidth { get; }

        /// <summary>
        /// Converts first character of the string to upper case, and returns the string
        /// </summary>
        /// <param name="text">string that needs the first letter converted to upper case</param>
        /// <returns></returns>
        string UpperFirstChar(string text);
    }
}

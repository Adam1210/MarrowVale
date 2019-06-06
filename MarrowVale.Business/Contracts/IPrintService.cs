using System;
using System.Collections.Generic;
using System.Text;

namespace MarrowVale.Business.Contracts
{
    public interface IPrintService
    {
        /// <summary>
        /// Prints to the console window
        /// </summary>
        /// <param name="line">String to print to console</param>
        void Print(string line);

        /// <summary>
        /// Prints in the center of current line in the console window
        /// </summary>
        /// <param name="line">String to print to console</param>
        void PrintCentered(string line);

        /// <summary>
        /// Prints in the center of current line in the console window
        /// </summary>
        /// <param name="line">String to print to console</param>
        /// <param name="seconds">Number of seconds to wait until starting to print</param>
        void PrintCentered(string line, int seconds);

        /// <summary>
        /// Prints to the console in the same fashion as a typewriter
        /// </summary>
        /// <param name="line">String to print to console</param>
        void Type(string line);

        /// <summary>
        /// Prints to the console in the same fashion as a typewriter
        /// </summary>
        /// <param name="line">String to print to console</param>
        /// <param name="seconds">Number of seconds to wait until starting to print</param>
        void Type(string line, int seconds);

        /// <summary>
        /// Prints to the console in the same fashion as a typewriter and centered
        /// </summary>
        /// <param name="line">String to print to console</param>
        void TypeCentered(string line);

        /// <summary>
        /// Prints to the console in the same fashion as a typewriter and centered
        /// </summary>
        /// <param name="line">String to print to console</param>
        /// <param name="seconds">Number of seconds to wait until starting to print</param>
        void TypeCentered(string line, int seconds);

        /// <summary>
        /// Returns the users input from the console
        /// </summary>
        /// <returns>A string of the users inputer</returns>
        string ReadInput();
    }
}

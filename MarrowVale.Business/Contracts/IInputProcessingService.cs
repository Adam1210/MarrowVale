using System;
using System.Collections.Generic;
using System.Text;

namespace MarrowVale.Business.Contracts
{
    public interface IInputProcessingService
    {
        /// <summary>
        /// Processes user input against the list of given commands
        /// </summary>
        /// <param name="input">User's Input during the game.</param>
        /// <returns>A string response based on the outcome of the processing</returns>
        string ProcessInput(string input);
    }
}

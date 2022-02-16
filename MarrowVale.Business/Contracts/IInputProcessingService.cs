using MarrowVale.Business.Entities.Commands;
using MarrowVale.Business.Entities.Entities;
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
        /// <param name="context">Context for what the player may mention in the input</param>
        /// <param name="contextObject">Object that maps context to Graph Database.</param>
        /// <returns>A string response based on the outcome of the processing.  Needs to be implemented</returns>
        Command ProcessInput(string input, string context, Player player);
    }
}

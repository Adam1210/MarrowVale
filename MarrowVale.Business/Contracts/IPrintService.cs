using MarrowVale.Business.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarrowVale.Business.Contracts
{
    public interface IPrintService
    {
        /// <summary>
        /// Adds automatic pagebreaks to text and then prints to the console window
        /// </summary>
        /// <param name="line">String to print to console</param>
        void PrintParagraph(string text);
        /// <summary>
        /// Prints to the console window
        /// </summary>
        /// <param name="line">String to print to console</param>
        void Print(string line, int milliseconds = 0);

        /// <summary>
        /// Prints in the center of current line in the console window
        /// </summary>
        /// <param name="line">String to print to console</param>
        void PrintCentered(string line);

        /// <summary>
        /// Prints in the center of current line in the console window
        /// </summary>
        /// <param name="line">String to print to console</param>
        /// <param name="milliseconds">Number of milliseconds to wait until starting to print.</param>
        void PrintCentered(string line, int milliseconds = 0);

        /// <summary>
        /// Prints to the console in the same fashion as a typewriter
        /// </summary>
        /// <param name="line">String to print to console</param>
        /// <param name="milliseconds">Number of milliseconds to wait until starting to print.</param>
        /// <param name="oneNewLine">Boolean, if true adds a linebreak before typing the string</param>
        void Type(string content, int milliSeconds = 0, bool onNewLine = false);

        /// <summary>
        /// Prints to the console in the same fashion as a typewriter and centered
        /// </summary>
        /// <param name="line">String to print to console</param>
        void TypeCentered(string line, int milliseconds = 0);

        /// <summary>
        /// Returns the users input from the console
        /// </summary>
        /// <returns>A string of the users inputer</returns>
        string ReadInput();

        /// <summary>
        /// Clears the Console window of all text
        /// </summary>
        void ClearConsole();
        /// <summary>
        /// Prints an NPC character's response to the player or another NPC
        /// </summary>
        /// <param NPC="npc">NPC that is responding</param>
        /// <param name="response">NPC's response text</param>
        void PrintDialogueResponse(Npc npc, string response);
        /// <summary>
        /// Prompt Player to enter dialogue
        /// </summary>
        /// <param Player="player">Current character the player is using</param>
        string ReadDialogue(Player player);
        /// <summary>
        /// Use this when there is a computer issue and you need a higher power to explain the problem to a user (exceptions, bad data, inappropriate input)
        /// </summary>
        /// <param Text="string">Text to be displayed in special format</param>
        void PrintDivineText(string text);


    }
}

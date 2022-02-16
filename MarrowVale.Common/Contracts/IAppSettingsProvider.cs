using System;
using System.Collections.Generic;
using System.Text;

namespace MarrowVale.Common.Contracts
{
    public interface IAppSettingsProvider
    {
        string BasePromptFileLocation { get; }
        string PromptEvaluationLocation { get; }
        string PromptInterpretCommandLocation { get; }
        string PromptSummaryLocation { get; }
        string CombatFileLocation { get; }
        string DataFilesLocation { get; }
        string SoundFilesLocation { get; }
        string DialogueFilesLocation { get; }
        string DivineInterventionLocation { get; }
        bool AskForRating { get; }
        int MainMenuWait { get; }  
        int CharacterCreateWait { get; }
        int TypeSpeed { get; }
        string OpenAiKey { get; }
        string Neo4jUser { get; }
        string Neo4jPassword { get; }
        string Neo4jUrl { get; }

    }
}

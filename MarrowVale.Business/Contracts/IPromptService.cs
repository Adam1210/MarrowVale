using MarrowVale.Business.Entities.Enums;
using MarrowVale.Business.Entities.Enums.Combat;
using MarrowVale.Business.Entities.Prompts;
using MarrowVale.Common.Prompts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarrowVale.Business.Contracts
{
    public interface IPromptService
    {
        StandardPrompt GenerateAttackDescription(string text, CombatActionEnum actionType, CombatSeverityEnum severity);
        StandardPrompt GenerateDefenseDescription(string text, CombatActionEnum actionType, CombatSeverityEnum severity);
        StandardPrompt GenerateCombatIntentPrompt(string text);
        StandardPrompt GenerateDirectObjectPrompt(string text, CommandEnum command);
        StandardPrompt GenerateSubjectVerbObjectPrompt(string text, CommandEnum command);
        StandardPrompt GenerateDialogueSummaryPrompt(string dialogue);
        StandardPrompt GenerateDialogueIntentPrompt(string text, DialogueTypeEnum intentType);
        StandardPrompt GenerateDialoguePrompt(string text, DialogueResopnseTypeEnum dialogueType, string outputPrefix);
        StandardPrompt GenerateDivineText(string text);
        StandardPrompt GenerateSmiteText(string text);
        StandardPrompt GenerateSummaryDescription(string text, SummaryTypeEnum summaryType);
        StandardPrompt CommandTypePrompt(string command);

    }
}

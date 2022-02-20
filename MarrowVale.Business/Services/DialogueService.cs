using MarrowVale.Business.Contracts;
using MarrowVale.Business.Entities.Entities;
using MarrowVale.Business.Entities.Enums;
using MarrowVale.Common;
using MarrowVale.Common.Contracts;
using MarrowVale.Data.Contracts;
using Neo4jClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarrowVale.Business.Services
{
    public class DialogueService : IDialogueService
    {
        private readonly IAiService _aiService;
        private readonly IGraphClient _graphClient;
        private readonly IPromptService _promptService;
        private readonly IPrintService _printService;
        private readonly INpcRepository _npcRepository;
        private readonly IWorldContextService _worldContextService;
        private readonly INpcActionService _npcActionService;

        public DialogueService(IAiService aiService, IGraphClient graphClient, IPromptService promptService, IPrintService printService, INpcRepository npcRepository,
                                IWorldContextService worldContextService, INpcActionService npcActionService)
        {
            _aiService = aiService;
            _graphClient = graphClient;
            _promptService = promptService;
            _printService = printService;
            _npcRepository = npcRepository;
            _worldContextService = worldContextService;
            _npcActionService = npcActionService;
        }

        public void Talk(Player player, Npc npc)
        {
            npc = _npcRepository.GetById(npc.Id).Result;
            var conversation = new StringBuilder();
            bool conversationEnded = false;
            _printService.Print("");

            while (!conversationEnded)
            {
                var playerText = _printService.ReadDialogue(player);
                conversation.Append($"Player:{playerText}");

                var responseType = DetermineReponseType(playerText);


                if (responseType == DialogueResopnseTypeEnum.Purchase)
                {
                    _npcActionService.PurchaseItem(npc, player, playerText);
                    break;
                }

                if (responseType == DialogueResopnseTypeEnum.Sell)
                {
                    _npcActionService.SellItem(npc, player, playerText);
                    break;
                }

                string response;
                if (responseType == DialogueResopnseTypeEnum.Answer)
                    response = respondToPlayer(conversation.ToString(), npc, responseType);
                else
                    response = respondToPlayer(conversation.ToString(), npc, responseType);

                if (responseType == DialogueResopnseTypeEnum.Farewell)
                    conversationEnded = true;

                conversation.AppendNewLine("");
                conversation.AppendNewLine($"{npc.FullName}:{response}");
                _printService.PrintDialogueResponse(npc, response);
            }

            var summary = summarizeConverstion(conversation.ToString(), npc);
            _npcRepository.CreateMemory(npc, summary);
        }

        private DialogueResopnseTypeEnum DetermineReponseType(string text)
        {
            var prompt = _promptService.GenerateDialogueIntentPrompt(text, DialogueTypeEnum.InterpretIntent);
            var responseType = _aiService.Complete(prompt).Result?.Trim()?.ToUpper();

            if (responseType == DialogueResopnseTypeEnum.Question.ToString().ToUpper())
                return DialogueResopnseTypeEnum.Answer;
            else if (responseType == DialogueResopnseTypeEnum.Introduction.ToString().ToUpper())
                return DialogueResopnseTypeEnum.Introduction;
            else if (responseType == DialogueResopnseTypeEnum.Statement.ToString().ToUpper())
                return DialogueResopnseTypeEnum.Statement;
            else if (responseType == DialogueResopnseTypeEnum.Farewell.ToString().ToUpper())
                return DialogueResopnseTypeEnum.Farewell;
            else if (responseType == DialogueResopnseTypeEnum.Attack.ToString().ToUpper())
                return DialogueResopnseTypeEnum.Attack;
            else if (responseType == DialogueResopnseTypeEnum.Purchase.ToString().ToUpper())
                return DialogueResopnseTypeEnum.Purchase;
            else if (responseType == DialogueResopnseTypeEnum.Sell.ToString().ToUpper())
                return DialogueResopnseTypeEnum.Sell;
            else if (responseType == DialogueResopnseTypeEnum.Give.ToString().ToUpper())
                return DialogueResopnseTypeEnum.Give;
            else
                return DialogueResopnseTypeEnum.Confused;
        }



        private string respondToPlayer(string conversation, Npc c, DialogueResopnseTypeEnum responseType)
        {
            var context = generateContext(conversation, c, responseType);
            var character = _graphClient.Cypher
                                .Match("(character:Character)")
                                .Where((Npc character) => character.Name == c.Name)
                                .Return(character => character.As<Npc>())
                                .ResultsAsync.Result.FirstOrDefault();

            StringBuilder input = new StringBuilder();
            input.AppendNewLine($"{character.FullName} is a {character.Sentiment.ToString()} {character.Race.ToString()} {character.Occupation.ToString()}.  {context}");
            input.Append(conversation);
            var dialoguePrompt = _promptService.GenerateDialoguePrompt(input.ToString(), responseType, $"{character.FullName}:");

            return _aiService.Complete(dialoguePrompt).Result;

        }

        private string summarizeConverstion(string conversation, Npc Character)
        {
            var summary = _promptService.GenerateDialogueSummaryPrompt(conversation);
            return _aiService.Complete(summary).Result;
        }

        private string memoryOfPlayer(Npc c)
        {
            var conversation = _npcRepository.MemoryofPlayer(c);
            return conversation != null ? $"In their last conversation {conversation.Summary}" : null;
        }

        private string generateContext(string conversation, Npc character, DialogueResopnseTypeEnum responseType)
        {
            var characterKnowledge = _npcRepository.AllKnowledge(character);
            var triggeredMemory =_worldContextService.ContextSearch(conversation, characterKnowledge);

            if (triggeredMemory != null)
            {
                var memoryContext = $"{character.Name} {triggeredMemory?.RelationshipLabel?.FirstOrDefault()} ";
                memoryContext = memoryContext + string.Join(", ",triggeredMemory.Relations.Select(x => x.Node.Name));
                 
                return memoryContext;
            }


            if (responseType == DialogueResopnseTypeEnum.Introduction)
                return memoryOfPlayer(character) ?? character.Objective;
            else if (responseType == DialogueResopnseTypeEnum.Farewell)
            { 
            
            }
            else if (responseType == DialogueResopnseTypeEnum.Statement)
            {

            }
            else if (responseType == DialogueResopnseTypeEnum.Attack)
            {

            }
            else if (responseType == DialogueResopnseTypeEnum.Confused)
            {

            }

            return null;
        }

    }
}

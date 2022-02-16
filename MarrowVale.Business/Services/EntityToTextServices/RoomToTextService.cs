using MarrowVale.Business.Contracts;
using MarrowVale.Business.Entities.Entities;
using MarrowVale.Common.Prompts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarrowVale.Business.Services.EntityToTextServices
{
    public class RoomToTextService: IEntityToTextService
    {
        public StandardPrompt GeneratePrompt<T>(T entity, string context = null) where T : GraphNode
        {
            var input = entity.DescriptionPromptInput();
            return null;

        }

    }
}

using MarrowVale.Business.Entities.Entities;
using MarrowVale.Common.Prompts;
using OpenAI_API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarrowVale.Business.Contracts
{
    public interface IEntityToTextService
    {
        StandardPrompt GeneratePrompt<T>(T entity, string context = null) where T : GraphNode;
    }
}

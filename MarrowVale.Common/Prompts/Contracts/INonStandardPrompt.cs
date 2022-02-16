using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarrowVale.Common.Prompts.Contracts
{
    public interface INonStandardPrompt
    {
        StandardPrompt Standardize();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarrowVale.Common
{
    public static class Extension
    {
        public static void AppendNewLine(this StringBuilder sb, string text)
        {
            sb.Append($"{text}\n");
        }
    }
}

using MarrowVale.Business.Entities.Entities;
using MarrowVale.Business.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarrowVale.Business.Entities.Commands
{
    public class Command
    {
        public Command()
        {

        }
        public Command(CommandEnum type)
        {
            Type = type;
        }

        public CommandEnum Type { get; set; }
        public GraphNode DirectObjectNode { get; set; }
        public GraphNode IndirectObjectNode { get; set; }
        public string Input { get; set; }

    }
}

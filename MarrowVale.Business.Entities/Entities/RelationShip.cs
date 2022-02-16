using MarrowVale.Business.Entities.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarrowVale.Business.Entities.Entities
{
    public class Relationship
    {
        [JsonIgnore]
        public Npc Toward { get; set; }
        [JsonIgnore]
        public RelationEnum Relation { get; set; }
        public int StrengthofRelation { get; set; }

    }
}

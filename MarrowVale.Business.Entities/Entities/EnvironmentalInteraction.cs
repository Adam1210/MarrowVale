﻿using MarrowVale.Business.Entities.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarrowVale.Business.Entities.Entities
{
    public class EnvironmentalInteraction
    {
        [JsonConstructor]
        public EnvironmentalInteraction(string Name, string Description, int Difficulty, InteractionTypeEnum Type)
        {
            this.Name = Name;
            this.Description = Description;
            this.Difficulty = Difficulty;
            this.Type = Type;
        }

        public string Name { get; }
        public string Description { get; }
        public int Difficulty { get; }
        public InteractionTypeEnum Type { get; }
    }
}

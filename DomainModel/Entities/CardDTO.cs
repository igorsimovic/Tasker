﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Entities
{
    public class CardDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
        public string Description { get; set; }

        public CardDTO(string id, string name, int order, string description)
        {
            this.Id = id;
            this.Name = name;
            this.Order = order;
            this.Description = description;
        }
    }
}

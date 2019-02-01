﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KnowledgeBaseEmployees.Models
{
    public class TechnologyProject
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public int TechnologyId { get; set; }

        public Project Project { get; set; }
        public Technology Technology { get; set; }
    }
}

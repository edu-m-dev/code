﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace chores.bl.ef
{
    public partial class Chore
    {
        public Chore()
        {
            CompletedChores = new HashSet<CompletedChore>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<CompletedChore> CompletedChores { get; set; }
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalToWeb
{
    public class Blog
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime TimeAdded { get; set; }
        public int? UserId { get; set; }
    }
}

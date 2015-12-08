﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace MoTMe.Models
{
    public class MoTMeContext : ApplicationDbContext
    {
        // IDbSet, IQueryable
        //public virtual DbSet<JitterUser> JitterUsers { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
    }
}
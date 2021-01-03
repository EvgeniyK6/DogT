using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DogT.Models;

namespace DogT.Data
{
    public class DogTContext : DbContext
    {
        public DogTContext(DbContextOptions<DogTContext> options) : base(options)
        {
        }

        DbSet<User> Users { get; set; }
    }
}

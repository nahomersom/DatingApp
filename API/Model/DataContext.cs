
// this class is responsible for connecting to our db and crud operations to our database server
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using Microsoft.EntityFrameworkCore;
// namespace can be anything but by default it's based on the folder structure of out current classo
namespace API.Model
{
    public class DataContext : DbContext
    {
        //DbContextOptions we got from DbContext and it contain our conection string and database provide provider to use
        public DataContext(DbContextOptions options) : base(options)  
        {
            
        }
        //users table name
        public DbSet<AppUser> Users { get; set; }

    }
}
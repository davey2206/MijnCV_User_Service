using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MijnCV_API.Models;

namespace MijnCV_User_Service.Models
{
    public class MijnCV_User_ServiceContext : DbContext
    {
        public MijnCV_User_ServiceContext(DbContextOptions<MijnCV_User_ServiceContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; } = default!;
    }
}

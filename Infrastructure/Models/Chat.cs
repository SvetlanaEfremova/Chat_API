using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models
{
    public class Chat
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Message> Messages { get; } = new List<Message>();

        public string CreatedBy { get; set; }

    }
}

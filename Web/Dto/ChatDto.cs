using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web;

namespace Web.Dto
{
    public class ChatDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<MessageDto> Messages { get; set; }
        public string CreatedBy { get; set; }
    }
}

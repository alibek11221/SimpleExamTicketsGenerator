using System.Collections.Generic;

namespace WebApplication.Models
{
    public class Ticket
    {
        public Ticket(int number)
        {
            Number = number;
        }
        public int Number { get; private set; }
        public List<Question> Questions { get; } = new List<Question>();
    }
}
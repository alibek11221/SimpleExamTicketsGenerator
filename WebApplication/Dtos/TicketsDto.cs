using System.Collections.Generic;

namespace WebApplication.Dtos
{
    public class TicketsDto
    {
        public string GroupName { get; set; }
        public string Semester { get; set; }
        public string DisciplineName { get; set; }
        public int Maximum { get; set; }
        public List<QuestionDto> Questions { get; set; } = new List<QuestionDto>();
    }
}
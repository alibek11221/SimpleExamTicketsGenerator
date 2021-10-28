using System;
using System.Collections.Generic;
using WebApplication.Models;

namespace WebApplication.Services
{
    public interface IQuestionsService
    {
        public void AddQuestion(int number, string questions);

        public void AssignQuestions(Dictionary<int, string[]> questions);

        public List<Ticket> GenerateTickets(int maximum);
    }
    public class QuestionsService : IQuestionsService
    {
        private Dictionary<int, string[]> _questions = new();
        private readonly Random _random = new Random();
        private int _ticketNumber = 1;
        
        public void AddQuestion(int number, string questions)
        {
            _questions.Add(number, questions.Split('\n'));
        }

        public void AssignQuestions(Dictionary<int, string[]> questions)
        {
            _questions = questions;
        }

        private Ticket CreateTicket()
        {
            var ticket = new Ticket(_ticketNumber);
            _ticketNumber++;
            foreach (KeyValuePair<int, string[]> question in _questions)
            {
                int random = _random.Next(0, question.Value.Length);
                string randomQuestion = question.Value[random];
                ticket.Questions.Add(new Question(randomQuestion, question.Key));
            }

            return ticket;
        }

        public List<Ticket> GenerateTickets(int maximum)
        {
            var output = new List<Ticket>();
            for (int i = 1; i < maximum; i++)
            {
                output.Add(CreateTicket());
            }

            return output;
        }
    }
}
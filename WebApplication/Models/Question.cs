using System;

namespace WebApplication.Models
{
    public class Question
    {
        public Question(string selectedQuestion, int number)
        {
            SelectedQuestion = selectedQuestion;
            Number = number;
        }
        public int Number { get; private set; }
        public string SelectedQuestion { get; private set; }
    }
}
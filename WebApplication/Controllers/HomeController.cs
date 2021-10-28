using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NPOI.XWPF.UserModel;
using WebApplication.Dtos;
using WebApplication.Models;
using WebApplication.Services;

namespace WebApplication.Controllers
{
    //TODO Почистить контроллер
    public class HomeController : Controller
    {
        private readonly IQuestionsService _questionsService;

        public HomeController(IQuestionsService questionsService)
        {
            _questionsService = questionsService;
        }

        [HttpGet("/")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("/create-doc")]
        public IActionResult CreateDoc([FromBody] TicketsDto ticketsData)
        {
            if (ticketsData.DisciplineName == null)
            {
                return Json("errror");
            }

            var newFile = @"file.docx";
            foreach (var dataQuestion in ticketsData.Questions)
            {
                _questionsService.AddQuestion(dataQuestion.Number, dataQuestion.Questions);
            }

            using var fs = new FileStream(newFile, FileMode.OpenOrCreate, FileAccess.Write);
            var document = new XWPFDocument();
            var tickets = _questionsService.GenerateTickets(ticketsData.Maximum);
            foreach (var ticket in tickets)
            {
                var p1 = document.CreateParagraph();
                p1.Alignment = ParagraphAlignment.CENTER;
                var p1Run = p1.CreateRun();
                p1Run.IsBold = true;
                p1Run.FontFamily = "Times New Roman";
                p1Run.FontSize = 10;
                p1Run.SetText(
                    "Грозненский государственный нефтяной технический университет им.акад. М.Д. Миллионщикова");
                var p2 = document.CreateParagraph();
                p2.Alignment = ParagraphAlignment.CENTER;
                var p2Run = p2.CreateRun();
                p2Run.IsBold = true;
                p2Run.FontFamily = "Times New Roman";
                p2Run.FontSize = 10;
                p2Run.SetText("Институт цифровой экономики и технологического предпринимательства");

                var p3 = document.CreateParagraph();
                p3.Alignment = ParagraphAlignment.CENTER;
                var p3Run = p3.CreateRun();
                p3Run.IsBold = true;
                p3Run.FontFamily = "Times New Roman";
                p3Run.FontSize = 10;
                p3Run.SetText($"Группа \"{ticketsData.GroupName}\" Семестр \"{ticketsData.Semester}\"");
                var p4 = document.CreateParagraph();
                p4.Alignment = ParagraphAlignment.CENTER;

                var p4Run = p4.CreateRun();
                p4Run.IsBold = true;
                p4Run.FontFamily = "Times New Roman";
                p4Run.FontSize = 10;
                p4Run.SetText($"Дисциплина {ticketsData.DisciplineName}");
                
                var p5 = document.CreateParagraph();
                p5.Alignment = ParagraphAlignment.CENTER;

                var p5Run = p5.CreateRun();
                p5Run.IsBold = true;
                p5Run.FontFamily = "Times New Roman";
                p5Run.FontSize = 10;
                p5Run.SetText($"Билет № {ticket.Number}");
                
                foreach (var ticketQuestion in ticket.Questions)
                {
                    var questions = document.CreateParagraph();
                    questions.Alignment = ParagraphAlignment.LEFT;
                    var questionsRun = questions.CreateRun();
                    questionsRun.FontFamily = "Times New Roman";
                    questionsRun.FontSize = 10;
                    questionsRun.IsBold = false;
                    questionsRun.SetText($"{ticketQuestion.Number}. {ticketQuestion.SelectedQuestion}");
                }

                var foot = document.CreateParagraph();
                foot.Alignment = ParagraphAlignment.DISTRIBUTE;
                var footRun = foot.CreateRun();
                footRun.IsBold = true;
                footRun.FontFamily = "Times New Roman";
                footRun.FontSize = 10;
                footRun.SetText("Подпись преподавателя___________________ Подпись заведующего кафедрой___________________");
                document.CreateParagraph().CreateRun().SetText("");
                document.CreateParagraph()
                    .CreateRun()
                    .SetText("_____________________________________________________________________________________");
                document.CreateParagraph().CreateRun().SetText("");
            }
            document.Write(fs);
            return File(new FileStream(newFile, FileMode.Open, FileAccess.Read),"application/octet-stream", "Bilets.docx");

        }
    }
}
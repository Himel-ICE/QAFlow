using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CFlow.Models;

namespace CFlow.Controllers
{
    public class AnswerController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult InsertAnswer(FormCollection formCollection)
        {
            string Answer = formCollection["answer-of-you"].ToString();
            int UID = Convert.ToInt32(formCollection["UserID"].ToString());
            int QID = Convert.ToInt32(formCollection["QID"].ToString());

            Answer = Answer.Trim();

            if (Answer != "")
                Answers.InsertAnswer(Answer, QID, UID);
            else return RedirectToAction("Question", "Home");
            return RedirectToAction("Question", "Home");
        }
    }
}
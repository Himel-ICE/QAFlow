using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CFlow.Models;

namespace CFlow.Controllers
{
    public class QuestionController : Controller
    {
        // GET: Question
        public ActionResult Question()
        {
            return View();
        }
        [HttpPost]
        public ActionResult InsertQuestion(FormCollection formCollection)
        {
            string Title = formCollection["Title"].ToString();
            string Description = formCollection["Description"].ToString();
            string Tags = formCollection["tags"].ToString();
            int UserID = Convert.ToInt32(formCollection["UserID"].ToString());

            if ((Title == " ") || (Description == " ") || (Tags == " ") || (Title == "") || (Description == "") || (Tags == ""))
                Session["InertQuestionValidation"] = "False";
            else 
                Session["InertQuestionValidation"] = "True";

            int result = Questions.InsertQuestion(Title, Description, UserID, Tags);


            if (result == 1)
                Session["InertQuestionValidation"] = "True";
            else
                Session["InertQuestionValidation"] = "False";

            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public ActionResult ViewQuestion(FormCollection formCollection)
        {
            int Qid = Convert.ToInt32(formCollection["QID"].ToString());
            int result = Questions.UpdateViewConut(Qid);
            Questions.QuestionView(Qid);
            Session["QID"] = Qid;
            return RedirectToAction("Question", "Home");
        }
        [HttpPost]
        public ActionResult InsertVote(FormCollection formCollection)
        {
            int QID = Convert.ToInt32(formCollection["QID"].ToString());
            int UserID = Convert.ToInt32(formCollection["UserID"].ToString());
            int Vote = Convert.ToInt32(formCollection["Vote"].ToString());
            int result = Questions.InsertVote(QID, UserID, Vote);
            return RedirectToAction("Question", "Home");
        }
    }
}
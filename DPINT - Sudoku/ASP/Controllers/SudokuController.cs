using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Activation;
using System.Web;
using System.Web.Mvc;
using Wrapper;

namespace ASP.Controllers
{
    public class SudokuController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.Message = Session["Message"];
            ViewBag.Game = GetGame();

            return View();
        }
        
        [HttpPost]
        public ActionResult Set(int x, int y, int value)
        {
            var message = string.Empty;

            if (!GetGame().Set(x, y, value))
            {
                message = "That was an invalid move!";
            }

            if (GetGame().IsComplete)
            {
                message = "You Won!";
            }

            Session["Message"] = message;

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Cheat()
        {
            GetGame().Cheat();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult NewGame()
        {
            Session["Game"] = null;

            return RedirectToAction("Index");
        }

        private Wrapper.Sudoku GetGame()
        {
            if (Session["Game"] == null)
            {
                Session["Game"] = new Wrapper.Sudoku();
            }

            return (Wrapper.Sudoku) Session["Game"];
        }
    }
}
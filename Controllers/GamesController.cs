using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameListMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameListMVC.Controllers
{
    [Authorize]
    public class GamesController : Controller
    {
        private readonly ApplicationDbContext _db;
        [BindProperty]
        public Game Game { get; set; }
        public GamesController(ApplicationDbContext db)
        {
            _db = db;
        }
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
        [Authorize]
        public IActionResult Upsert(int? id)
        {
            Game = new Game();
            if (id == null)
            {
                //create
                return View(Game);
            }
            //update
            Game = _db.Games.FirstOrDefault(u => u.Id == id);
            if (Game == null)
            {
                return NotFound();
            }
            return View(Game);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Upsert()
        {
            if (ModelState.IsValid)
            {
                if (Game.Id == 0)
                {
                    //create
                    _db.Games.Add(Game);
                }
                else
                {
                    _db.Games.Update(Game);
                }
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(Game);
        }

        #region API Calls
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(new { data = await _db.Games.ToListAsync() });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var GameFromDb = await _db.Games.FirstOrDefaultAsync(u => u.Id == id);
            if (GameFromDb == null)
            {
                return Json(new { success = false, message = "Error while Deleting" });
            }
            _db.Games.Remove(GameFromDb);
            await _db.SaveChangesAsync();
            return Json(new { success = true, message = "Delete successful" });
        }
        #endregion
    }
}
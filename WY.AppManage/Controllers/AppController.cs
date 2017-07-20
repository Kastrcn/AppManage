using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppManage.Model;
using WY.AppManage.Data;
using WY.AppManage.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using WY.AppManage.Models.AppViewModels;

namespace WY.AppManage.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]/")]
    public class AppController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AppController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/App
        [HttpGet]
        public IActionResult GetAppList(int id)
        {
            if (id != 0)
            {
                var canyon = (from d in _context.Project where d.Id == id select d).Single();
                _context.Entry(canyon).Collection(d => d.App).Load();
                return Ok(new { code = 1, msg = "ok", date = canyon.App.Select(e => new AppViewModel { Id = e.Id, FileUrl = e.FileUrl, CreateTime = e.CreateTime, Name = e.Name, Number = e.Number }) });
            }
            else
            {
                var data = _context.App.Select(t => new AppViewModel { Id = t.Id, Name = t.Name, FileUrl = t.FileUrl, Number = t.Number, CreateTime = t.CreateTime });
                return Ok(new { code = 1, msg = "ok", date = data });
            }
        }
        // GET: api/App/5
        [HttpGet]
        public async Task<IActionResult> GetApp(int id)
        {

            if (!ModelState.IsValid)
            {
                return Ok(new { code = 0, msg = BadRequest(ModelState).Value });
            }

            var app = await _context.App.SingleOrDefaultAsync(m => m.Id == id);

            if (app == null)
            {
                return Ok(new { code = 0, msg = "数据不存在" });
            }
            return Ok(new { code = 1, msg = "ok", date = new AppViewModel { Id = app.Id, CreateTime = app.CreateTime, Name = app.Name, FileUrl = app.FileUrl, Number = app.Number } });
        }

        // PUT: api/App/5
        [HttpPut]
        public async Task<IActionResult> App(int id, [Bind("Name,Number,FileUrl")]ChangeAppViewModel ChangeAppViewModel)
        {
            if (!ModelState.IsValid)
            {
                return Ok(new { code = 0, msg = BadRequest(ModelState).Value });
            }
            _context.Entry(new App { Id = id, Name = ChangeAppViewModel.Name, Number = ChangeAppViewModel.Number, FileUrl = ChangeAppViewModel.FileUrl }).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppExists(id))
                {
                    return Ok(new { code = 0, msg = "数据不存在" });
                }
                else
                {
                    throw;
                }
            }
            return Ok(new { code = 1, msg = "ok" });
        }

        // POST: api/App
        [HttpPost]
        public async Task<IActionResult> App(int id,[Bind("Name,Number,FileUrl")] AddAppViewModel AddAppViewModel)
        {
            if (!ModelState.IsValid)
            {
                return Ok(new { code = 0, msg = BadRequest(ModelState).Value });
            }

            //var canyon = (from d in _context.Project
            //              where d.Id ==id
            //              select d).Single();
            //canyon.App.Add()

            //var t = _context.Project.FirstOrDefault();
            //t.App.Add(new App { });
            _context.App.Add(new App { CreateTime = DateTime.Now, FileUrl = AddAppViewModel.FileUrl, Number = AddAppViewModel.Number, Name = AddAppViewModel.Name });
            await _context.SaveChangesAsync();
            return Ok(new { code = 1, msg = "ok" });
        }

        // DELETE: api/App/5
        [HttpDelete]
        public async Task<IActionResult> App(int id)
        {
            if (!ModelState.IsValid)
            {
                return Ok(new { code = 0, msg = BadRequest(ModelState).Value });
            }
            var app = await _context.App.SingleOrDefaultAsync(m => m.Id == id);
            if (app == null)
            {
                return Ok(new { code = 0, msg = "数据不存在" });
            }

            _context.App.Remove(app);
            await _context.SaveChangesAsync();

            return Ok(new { code = 1, msg = "删除成功" });
        }

        private bool AppExists(int id)
        {
            return _context.App.Any(e => e.Id == id);
        }
    }
}
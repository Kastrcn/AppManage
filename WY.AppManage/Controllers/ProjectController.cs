using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WY.AppManage.Data;
using WY.AppManage.Models;
using AppManage.Model;
using WY.AppManage.Models.ProjectViewModels;
using Microsoft.AspNetCore.Authorization;

namespace WY.AppManage.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]/")]
    public class ProjectController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProjectController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Project
        [HttpGet]
        public IActionResult GetProjectList()
        {
            var data = _context.Project.Select(t => new ProjectViewModel { Id = t.Id, Name = t.Name, CreateTime = t.CreateTime });
            return Ok(new { code = 1, msg = "ok", date = data });

        }

        // GET: api/Project/5
        [HttpGet]
        public async Task<IActionResult> GetProject(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var project = await _context.Project.SingleOrDefaultAsync(m => m.Id == id);

            if (project == null)
            {

                return Ok(new { code = 0, msg = "数据不存在" });

            }

            return Ok(new { code = 1, msg = "ok", date = new ProjectViewModel { Id = project.Id, CreateTime = project.CreateTime, Name = project.Name } });

        }

        // PUT: api/Project/5
        [HttpPost]
        public async Task<IActionResult> Projectput(int id, [Bind("Name")]ChangeProjectViewModel ChangeProjectViewModel)
        {
            if (!ModelState.IsValid)
            {
                return Ok(new { code = 0, msg = BadRequest(ModelState).Value });
            }

            var p = _context.Project.SingleOrDefault(m => m.Id == id);
            p.Name = ChangeProjectViewModel.Name;

            _context.Entry(p).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectExists(id))
                {
                    return Ok(new { code = 0, msg = "id不存在" });
                }
                else
                {
                    throw;
                }
            }

            return Ok(new { code = 1, msg = "ok" });
        }

        // POST: api/Project
        [HttpPost]
        public async Task<IActionResult> Project([FromBody]AddProjectViewModel AddProjectViewModel)
        {
            if (!ModelState.IsValid)
            {
                return Ok(new { code = 0, msg = BadRequest(ModelState).Value });
            }
            _context.Project.Add(new Project { Name = AddProjectViewModel.Name, CreateTime = DateTime.Now });
            await _context.SaveChangesAsync();
            return Ok(new { code = 1, msg = "ok" });
        }

        // DELETE: api/Project/5
        [HttpPost]
        public async Task<IActionResult> Projectdel(int id)
        {
            if (!ModelState.IsValid)
            {
                return Ok(new { code = 0, msg = BadRequest(ModelState).Value });
            }

            var projectViewModel = await _context.Project.SingleOrDefaultAsync(m => m.Id == id);
            if (projectViewModel == null)
            {
                return Ok(new { code = 0, msg = "id不存在" });
            }

            _context.Project.Remove(projectViewModel);
            await _context.SaveChangesAsync();

            return Ok(new { code = 1, msg = "删除成功" });
        }

        private bool ProjectExists(int id)
        {
            return _context.Project.Any(e => e.Id == id);
        }
    }
}
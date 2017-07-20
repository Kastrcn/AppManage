using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WY.AppManage.Data;
using WY.AppManage.Models;
using WY.AppManage.Models.SuggestViewModels;

namespace WY.AppManage.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]/")]
    public class SuggestsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SuggestsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Suggests
        [HttpGet]
        public IActionResult GetSuggestList()
        {

            var data = _context.Suggest.Select(t => new SuggestViewModel { Id = t.Id, PhoneModel = t.PhoneModel, Location = t.Location, Content = t.Content, CreateTime = t.CreateTime });
            return Ok(new { code = 1, msg = "ok", date = data });

        }

        // GET: api/Suggests/5
        [HttpGet]
        public async Task<IActionResult> GetSuggest(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var suggest = await _context.Suggest.SingleOrDefaultAsync(m => m.Id == id);

            if (suggest == null)
            {
                return Ok(new { code = 0, msg = "数据不存在" });
            }

            return Ok(new { code = 1, msg = "ok", date = new SuggestViewModel { Id = suggest.Id, CreateTime = suggest.CreateTime, PhoneModel = suggest.PhoneModel, Content = suggest.Content, Location = suggest.Location } });
        }


        // POST: api/Suggests
        [HttpPost]
        public async Task<IActionResult> Suggest(AddSuggestViewModel AddSuggestViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.Suggest.Add(new Suggest { PhoneModel = AddSuggestViewModel.PhoneModel, Location = AddSuggestViewModel.Location, Content = AddSuggestViewModel.Content, CreateTime = DateTime.Now });
            await _context.SaveChangesAsync();
            return Ok(new { code = 1, msg = "ok" });
        }

        // DELETE: api/Suggests/5
        [HttpDelete]
        public async Task<IActionResult> Suggest(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var suggest = await _context.Suggest.SingleOrDefaultAsync(m => m.Id == id);
            if (suggest == null)
            {
                return Ok(new { code = 0, msg = "id不存在" });
            }

            _context.Suggest.Remove(suggest);
            await _context.SaveChangesAsync();

            return Ok(new { code = 1, msg = "删除成功" });
        }

        private bool SuggestExists(int id)
        {
            return _context.Suggest.Any(e => e.Id == id);
        }
    }
}
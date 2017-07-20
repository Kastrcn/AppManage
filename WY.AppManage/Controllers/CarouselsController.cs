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
using WY.AppManage.Models.CarouselViewModels;

namespace WY.AppManage.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]/")]
    public class CarouselsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CarouselsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Carousels
        [HttpGet]
        public IActionResult GetCarouselList()
        {
            var data = _context.Carousel.Select(t => new CarouselViewModel { Id = t.Id, Name = t.Name, CreateTime = t.CreateTime, ImgUrl = t.Url });
            return Ok(new { code = 1, msg = "ok", date = data });
        }

        // GET: api/Carousels/5
        [HttpGet]
        public async Task<IActionResult> GetCarousel([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var carousel = await _context.Carousel.SingleOrDefaultAsync(m => m.Id == id);

            if (carousel == null)
            {
                return Ok(new { code = 0, msg = "数据不存在" });
            }
            return Ok(new { code = 1, msg = "ok", date = new CarouselViewModel { Id = carousel.Id, CreateTime = carousel.CreateTime, Name = carousel.Name, ImgUrl = carousel.Url } });
        }

        // PUT: api/Carousels/5
        [HttpPut]
        public async Task<IActionResult> Carousel(int id, [FromBody] CarouselViewModel CarouselViewModel)
        {
            if (!ModelState.IsValid)
            {
                return Ok(new { code = 0, msg = BadRequest(ModelState).Value });
            }


            _context.Entry(new Carousel { Id = id, Name = CarouselViewModel.Name, Url = CarouselViewModel.ImgUrl }).State = EntityState.Modified;


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarouselExists(id))
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


        // POST: api/Carousels
        [HttpPost]
        public async Task<IActionResult> Carousel(AddCarouselViewModel AddCarouselViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Carousel.Add(new Carousel { Url = AddCarouselViewModel.Url, Name = AddCarouselViewModel.Name, CreateTime = DateTime.Now });
            await _context.SaveChangesAsync();

            return Ok(new { code = 1, msg = "ok" });
        }

        // DELETE: api/Carousels/5
        [HttpDelete]
        public async Task<IActionResult> Carousel(int id)
        {
            if (!ModelState.IsValid)
            {
                return Ok(new { code = 0, msg = BadRequest(ModelState).Value });
            }

            var carousel = await _context.Carousel.SingleOrDefaultAsync(m => m.Id == id);
            if (carousel == null)
            {
                return Ok(new { code = 0, msg = "id不存在" });
            }

            _context.Carousel.Remove(carousel);
            await _context.SaveChangesAsync();

            return Ok(new { code = 1, msg = "删除成功" });
        }

        private bool CarouselExists(int id)
        {
            return _context.Carousel.Any(e => e.Id == id);
        }


    }
}
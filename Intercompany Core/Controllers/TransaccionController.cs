#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IntercompanyCore.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IntercompanyCore;
using AutoMapper;
using IntercompanyCore.DTOs;
 
namespace IntercompanyCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransaccionController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper mapper;

        public TransaccionController(ApplicationDbContext context, IMapper Mapper)
        {
            this._context = context;
            this.mapper = Mapper;
        }


        // POST: SocioNegocios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("CrearTransaccion")]
        public IActionResult Post(TransaccionCreacionDTO transaccionCreacionDTO)
        {
            DateTime localDate = DateTime.Now;
            Transaccion transaccion = this.mapper.Map<Transaccion>(transaccionCreacionDTO);
            transaccion.FechaSincronizacion = localDate;
            _context.Add(transaccion);
            _context.SaveChanges();
            return Ok();
        }

        [HttpGet("ObtenerTransacciones")]
        public async Task<ActionResult<List<Transaccion>>> Get()
        {
            return await _context.Transaccion.ToListAsync();
        }

        [HttpPut("EditarTransaccion/{id:int}")]
        public async Task<ActionResult<List<Transaccion>>> Put(Transaccion transaccion, int id)
        {
            if (transaccion.Id != id)
            {
                return BadRequest("La transaccion no coincide con el Id de la URL");
            }
            var existe = await _context.Transaccion.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return NotFound();
            }
            _context.Update(transaccion);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("BorrarTransaccion/{id:int}")]
        public async Task<ActionResult<List<Transaccion>>> Delete(int id)
        {
            var existe = await _context.Transaccion.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return NotFound();
            }
            _context.Remove(new Transaccion() { Id = id });
            await _context.SaveChangesAsync();
            return Ok();
        }

    }
}
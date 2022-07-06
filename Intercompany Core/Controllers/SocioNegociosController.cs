#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IntercompanyCore;
using AutoMapper;
using IntercompanyCore.DTOs;
using IntercompanyCore.Entities;
 
namespace IntercompanyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SocioNegociosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper mapper;

        public SocioNegociosController(ApplicationDbContext context, IMapper Mapper)
        {
            this._context = context;
            this.mapper = Mapper;
        }

        public async Task<ActionResult<List<SocioNegocios>>> Get()
        {

            // Console.WriteLine("CLAVE:  " + _context.SocioNegocios.Count<SocioNegocios>() + 1);
            return await _context.SocioNegocios.ToListAsync();
        }
    }
}
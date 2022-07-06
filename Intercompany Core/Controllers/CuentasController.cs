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
    public class CuentasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper mapper;

        public CuentasController(ApplicationDbContext context, IMapper Mapper)
        {
            _context = context;
            mapper = Mapper;
        }

        public async Task<ActionResult<List<Cuentas>>> Get()
        {
            return await _context.Cuentas.ToListAsync();
        }
    }
}
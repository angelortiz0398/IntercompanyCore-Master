#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IntercompanyCore;
using IntercompanyCore.DTOs;
using AutoMapper;
using IntercompanyCore.Entities;

namespace IntercompanyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper mapper;

        public ItemsController(ApplicationDbContext context, IMapper Mapper)
        {
            _context = context;
            mapper = Mapper;
        }
        
        public async Task<ActionResult<List<Items>>> Get()
        {

            //Console.WriteLine("CLAVE:  " + _context.Items.Count<Items>());
            return await _context.Items.ToListAsync();
        }
    }
}
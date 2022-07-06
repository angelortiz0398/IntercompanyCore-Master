#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IntercompanyCore;
using AutoMapper;
using IntercompanyCore.DTOs;
using Microsoft.EntityFrameworkCore;

namespace IntercompanyAPI.Controllers
{
    public class CentrosCostoController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper mapper;

        public CentrosCostoController()
        {
        }
         
        public CentrosCostoController(ApplicationDbContext context, IMapper Mapper)
        {
            this._context = context;
            this.mapper = Mapper;
        }


        public async Task<ActionResult<List<CentrosCosto>>> ObtenerCentrosCosto()
        {
            //.WriteLine(_context.CentrosCosto.FromSqlRaw("SELECT * From CentrosCosto Where Clave = {0}", Clave).FirstOrDefault());
            //return _context.CentrosCosto.FromSqlRaw("SELECT * From CentrosCosto Where Clave = {0}", Clave).FirstOrDefault();
            //return  _context.CentrosCosto.FirstOrDefault(cc => cc.Clave == Clave);
            return await _context.CentrosCosto.ToListAsync();
        }
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using vegga.Controllers.Resources;
using vegga.Models;
using vegga.Persistence;

namespace vegga.Controllers
{
    public class MakesController : Controller
    {
        private readonly VeggaDbContext _context;
        private readonly IMapper mapper;

        public MakesController(VeggaDbContext context, IMapper mapper)
        {
            this.mapper = mapper;
            _context = context;
        }

        [HttpGet("api/makes")]
        public async Task<IEnumerable<MakeResource>> GetMakes()
        {
            var makes = await _context.Makes
                .Include(m => m.Models)
                .ToListAsync();

            return mapper.Map<List<Make>,List<MakeResource>>(makes);
        }

    }
}
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
    [Route("api")]
    public class FeaturesController : Controller
    {
        private readonly VeggaDbContext context;
        private readonly IMapper mapper;
        public FeaturesController(VeggaDbContext context, IMapper mapper)
        {
            this.mapper = mapper;
            this.context = context;
        }

        [HttpGet("features")]
        public async Task<IEnumerable<KeyValuePairResource>> GetFeatures()
        {
            var features = await context.Features.ToListAsync();

            return mapper.Map<List<Feature>,List<KeyValuePairResource>>(features);
        }
    }
}
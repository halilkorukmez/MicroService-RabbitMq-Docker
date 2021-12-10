using MicroService.Sourcing.Entities;
using MicroService.Sourcing.Repositories.SourcingRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MicroService.Sourcing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuctionController : ControllerBase
    {
        private readonly ISourcingRepository _auctionRepository;
        private readonly ILogger<AuctionController> _logger;

        public AuctionController(ISourcingRepository auctionRepository,ILogger<AuctionController> logger)
        {
            _auctionRepository = auctionRepository;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Auction>),(int)HttpStatusCode.OK)]
        public async Task<ActionResult<Auction>> GetlistAsync()
        {
            var auction = await _auctionRepository.GetListAsync();
            return Ok(auction);

        } 
        
        [HttpGet("{id:length(24)}",Name ="GetAsync")]
        [ProducesResponseType(typeof(Auction),(int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<Auction>> GetAsync(string id)
        {
            var auction = await _auctionRepository.GetAsync(id);
            if (auction !=null)
               
               return Ok(auction);
            _logger.LogError($"Database Kaydı Bulunamadı! ID:{id})");
            return BadRequest();

        }

        [HttpPost]
        [ProducesResponseType(typeof(Auction), (int)HttpStatusCode.Created)]
        public async Task<ActionResult<Auction>> AddAsync([FromBody]Auction auction)
        {
             await _auctionRepository.AddAsync(auction);
            return CreatedAtRoute("GetAsync",new { id = auction.Id }, auction);

        }

        [HttpPut]
        [ProducesResponseType(typeof(Auction), (int)HttpStatusCode.Created)]
        public async Task<ActionResult<Auction>> UpdateAsync([FromBody]Auction auction)
        {
            return Ok(await _auctionRepository.UpdateAsync(auction));

        }

        [HttpDelete("{id:length(24)}")]
        [ProducesResponseType(typeof(Auction), (int)HttpStatusCode.Created)]
        public async Task<ActionResult<Auction>> DeleteAsync([FromBody] string id)
        {
            return Ok(await _auctionRepository.DeleteAsync(id));

        }
    }
}

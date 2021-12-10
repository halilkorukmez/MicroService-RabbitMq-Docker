using MicroService.Sourcing.Entities;
using MicroService.Sourcing.Repositories.BidRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MicroService.Sourcing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BidController : ControllerBase
    {
        private readonly IBidRepository _bidRepository;

        public BidController(IBidRepository bidRepository)
        {
            _bidRepository = bidRepository;
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult> SendBid([FromBody] Bid bid)
        {
            await _bidRepository.SendAsync(bid);
            return Ok();
        }

        [HttpGet("GetByAuctionId")]
        [ProducesResponseType(typeof(IEnumerable<Bid>),(int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Bid>>> GetByAuctionId(string id)
        {
            IEnumerable<Bid> bids = await _bidRepository.GetByAuctionIdAsync(id);
            return Ok(bids);
        }

        [HttpGet("GetWinnerId")]
        [ProducesResponseType(typeof(Bid), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Bid>> GetWinnerId(string id)
        {
            Bid bids = await _bidRepository.GetWinnerIdAsync(id);
            return Ok(bids);
        }



    }
}

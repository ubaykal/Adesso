using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WorldLeague.Business.Abstracts;
using WorldLeague.ViewModels;

namespace WorldLeague.Controllers
{
    public class WorldLeagueController : BaseController
    {
        private readonly IDrawLot _drawLot;

        public WorldLeagueController(IDrawLot drawLot)
        {
            _drawLot = drawLot;
        }

        [HttpPost]
        [Route("DrawLot")]
        [SwaggerOperation(
            Summary = "Kura Çekilişi Başlat")]
        public async Task<IActionResult> DrawLot(DrawLotRequestViewModel request)
        {
          var ss =  _drawLot.DrawLotsStart(request);
            return Ok(ss);
        }
    }
}
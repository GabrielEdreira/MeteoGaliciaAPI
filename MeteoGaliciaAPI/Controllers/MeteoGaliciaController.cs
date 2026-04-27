using MeteoGaliciaAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MeteoGaliciaAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MeteoGaliciaController(IMeteoGaliciaService _meteoGaliciaService) : ControllerBase
{

    [HttpGet("{municipio}")]
    public async Task<IActionResult> Get(string municipio)
    {
        try
        {
            var result = await _meteoGaliciaService.GetPredictionsAsync(municipio);

            return Ok(result);
        }
        catch(Exception ex)
        {
            return StatusCode(500, new { error = ex.Message });
        }
    }


}


using MeteoGaliciaAPI.Models;

namespace MeteoGaliciaAPI.Interfaces;

public interface IMeteoGaliciaService
{
    Task<MeteoResponse> GetPredictionsAsync(string municipio);
}


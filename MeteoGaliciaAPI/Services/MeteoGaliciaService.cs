using MeteoGaliciaAPI.Interfaces;
using MeteoGaliciaAPI.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MeteoGaliciaAPI.Services;

public class MeteoGaliciaService(Clients.MeteoGaliciaClient _client) : IMeteoGaliciaService
{
    /// <summary>
    /// Obtiene los datos de la API de MeteoGalicia
    /// </summary>
    /// <param name="municipio"></param>
    /// <returns></returns>
    /// <exception cref="Exception"> Si no puede accederse a los datos lanza una excepción </exception>
    public async Task<Models.MeteoResponse> GetPredictionsAsync(string municipio)
    {
        var result = new List<DiaPrediccion>();

        var jsonData = await _client.GetDataAsync(municipio);

        var data = JsonConvert.DeserializeObject(jsonData);
        if (data == null)
            throw new Exception("No se pudo obtener datos de Meteogalicia");

        var pred = (data as JObject)?["predConcello"];
        if (pred == null)
            throw new Exception("No se pudo obtener datos de Meteogalicia");
        if (!pred.HasValues)
            throw new Exception("No se encuentra el municipio proporcionado");

        var dias = pred?["listaPredDiaConcello"] as JArray;

        #region Obtener Días

        for (int i = 0; i < 3; i++)
        {
            var dia = dias?[i];
            if (dia == null)
                continue;

            result.Add(new DiaPrediccion
            {
                TMax = dia["tMax"]?.Value<int>() ?? -1,
                TMin = dia["tMin"]?.Value<int>() ?? -1,
                Fecha = dia["dataPredicion"]?.Value<string>() ?? String.Empty,

                Cielo = new Turno
                {
                    Manana = MapeoEstado(dia["ceo"]?["manha"]?.Value<int>() ?? -1),
                    Tarde = MapeoEstado(dia["ceo"]?["tarde"]?.Value<int>() ?? -1),
                    Noche = MapeoEstado(dia["ceo"]?["noite"]?.Value<int>() ?? -1)
                },

                Lluvia = new ()
                {
                    { "Mañana", dia["pchoiva"]?["manha"]?.Value<int>() ?? -1 },
                    { "Tarde", dia["pchoiva"]?["tarde"]?.Value<int>() ?? -1},
                    { "Noche", dia["pchoiva"]?["noite"]?.Value<int>() ?? -1}
                }
            });
        }

        #endregion

        return new MeteoResponse { Dias = result };
    }

    private readonly Dictionary<int, string> EstadoMap = new()
    {
        { -9999, "Non dispoñible" },
        { 101, "Despexado" },
        { 102, "Nubes" },
        { 103, "Nubes e claros" },
        { 104, "Anubrado 75%" },
        { 105, "Cuberto" },
        { 106, "Néboas" },
        { 107, "Chuvasco" },
        { 108, "Chuvasco 75%" },
        { 109, "Chuvasco neve" },
        { 110, "Orballo" },
        { 111, "Choiva" },
        { 112, "Neve" },
        { 113, "Treboada" },
        { 114, "Brétema" },
        { 115, "Bancos de néboa" },
        { 116, "Nubes medias" },
        { 117, "Choiva débil" },
        { 118, "Chuvascos débiles" },
        { 119, "Treboada con poucas nubes" },
        { 120, "Auga neve" },
        { 121, "Sarabia" },
    };

    /// <summary>
    /// Mapea los datos de la API con información propia
    /// </summary>
    /// <param name="codigo"></param>
    /// <returns> String referente al diccionario EstadoMap </returns>
    private string MapeoEstado(int codigo)
    {
        return 
            !EstadoMap.TryGetValue(codigo, out string? estatus) ? $"Desconocido ({codigo})" : estatus;
    }
}

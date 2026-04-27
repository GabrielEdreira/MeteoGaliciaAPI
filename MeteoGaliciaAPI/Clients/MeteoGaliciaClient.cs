namespace MeteoGaliciaAPI.Clients;

public class MeteoGaliciaClient
{
    private readonly HttpClient _httpClient;
    private const string BaseUrl = "https://servizos.meteogalicia.gal/mgrss/predicion/jsonPredConcellos.action";

    public MeteoGaliciaClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> GetDataAsync(string municipio)
    {
        if (String.IsNullOrWhiteSpace(municipio))
            return "El valor del municipio no debe ser nulo.";

        var url = $"{BaseUrl}?idConc={municipio}";

        return 
            await _httpClient.GetStringAsync(url);
    }
}


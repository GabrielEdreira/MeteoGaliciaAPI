namespace MeteoGaliciaAPI.Models;

public class MeteoResponse
{
    public List<DiaPrediccion> Dias { get; set; }

}
public class DiaPrediccion
{
    public string Fecha { get; set; }
    public int TMax { get; set; }
    public int TMin { get; set; }
    public Turno Cielo { get; set; }
    public Dictionary<string, int> Lluvia { get; set; }
}

public class Turno
{
    public String Manana { get; set; }
    public String Tarde { get; set; }
    public String Noche { get; set; }
}
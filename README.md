# MeteoGaliciaAPI

API REST desarrollada en .NET que expone un único endpoint para la obtención de datos meteorológicos de un Concello de Galicia apoyado en el API de MeteoGalicia.

---

## Cómo ejecutar el proyecto

1. Clonar el repositorio:

```bash
git clone https://github.com/GabrielEdreira/MeteoGaliciaAPI.git
cd MeteoGaliciaAPI
```

2. Restaurar dependencias:

```bash
dotnet restore
```

3. Ejecutar la aplicación:
```bash
dotnet run
```

## Cómo probar la API

El proyecto tiene un archivo `.http` incluido con una serie de ejemplos

## Endpoint

GET/api/observacion/{municipio} 
**Parámetros:**
  - `municipio` (int) => Identificador del municipio
  
Devuelve datos meteorológicos del municipio seleccionado.

Listado de municipios disponible en: https://meteo-estaticos.xunta.gal/datosred/infoweb/meteo/docs/rss/JSON_Pred_MPrazo_gl.pdf

### Respuesta Correcta 

```json
{
  "dias": [
    {
      "fecha": "fecha",
      "tMax": -1,
      "tMin": -1,
      "cielo": {
        "manana": "cielo_mañana",
        "tarde": "cielo_tarde",
        "noche": "cielo_noche"
      },
      "lluvia": {
        "Mañana": -1,
        "Tarde": -1,
        "Noche": -1
      }
    },
    ...
  ]
}
```
### Respuesta Errónea 

```json
{
  "error": "No se encuentra el municipio proporcionado"
}
```

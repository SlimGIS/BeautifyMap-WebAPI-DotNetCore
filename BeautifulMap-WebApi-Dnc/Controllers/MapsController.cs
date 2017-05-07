using Microsoft.AspNetCore.Mvc;
using SlimGis.MapKit.Geometries;
using SlimGis.MapKit.Layers;
using SlimGis.MapKit.Symbologies;
using SlimGis.MapKit.WebApi;

namespace BeautifulMap_WebApi_Dnc.Controllers
{
    [Route("api/[controller]")]
    public class MapsController : Controller
    {
        private static MapModelManager mapModelManager = new MapModelManager();

        // GET api/values
        [HttpGet("{theme}/{z}/{x}/{y}")]
        public IActionResult Get(string theme, int z, int x, int y)
        {
            //MapModel mapModel = mapModelManager.Dequeue(theme, () =>
            //{
            //    string themeFilename = char.ToUpper(theme[0]) + theme.Substring(1);
            //    CssDocument cssDoc = CssDocument.Load($@"Themes\{themeFilename}Theme.sgcss");
            //    LayerGroup layerGroup = new LayerGroup();
            //    layerGroup.LoadStyledLayers("AppData", cssDoc);

            //    MapModel tempMapModel = new MapModel(GeoUnit.Meter);
            //    tempMapModel.Layers.AddRange(layerGroup.Layers);
            //    return tempMapModel;
            //});
            //return new XyzTileResult(mapModel, x, y, z);

            string themeFilename = char.ToUpper(theme[0]) + theme.Substring(1);
            CssDocument cssDoc = CssDocument.Load($@"Themes\{themeFilename}Theme.sgcss");
            LayerGroup layerGroup = new LayerGroup();
            layerGroup.LoadStyledLayers("AppData", cssDoc);

            MapModel tempMapModel = new MapModel(GeoUnit.Meter);
            tempMapModel.Layers.AddRange(layerGroup.Layers);
            return new XyzTileResult(tempMapModel, x, y, z);
        }

        [HttpGet("{theme}/css")]
        public IActionResult Get(string theme)
        {
            string themeFilename = char.ToUpper(theme[0]) + theme.Substring(1);
            themeFilename = $@"Themes\{themeFilename}Theme.sgcss";

            if (!System.IO.File.Exists(themeFilename)) return NotFound(theme);
            return this.Content(System.IO.File.ReadAllText(themeFilename));
        }
    }
}

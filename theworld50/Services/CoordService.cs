using Microsoft.Framework.Logging;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;


namespace theworld50.Services
{
    public class CoordService
    {
        private ILogger<CoordService> logger;

        public CoordService(ILogger<CoordService> logger)
        {
            this.logger = logger;
        }

        public async Task<CordServiceResult> Lookup(string location)
        {
            var result = new CordServiceResult
            {
                Success = false,
                Message = "Failed"
            };

            var url = string.Format("http://maps.googleapis.com/maps/api/geocode/xml?address={0}&sensor=false", System.Net.WebUtility.UrlEncode(location));

            var client = new HttpClient();

            var xml = await client.GetStringAsync(url);

            var xdoc = XDocument.Parse(xml);

            var resultElement = xdoc.Element("GeocodeResponse").Element("result");
            var locationElement = resultElement.Element("geometry").Element("location");

            result = new CordServiceResult
            {
                Success = true,
                Message = "Success",
                Longitude = (double)locationElement.Element("lng"),
                Latitude = (double)locationElement.Element("lat")
            };

            return result;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Text.Json;
using System.Threading.Tasks;

namespace Tour_Planner.BL {
    public class MapCreator {
        public async Task<string> CreateMap(string from, string to) {
            var key = "puPOsmfIq48rX6ia0nDeC5VBwr8wX3Po";

            var url = $"http://www.mapquestapi.com/directions/v2/route?key={key}&from={from}&to={to}";

            using var client = new HttpClient();
            var response = await client.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            var rootNode = JsonNode.Parse(content);
            Console.WriteLine(rootNode?.ToJsonString(new JsonSerializerOptions { WriteIndented = true }));

            var sessionId = rootNode["route"]["sessionId"].ToString();
            var boundingBox = rootNode["route"]["boundingBox"];
            var ul_lat = boundingBox["ul"]["lat"].ToString();
            var ul_lng = boundingBox["ul"]["lng"].ToString();
            var lr_lat = boundingBox["lr"]["lat"].ToString();
            var lr_lng = boundingBox["lr"]["lng"].ToString();

            url = $"http://www.mapquestapi.com/staticmap/v5/map?key={key}&session={sessionId}&boundingBox={ul_lat},{ul_lng},{lr_lat},{lr_lng}&size=800,600";
            var stream = await client.GetStreamAsync(url);
            var filename = $"map_{from}To{to}.png";
            await using var fileStream = new FileStream("map_Walbrzych2Wien.png", FileMode.Create, FileAccess.Write);
            stream.CopyTo(fileStream);
            return filename;
        }
    }
}

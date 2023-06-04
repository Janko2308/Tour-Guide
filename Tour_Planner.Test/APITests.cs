using NuGet.Frameworks;
using NUnit.Framework.Constraints;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json.Nodes;
using System.Text.Json;
using System.Threading.Tasks;
using Tour_Planner.BL;
using Tour_Planner.DAL;
using Tour_Planner.Model;
using Tour_Planner.Model.Structs;

namespace Tour_Planner.Test {
    public class APITests {
        [SetUp]
        public void Setup() {
        }

        // Assess whether MapQuestAPI works as it should
        [Test]
        public async Task MapQuestAPITest() {
            // Arrange
            TourCreation tc = new TourCreation();

            // Act
            tc = CreateMap("Vienna", "Budapest", Model.Enums.Transport.pedestrian);

            // Assert
            // check if tc.distance is larger than 0, tc.estimatedtime is larger than (0,0,0) and tc.mapimage exists
            Assert.That(tc.Distance, Is.GreaterThan(0));
            Assert.That(tc.EstimatedTime, Is.GreaterThan(new TimeSpan(0, 0, 0)));
            Assert.That(tc.Picture, Is.Not.Null);
        }

        // Assess if API fails upon wrong input
        [Test]
        public void MapQuestAPIFailTest() {
            // Arrange
            TourCreation tc = new TourCreation();
            
            // Act
            tc = CreateMap("adgssssssss", "712 7 78dbcxm", Model.Enums.Transport.shortest);

            // Assert
            Assert.IsTrue(tc.Distance == 0 && tc.EstimatedTime == new TimeSpan(0,0,0) && tc.Picture == null);
        }

        public TourCreation CreateMap(string from, string to, Model.Enums.Transport transportType) {
            TourCreation res = new TourCreation();
            var key = "puPOsmfIq48rX6ia0nDeC5VBwr8wX3Po";

            var transportTypeString = transportType.ToString();
            var url = $"http://www.mapquestapi.com/directions/v2/route?key={key}&from={from}&to={to}&unit=k&routeType={transportTypeString}";

            using var client = new HttpClient();
            var response = client.GetAsync(url).Result;
            var content = response.Content.ReadAsStringAsync().Result;
            var rootNode = JsonNode.Parse(content);

            if (rootNode["route"]["routeError"] != null) {
                res.Picture = null;
                res.EstimatedTime = new TimeSpan(0,0,0);
                res.Distance = 0;
                return res;
            }

            Console.WriteLine(rootNode?.ToJsonString(new JsonSerializerOptions { WriteIndented = true }));


            var sessionId = rootNode["route"]["sessionId"].ToString();
            var boundingBox = rootNode["route"]["boundingBox"];
            var ul_lat = boundingBox["ul"]["lat"].ToString();
            var ul_lng = boundingBox["ul"]["lng"].ToString();
            var lr_lat = boundingBox["lr"]["lat"].ToString();
            var lr_lng = boundingBox["lr"]["lng"].ToString();
            var distance = rootNode["route"]["distance"].ToString();
            var estTime = rootNode["route"]["formattedTime"].ToString();

            distance = distance.Replace(".", ",");
            res.Distance = Convert.ToDouble(distance);

            string[] timeArray = estTime.Split(':');
            int days = 0;
            int hours = Convert.ToInt32(timeArray[0]);
            int minutes = Convert.ToInt32(timeArray[1]);
            int seconds = Convert.ToInt32(timeArray[2]);

            while (hours > 23) {
                hours -= 24;
                days++;
            }
            res.EstimatedTime = new TimeSpan(days, hours, minutes, seconds);

            url = $"http://www.mapquestapi.com/staticmap/v5/map?key={key}&session={sessionId}&boundingBox={ul_lat},{ul_lng},{lr_lat},{lr_lng}&size=800,600";
            var stream = client.GetStreamAsync(url);

            byte[] bitmapData;
            using (var ms = new MemoryStream()) {
                stream.Result.CopyTo(ms);
                bitmapData = ms.ToArray();
            }

            res.Picture = bitmapData;
            return res;
        }
    }
}

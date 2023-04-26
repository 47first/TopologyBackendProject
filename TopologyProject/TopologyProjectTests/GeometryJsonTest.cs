using System.Diagnostics;
using System.Text.Json;
using TopologyProject;

namespace TopologyProjectTests
{
    [TestClass]
    public class GeometryJsonTest
    {
        [TestMethod]
        public void GeometryJsonReadWithoutExceptions()
        {
            string json = GetTestJson();

            var result = JsonSerializer.Deserialize<FeatureCollection>(json);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GeometryJsonWriteWithoutExceptions()
        {
            string json = GetTestJson();

            var featureCollection = JsonSerializer.Deserialize<FeatureCollection>(json);

            string result = JsonSerializer.Serialize(featureCollection);

            Assert.IsFalse(string.IsNullOrEmpty(result));
        }

        [TestMethod]
        public void SerializeDeserializeTest()
        {
            string json = GetTestJson();

            var featureCollection = JsonSerializer.Deserialize<FeatureCollection>(json);

            json = JsonSerializer.Serialize(featureCollection);

            File.WriteAllText(Path.Combine(Environment.CurrentDirectory, "result.json"), json);

            featureCollection = JsonSerializer.Deserialize<FeatureCollection>(json);

            string result = JsonSerializer.Serialize(featureCollection);

            Assert.AreEqual(result, json);
        }

        private string GetTestJson()
        {
            string[] lines = File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, "features.json"));

            return string.Join("", lines);
        }
    }
}

using System.Text.Json;
using TopologyProject;

namespace TopologyProjectTests
{
    [TestClass]
    public class GeometryJsonTest
    {
        [TestMethod]
        public void ReadWithoutExceptions()
        {
            string json = GetTestJson();

            var result = JsonSerializer.Deserialize<FeatureCollection>(json);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void WriteWithoutExceptions()
        {
            string json = GetTestJson();

            var featureCollection = JsonSerializer.Deserialize<FeatureCollection>(json);

            string result = JsonSerializer.Serialize(featureCollection);

            Assert.IsFalse(string.IsNullOrEmpty(result));
        }

        [TestMethod]
        public void IsInitialAndSerializedValuesEquals()
        {
            string json = GetTestJson();

            var featureCollection = JsonSerializer.Deserialize<FeatureCollection>(json);

            json = JsonSerializer.Serialize(featureCollection);

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

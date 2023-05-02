using static TopologyProject.Topology.GroupDistributor;

namespace TopologyProject.Topology
{
    public static class TopologyCreator
    {
        public static List<TopologyComponent> CreateTopology(this IEnumerable<Feature> features)
        {
            List<TopologyComponent> topology = new();

            foreach (var feature in features)
                topology.Add(new(feature));

            foreach (var component in topology)
                AddConnections(component, topology);

            return topology;
        }

        private static void AddConnections(TopologyComponent mainComponent, IEnumerable<TopologyComponent> topology)
        {
            var connectedFeatures = topology.Where(component =>
                component != mainComponent &&
                component.Feature.Geometry.MatchAnyCoordinate(component.Feature.Geometry.Coordinates));

            mainComponent.AddFeatureConnections(connectedFeatures);
        }
    }
}

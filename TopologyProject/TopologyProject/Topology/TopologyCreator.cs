namespace TopologyProject.Topology
{
    public static class TopologyCreator
    {
        public static List<TopologyComponent> CreateTopology(this IEnumerable<Feature> features)
        {
            List<TopologyComponent> topology = new();

            foreach (var feature in features)
                topology.Add(new(feature));

            Parallel.ForEach(topology, component => AddConnections(component, topology));

            return topology;
        }

        private static void AddConnections(TopologyComponent mainComponent, IEnumerable<TopologyComponent> topology)
        {
            Parallel.ForEach(topology, component => {
                if(component != mainComponent &&
                component.Feature.Geometry.MatchAnyCoordinate(mainComponent.Feature.Geometry.Coordinates))
                    mainComponent.AddFeatureConnection(component);
            });
        }
    }
}

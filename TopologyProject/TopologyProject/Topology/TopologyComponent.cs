namespace TopologyProject.Topology
{
    public class TopologyComponent
    {
        private List<TopologyComponent>? _childComponents;
        public Feature Feature { get; private set; }

        public TopologyComponent(Feature feature)
        {
            Feature = feature;
        }

        public bool HasAnyConnections => _childComponents is not null;

        public IEnumerable<TopologyComponent>? ConnectedComponents => _childComponents;

        public void AddFeatureConnections(IEnumerable<TopologyComponent> connectedComponents)
        {
            _childComponents ??= new();
            _childComponents.AddRange(connectedComponents);
        }
    }
}

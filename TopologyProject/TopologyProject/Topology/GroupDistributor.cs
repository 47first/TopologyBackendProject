namespace TopologyProject.Topology
{
    public class TopologySorter
    {
        private int _lastGroupId = -1;

        public void SetGroupsIn(IEnumerable<Feature> features)
        {
            var topology = CreateTopology(features);

            GroupComponents(topology);
        }

        private void GroupComponents(IEnumerable<TopologyComponent> topologyComponents)
        {
            foreach (var component in topologyComponents)
                GroupComponentAndChildren(component);
        }

        private List<TopologyComponent> CreateTopology(IEnumerable<Feature> features)
        {
            List<TopologyComponent> topology = new();

            foreach (var feature in features)
                topology.Add(new(feature));

            foreach (var component in topology)
                AddConnections(component, topology);

            return topology;
        }

        private void AddConnections(TopologyComponent mainComponent, IEnumerable<TopologyComponent> topology)
        {
            var connectedFeatures = topology.Where(component =>
                component != mainComponent &&
                component.Feature.Geometry.MatchAnyCoordinate(component.Feature.Geometry.Coordinates));

            mainComponent.AddFeatureConnections(connectedFeatures);
        }

        private void GroupComponentAndChildren(TopologyComponent mainComponent)
        {
            if (mainComponent.Feature.GroupId >= 0)
                return;

            GroupComponentAndChildren(mainComponent, GetNextGroupLevel());
        }

        private void GroupComponentAndChildren(TopologyComponent component, GroupLevel level)
        {
            component.Feature.GroupId = level.GroupId;

            if (component.HasAnyConnections == false)
                return;

            foreach (var connectedComponent in component.ConnectedComponents)
            {
                var nextLevel = GetNextLevel(level, connectedComponent);

                GroupComponentAndChildren(connectedComponent, nextLevel);
            }
        }

        private GroupLevel GetNextLevel(GroupLevel curLevel, TopologyComponent component)
        {
            if (++curLevel.InnerIndex >= 4 && component.HasAnyConnections)
                return GetNextGroupLevel();

            return curLevel;
        }

        private GroupLevel GetNextGroupLevel() => new(++_lastGroupId, 0);

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
}

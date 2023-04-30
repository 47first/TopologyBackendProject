namespace TopologyProject.Topology
{
    public class TopologySorter
    {
        public TopologySorter() { }

        public void SeparateByGroups(IEnumerable<Feature> features)
        {
            List<TopologyComponent> topology = new();

            foreach (var feature in features)
                topology.Add(new(feature));

            foreach(var component in topology)
                AddConnections(component, topology);

            GroupTopologyComponents(GroupLevel.Zero, topology.First());
        }

        private void AddConnections(TopologyComponent mainComponent, IEnumerable<TopologyComponent> topology)
        {
            var connectedFeatures = topology.Where(component =>
                component != mainComponent &&
                component.Feature.Geometry.MatchAnyCoordinate(component.Feature.Geometry.Coordinates));

            mainComponent.AddFeatureConnections(connectedFeatures);
        }

        private void GroupTopologyComponents(GroupLevel level, TopologyComponent mainComponent)
        {
            mainComponent.Feature.TopologyGroupId = level.GroupId;

            if (mainComponent.HasAnyConnections == false)
                return;

            level = GetNextLevel(level);

            foreach (var component in mainComponent.ConnectedComponents)
                GroupTopologyComponents(level, component);
        }

        private GroupLevel GetNextLevel(GroupLevel curLevel)
        {
            if (++curLevel.InnerIndex >= 4)
            {
                curLevel.GroupId++;
                curLevel.InnerIndex = 0;
            }

            return curLevel;
        }

        public class TopologyComponent
        {
            private List<TopologyComponent>? _childComponents;
            public TopologyFeature Feature { get; private set; }

            public TopologyComponent(TopologyFeature feature)
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

        public struct GroupLevel
        {
            public int GroupId;

            public int InnerIndex;

            public GroupLevel(int groupId, int innerIndex)
            {
                InnerIndex = innerIndex;
                GroupId = groupId;
            }

            public static GroupLevel Undefined => new(-1, -1);

            public static GroupLevel Zero => new(0, 0);
        }
    }
}

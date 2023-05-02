namespace TopologyProject.Topology
{
    public class GroupDistributor
    {
        private int _lastGroupId = -1;

        public void DistributeByGroups(IEnumerable<Feature> features)
        {
            var topology = features.CreateTopology();

            GroupComponents(topology);
        }

        private void GroupComponents(IEnumerable<TopologyComponent> topologyComponents)
        {
            foreach (var component in topologyComponents)
                GroupComponentAndChildren(component);
        }

        private void GroupComponentAndChildren(TopologyComponent mainComponent)
        {
            int? groupId = mainComponent.Feature.GroupId;

            if (groupId is not null && groupId >= 0)
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
    }
}

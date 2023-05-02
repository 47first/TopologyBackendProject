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

        private void GroupComponents(IEnumerable<TopologyComponent> topology)
        {
            foreach (var component in topology)
            {
                if (component is not null && IsGroupDefined(component) == false)
                    GroupComponentAndChildren(component, GetNextGroupLevel());
            }
        }

        private bool IsGroupDefined(TopologyComponent component) => component.Feature.GroupId is not null;

        private void GroupComponentAndChildren(TopologyComponent component, GroupLevel level)
        {
            if (IsGroupDefined(component))
                return;

            component.Feature.GroupId = level.GroupId;

            if(component.HasAnyConnections == false)
                return;

            foreach (var connectedComponent in component.ConnectedComponents)
            {
                if (IsGroupDefined(connectedComponent))
                    continue;

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

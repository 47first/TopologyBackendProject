namespace TopologyProject.Topology
{
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

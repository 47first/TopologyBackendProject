﻿namespace TopologyProject
{
    public class FeatureCollection
    {
        public string Type { get; set; } = "FeatureCollection";
        public List<Feature> Features { get; set; }
    }
}

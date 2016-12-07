using System;
using Core.Map;

namespace Core.Map.Pathfinding
{
    internal interface IPathFinder
    {
        EPathfindingAlgorithm Algorithm
        {
            get;
        }

        Path FindPathToDestination(IJ currentNodeIndex, IJ targetNodeIndex, MapController map);
    }

    public enum EPathfindingAlgorithm
    {
        Deikstra,
        AStar,
        DepthFirstSearch,
        BreadthFirst
    }
}


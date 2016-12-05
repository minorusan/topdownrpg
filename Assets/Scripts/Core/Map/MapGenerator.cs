using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Core.Map
{
    public enum ECellType
    {
        Walkable,
        Blocked
    }

    public class Node
    {
        public int GCost;
        public int HCost;

        public int FCost
        {
            get
            {
                return GCost + HCost;
            }
        }

        public ECellType CurrentCellType;
        public Vector3 Position;
        public IJ GridPosition;
    }

    [Serializable]
    public class IJ
    {
        public int I;
        public int J;

        public IJ(int i, int j)
        {
            I = i;
            J = j;
        }

        public override bool Equals(object obj)
        {
            return this.GetHashCode() == obj.GetHashCode();
        }

        public override int GetHashCode()
        {
            return I * 1000 + J;
        }
    }

    public class MapGenerator : MonoBehaviour
    {
        #region PRIVATE

        private NonWalkable[] _nonWalkables;
        private List<Node> _currentCellsArray;
        private Node[,] _currentNodeMatrix;
        private List<Node> _cellsInGame;
        private TerrainData _terrainData;
        private Dictionary<IJ, Node> _nodesMap = new Dictionary<IJ, Node>();

        #endregion

        public bool DoneUpdatingVerticeNodes
        {
            get;
            set;
        }

        [Header("Map settings")]
        public bool DrawDebug;

        public IJ MapDimentions;
        public Vector2 CellSize;

        public Transform StartPoint;

        #region Monobehaviour

        void Awake()
        {
            InstantiateCells();
        }

        private void OnDrawGizmos()
        {
            if (_currentCellsArray == null || !DrawDebug)
            {
                return;
            }

            foreach (var item in _currentCellsArray)
            {
                var gizmoColor = Color.white;
               
                switch (item.CurrentCellType)
                {
                    case ECellType.Blocked:
                        {
                            gizmoColor = Color.red;
                            break;
                        }
                    case ECellType.Walkable:
                        {
                            gizmoColor = Color.green;
                            break;
                        }
                    default:
                        break;
                }

                Gizmos.color = gizmoColor;
                Gizmos.DrawSphere(item.Position, 0.3f);
            }
        }

        #endregion

        #region MapGeneratorInit

        public void InstantiateCells()
        {
            if (_currentCellsArray == null)
            {
                _currentCellsArray = new List<Node>();
            }
            else
            {
                _currentCellsArray.Clear();
                _nodesMap.Clear();
            }

            _currentNodeMatrix = new Node[MapDimentions.I, MapDimentions.J];

            var currentPosition = StartPoint.position;
            for (int i = 0; i < MapDimentions.I; i++)
            {
                for (int j = 0; j < MapDimentions.J; j++)
                {
                    var instantiated = new Node();

                    currentPosition = new Vector3(currentPosition.x + CellSize.x, currentPosition.y, currentPosition.z);
                    instantiated.Position = currentPosition;
                   
                    instantiated.GridPosition = new IJ(i, j);

                    var flooredKey = new IJ(Mathf.RoundToInt(currentPosition.x), Mathf.RoundToInt(currentPosition.y));

                    _nodesMap.Add(flooredKey, instantiated);
                    _currentNodeMatrix[i, j] = instantiated;
                    _currentCellsArray.Add(instantiated);
                }
                currentPosition = new Vector3(StartPoint.localPosition.x, currentPosition.y + CellSize.y, currentPosition.z);
            }

            DefineInwalkables();
        }

       
        private void DefineInwalkables()
        {
            _nonWalkables = FindObjectsOfType<NonWalkable>();

            for (int i = 0; i < _nonWalkables.Length; i++)
            {
                foreach (var item in _currentCellsArray)
                {
                    if (_nonWalkables[i].Bounds.Contains(item.Position))
                    {
                        item.CurrentCellType = Core.Map.ECellType.Blocked;
                    }
                }
            }
        }

        #endregion

        #region MapGeneratorUtils

        IJ el = new IJ(0, 0);

        public Node GetNodeByPosition(Vector3 position)
        {
            el.I = (Mathf.RoundToInt(position.x));
            el.J = Mathf.RoundToInt(position.z);

            return _nodesMap[el];
        }

        public int GetDistance(Node nodeA, Node nodeB)
        {
            int dstX = Mathf.Abs(nodeA.GridPosition.J - nodeB.GridPosition.J);
            int dstY = Mathf.Abs(nodeA.GridPosition.I - nodeB.GridPosition.I);

            if (dstX > dstY)
                return 14 * dstY + 10 * (dstX - dstY);
            return 14 * dstX + 10 * (dstY - dstX);
        }

        #endregion
    }

    #if UNITY_EDITOR
    [CustomEditor(typeof(MapGenerator))]
    public class MapGeneratorEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            var mapGenerator = (MapGenerator)target;
            if (GUILayout.Button("Generate map"))
            {
                mapGenerator.InstantiateCells();
            }
        }
    }
    #endif
}


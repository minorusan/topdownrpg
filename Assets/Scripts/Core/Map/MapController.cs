using UnityEngine;


#if UNITY_EDITOR
using UnityEditor;
#endif

using System.Collections.Generic;
using System.Linq;


namespace Core.Map
{
	public class MapController : MonoBehaviour
	{
		#region PRIVATE

		private static MapController[] _maps;
		private NonWalkable[] _nonWalkables;
		private List<Node> _currentCellsArray;
		private Node[,] _currentNodeMatrix;
		private List<Node> _cellsInGame;
		private TerrainData _terrainData;
		private Dictionary<IJ, Node> _nodesMap = new Dictionary<IJ, Node> ();

		#endregion

		[Header ("Map settings")]
		public bool DrawDebug;

		public IJ MapDimentions;
		public Vector2 CellSize;

		public Node[,] CurrrentMapAsMatrix
		{
			get
			{
				return _currentNodeMatrix;
			}
		}

		public Node CenterNode
		{
			get
			{
				return _currentNodeMatrix [MapDimentions.I / 2, MapDimentions.J / 2];
			}
		}

		#region Monobehaviour

		void Awake ()
		{
			InstantiateCells ();
		}

		private void OnDrawGizmos ()
		{
			if (_currentCellsArray == null || !DrawDebug)
			{
				InstantiateCells ();
				return;
			}

			if (_maps == null || _maps.Length <= 0)
			{
				GetMapsOnScene ();
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
				case ECellType.Busy:
					{
						gizmoColor = Color.yellow;
						break;
					}
				default:
					break;
				}

				Gizmos.color = gizmoColor;
				Gizmos.DrawSphere (item.Position, 0.3f);
			}
		}

		#endregion

		#region MapGeneratorInit

		public void InstantiateCells ()
		{
			if (_currentCellsArray == null)
			{
				_currentCellsArray = new List<Node> ();
			}
			else
			{
				_currentCellsArray.Clear ();
				_nodesMap.Clear ();
			}

			_currentNodeMatrix = new Node[MapDimentions.I, MapDimentions.J];

			var currentPosition = transform.position;
			for (int i = 0; i < MapDimentions.I; i++)
			{
				for (int j = 0; j < MapDimentions.J; j++)
				{
					var instantiated = new Node ();

					currentPosition = new Vector3 (currentPosition.x + CellSize.x, currentPosition.y, currentPosition.z);
					instantiated.Position = currentPosition;
                   
					instantiated.GridPosition = new IJ (i, j);

					var flooredKey = new IJ (Mathf.RoundToInt (currentPosition.x), Mathf.RoundToInt (currentPosition.y));

					_nodesMap.Add (flooredKey, instantiated);
					_currentNodeMatrix [i, j] = instantiated;
					_currentCellsArray.Add (instantiated);
				}
				currentPosition = new Vector3 (transform.localPosition.x, currentPosition.y + CellSize.y, currentPosition.z);
			}

			DefineInwalkables ();
		}

		private void DefineInwalkables ()
		{
			_nonWalkables = GetComponentsInChildren<NonWalkable> ();

			for (int i = 0; i < _nonWalkables.Length; i++)
			{
				foreach (var item in _currentCellsArray)
				{
					if (_nonWalkables [i].Bounds.Contains (item.Position))
					{
						item.CurrentCellType = Core.Map.ECellType.Blocked;
					}
				}
			}
		}

		#endregion

		#region MapGeneratorUtils

		IJ el = new IJ (0, 0);

		public Node GetNodeByPosition (Vector3 position)
		{
			el.I = Mathf.RoundToInt (position.x);
			el.J = Mathf.RoundToInt (position.y);
			Node node;
			_nodesMap.TryGetValue (el, out node);
			return node;
		}

		public int GetDistance (Node nodeA, Node nodeB)
		{
			int dstX = Mathf.Abs (nodeA.GridPosition.J - nodeB.GridPosition.J);
			int dstY = Mathf.Abs (nodeA.GridPosition.I - nodeB.GridPosition.I);

			if (dstX > dstY)
				return 14 * dstY + 10 * (dstX - dstY);
			return 14 * dstX + 10 * (dstY - dstX);
		}

		Node[] _neighbours = new Node[8];

		public Node[] GetNeighbours (Node node)
		{
			if (node == null)
			{
				return null;
			}
			int iterator = 0;

			for (int x = -1; x <= 1; x++)
			{
				for (int y = -1; y <= 1; y++)
				{
					if (x == 0 && y == 0)
						continue;

					int checkX = node.GridPosition.J + x;
					int checkY = node.GridPosition.I + y;

					if (checkX >= 0 && checkX < MapDimentions.J && checkY >= 0 && checkY < MapDimentions.I)
					{
						_neighbours [iterator] = _currentNodeMatrix [checkY, checkX];
						iterator++;
					}
				}
			}

			return _neighbours;
		}

		public static MapController[] GetMapsOnScene ()
		{
			_maps = FindObjectsOfType<MapController> ();
			return _maps;
		}

		public static List<Node> TopEdgeNodesOfMap (MapController map)
		{
			if (map == null)
			{
				return new List<Node> ();
			}
			var list = map._currentCellsArray.Where (node => node.GridPosition.I == map.MapDimentions.I - 1).ToList ();
			return list;
		}

		public static List<Node> BottomEdgeNodesOfMap (MapController map)
		{
			if (map == null)
			{
				return new List<Node> ();
			}
			var list = map._currentCellsArray.Where (node => node.GridPosition.I == 0).ToList ();
			return list;
		}

		public static List<Node> LeftEdgeNodesOfMap (MapController map)
		{
			if (map == null)
			{
				return new List<Node> ();
			}
			var list = map._currentCellsArray.Where (node => node.GridPosition.J == 0).ToList ();
			return list;
		}

		public static List<Node> RightEdgeNodesOfMap (MapController map)
		{
			if (map == null)
			{
				return new List<Node> ();
			}
			var list = map._currentCellsArray.Where (node => node.GridPosition.J == map.MapDimentions.J - 1).ToList ();
			return list;
		}

		#endregion
	}

	#if UNITY_EDITOR
	[CustomEditor (typeof(MapController))]
	public class MapGeneratorEditor : Editor
	{
		public override void OnInspectorGUI ()
		{
			DrawDefaultInspector ();
			var mapGenerator = (MapController)target;
			if (GUILayout.Button ("Generate map"))
			{
				mapGenerator.InstantiateCells ();
			}
		}
	}
	#endif
}


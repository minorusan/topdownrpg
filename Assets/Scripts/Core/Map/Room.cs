using UnityEngine;
using System.Collections;
using Utils;


namespace Core.Map
{
	[RequireComponent (typeof(MapController))]
	public class Room:MonoBehaviour
	{
		private MapController _map;

		[ReadOnly]
		public RoomExit[] Exits;
		[ReadOnly]
		public Room[] LinkedRooms;

		public MapController Map
		{
			get
			{
				return _map;
			}
		}

		public void Init ()
		{
			Exits = GetComponentsInChildren <RoomExit> ();
			if (_map == null)
			{
				_map = GetComponent <MapController> ();
				_map.InstantiateCells ();

			}
			for (int i = 0; i < Exits.Length; i++)
			{
				Exits [i].Init ();
			}
		}

		private void OnEnable ()
		{
			Init ();
		}

		private void OnDrawGizmos ()
		{
			Init ();
		}
	}
}

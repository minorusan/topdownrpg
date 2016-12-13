using UnityEngine;
using System.Collections;
using Utils;
using System.Collections.Generic;
using System.Linq;


namespace Core.Map
{
	public class ChunkCreator : MonoBehaviour
	{
		private const string kPathToChunkRoomPrefabs = "Prefabs/Decorations/Chunks/{0}/Rooms/";
		private Room[] _rooms;
		private List<Room> _generatedRoomsList = new List<Room> ();

		public int RoomsCount;
		public string ChunkName;

		[ReadOnly]
		public Room[] GeneratedRooms;

		private void OnEnable ()
		{
			_rooms = Resources.LoadAll <Room> (string.Format (kPathToChunkRoomPrefabs, ChunkName));
			GenerateChunk ();
		}

		private void OnDisable ()
		{
			_generatedRoomsList.Clear ();
			for (int i = 0; i < transform.childCount; i++)
			{
				Destroy (transform.GetChild (i).gameObject);
			}
		}

		private void GenerateChunk ()
		{
			var firstRoom = Instantiate (_rooms [Random.Range (0, _rooms.Length)]);
			firstRoom.transform.parent = transform;
			firstRoom.transform.localPosition = Vector3.zero;

			firstRoom.gameObject.SetActive (true);
			firstRoom.Map.InstantiateCells ();

			_generatedRoomsList.Add (firstRoom);

			for (int i = 0; i < firstRoom.Exits.Length; i++)
			{
				if (_generatedRoomsList.Count <= RoomsCount)
				{
					GenerateRoomForAnExit (firstRoom.Exits [i]);
				}
			}

			foreach (var item in _generatedRoomsList)
			{
				item.Map.InstantiateCells ();
			}
			var nonWalkables = FindObjectsOfType <NonWalkable> ();
			for (int i = 0; i < nonWalkables.Length; i++)
			{
				nonWalkables [i].Refresh ();
			}
		}

		private void GenerateRoomForAnExit (RoomExit exit)
		{
			var prefabsList = new List<Room> (_rooms);
			var roomsThatFit = prefabsList.Where (r => r.Exits.Any (e => e.ExitSide == exit.LinksWithSide)).ToList ();

			if (roomsThatFit.Count < 1)
			{
				return;
			}

			var newNeighbour = roomsThatFit [Random.Range (0, roomsThatFit.Count)];

			var instantiatedNeighbour = Instantiate (newNeighbour);
			_generatedRoomsList.Add (instantiatedNeighbour);
			instantiatedNeighbour.transform.parent = transform;
			instantiatedNeighbour.transform.localPosition = Vector3.zero;

			instantiatedNeighbour.gameObject.SetActive (true);
			instantiatedNeighbour.Map.InstantiateCells ();

			PairExitWithRoom (exit, instantiatedNeighbour);

			var unCheckedExits = instantiatedNeighbour.Exits.Where (e => e.LinkedWith == null).ToArray ();
			for (int i = 0; i < unCheckedExits.Length; i++)
			{
				if (_generatedRoomsList.Count < RoomsCount)
				{
					GenerateRoomForAnExit (unCheckedExits [i]);
				}
			}
		}

		private void PairExitWithRoom (RoomExit exit, Room linkedRoom)
		{
			var roomExits = linkedRoom.Exits;
			var exitThatFit = roomExits.First (e => e.ExitSide == exit.LinksWithSide && e.LinkedWith == null);

			exitThatFit.LinkedWith = exit;
			exit.LinkedWith = exitThatFit;

			var yDifference = exit.transform.position.y - exitThatFit.transform.position.y;
			var xDifference = exit.transform.position.x - exitThatFit.transform.position.x;
			var adjustmentVector = new Vector3 (linkedRoom.transform.position.x + xDifference,
			                                    linkedRoom.transform.position.y + yDifference, 0);
			linkedRoom.transform.position = adjustmentVector;
		}
	}
}


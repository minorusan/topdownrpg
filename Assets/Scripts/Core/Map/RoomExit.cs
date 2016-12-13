using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEditor;
using Utils;


namespace Core.Map
{
	[InitializeOnLoadAttribute]
	[RequireComponent (typeof(BoxCollider2D))]
	public class RoomExit : MapDependentObject
	{
		#region Private

		private Bounds _bounds;
		private bool _initialised;

		#endregion

		public Bounds Bounds
		{
			get
			{
				return _bounds;
			}
		}

		[ReadOnly]
		public EExitSide ExitSide;

		[ReadOnly]
		public EExitSide LinksWithSide;
		[ReadOnly]
		public RoomExit LinkedWith;

		#region Monobehaviour

		public void Init ()
		{
			ExitSide = GetExitSide ();
			LinksWithSide = GetLinkingSide ();
		}

		private void OnEnable ()
		{
			Init ();
		}

		private void OnDrawGizmos ()
		{
			if (!_initialised)
			{
				_initialised = true;
				Init ();
			}

			Gizmos.color = Color.yellow;
			Gizmos.DrawCube (_bounds.center, _bounds.size);
		}

		private EExitSide GetLinkingSide ()
		{
			switch (ExitSide)
			{
			case EExitSide.Down:
				{
					return  EExitSide.Top;
				}
			case EExitSide.Top:
				{
					return  EExitSide.Down;
				}
			case EExitSide.Left:
				{
					return  EExitSide.Right;
				}
			default:
				{
					return  EExitSide.Left;
				}
			}
		}

		private EExitSide GetExitSide ()
		{
			_bounds = GetComponent <BoxCollider2D> ().bounds;
			if (MapController.BottomEdgeNodesOfMap (Map).Any (n => _bounds.Contains (n.Position)))
			{
				return EExitSide.Down; 
			}

			if (MapController.TopEdgeNodesOfMap (Map).Any (n => _bounds.Contains (n.Position)))
			{
				return EExitSide.Top; 
			}

			if (MapController.LeftEdgeNodesOfMap (Map).Any (n => _bounds.Contains (n.Position)))
			{
				return EExitSide.Left; 
			}

			return EExitSide.Right; 
		}

		#endregion
	}

	public enum EExitSide
	{
		Right,
		Left,
		Top,
		Down
	}
}

using UnityEngine;
using System.Collections;


namespace Core.Map
{
	[RequireComponent (typeof(BoxCollider2D))]
	public class NonWalkable : MapDependentObject
	{
		#region PRIVATE

		private Bounds _bounds;

		#endregion

		public Bounds Bounds {
			get
			{
				return _bounds;
			}
		}

		private void Awake ()
		{
			_bounds = GetComponent<BoxCollider2D> ().bounds;
		}

		protected override void Start ()
		{
			base.Start ();
			if (Map != null)
			{
				var matrix = Map.CurrrentMapAsMatrix;
				for (int i = 0; i < matrix.GetLength (0); i++)
				{
					for (int j = 0; j < matrix.GetLength (1); j++)
					{
						if (_bounds.Contains (matrix [i, j].Position))
						{
							matrix [i, j].CurrentCellType = ECellType.Blocked;
						}
					}
				}
			}
		}
	}
}


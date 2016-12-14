using UnityEngine;


namespace Core.Map
{
	[RequireComponent (typeof(BoxCollider2D))]
	public class NonWalkable : MonoBehaviour
	{
		public Bounds Bounds
		{
			get
			{
				return GetComponent<BoxCollider2D> ().bounds;
			}
		}
	}
}


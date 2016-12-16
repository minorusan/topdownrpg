using UnityEngine;
using System.Collections;


namespace Core.Inventory
{
	[RequireComponent (typeof(BoxCollider2D))]
	public class Trap : MonoBehaviour
	{
		private TrapItemBase _selfTrap;
		public string TrapId;

		private void Start ()
		{
			_selfTrap = ItemsData.GetTrapById (TrapId);
		}

		private void OnTriggerEnter2D (Collider2D col)
		{
			if (col.tag != "Player")
			{
				_selfTrap.TrapAction (col.gameObject);
				gameObject.SetActive (false);
			}
		}
	}

}

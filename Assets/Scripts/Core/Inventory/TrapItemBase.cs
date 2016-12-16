using UnityEngine;
using System.Collections;
using System;


namespace Core.Inventory
{
	public delegate void TrapAction (GameObject obj);
	public class TrapItemBase : AItemBase
	{
		private TrapAction _trapAction;
		private float _requiredTime;

		public float RequiredTime
		{
			get
			{
				return _requiredTime;
			}
		}

		public TrapAction TrapAction
		{
			get
			{
				return _trapAction;
			}
		}

		public TrapItemBase (string itemId, float requiredTime, TrapAction trapAction) : base (itemId, EItemType.Trap)
		{
			_requiredTime = requiredTime;
			_trapAction = trapAction;
		}
	}
}



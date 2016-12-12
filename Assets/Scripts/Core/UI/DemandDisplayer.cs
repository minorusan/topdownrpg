using UnityEngine;
using System.Collections;
using Core.Characters.Player.Demand;
using UnityEngine.UI;


namespace Core.UI
{
	[RequireComponent (typeof(Image))]
	public class DemandDisplayer : MonoBehaviour
	{
		#region PRIVATE

		private Image _demandBar;

		#endregion

		public DemandAffector Demand;

		#region Monobehaviour

		private void Start ()
		{
			_demandBar = GetComponent <Image> ();
		}

		private void Update ()
		{
			_demandBar.fillAmount = (float)Demand.DemandState / 100;
		}

		#endregion
	}
}


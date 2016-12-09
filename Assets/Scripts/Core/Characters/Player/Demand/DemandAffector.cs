using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngineInternal;


namespace Core.Characters.Player.Demand
{
	public class DemandAffector : MonoBehaviour
	{

		public Image Bar;

		public virtual int DemandState
		{
			get;
			protected set;
		}


		// Use this for initialization
		void Start ()
		{

		}

		// Update is called once per frame
		void Update ()
		{

		}

		#region Internal

		protected void UpdateBar ()
		{
			var coef = (float)(DemandState)Bar / 100;
			Bar.fillAmount = coef;
		}

		#endregion
	}
}



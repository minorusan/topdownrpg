using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngineInternal;


namespace Core.Characters.Player.Demand
{
	public class DemandAffector : MonoBehaviour
	{
		public static float DefaultTickTime = 2f;

		public int DemandState;
		public float DemandTickTime = 2f;

		private void Awake ()
		{
			DefaultTickTime = DemandTickTime;
		}
	}
}



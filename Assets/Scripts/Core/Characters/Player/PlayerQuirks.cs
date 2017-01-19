using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Core.Characters.Player
{
    public enum EPlayerCharachteristic
    {
        Erudition,
        Empathy,
        Reflection,
        SelfCare,
        Prowlness
    }

	public static class PlayerQuirks
	{
        private static Dictionary<EPlayerCharachteristic, int> _characteristics = new Dictionary<EPlayerCharachteristic, int>();


        static PlayerQuirks()
        {
            _characteristics.Add(EPlayerCharachteristic.Empathy, 1);
            _characteristics.Add(EPlayerCharachteristic.Erudition, 0);
            _characteristics.Add(EPlayerCharachteristic.Prowlness, 5);
            _characteristics.Add(EPlayerCharachteristic.Reflection, 2);
            _characteristics.Add(EPlayerCharachteristic.SelfCare, 5);
        }

        public static void ModifyCharachteristic(EPlayerCharachteristic characteristic, int value)
        {
            _characteristics[characteristic] = Mathf.Clamp(_characteristics[characteristic] + value, 0, 100);
        }

        public static int GetCharactheristic(EPlayerCharachteristic characteristic)
        {
            return _characteristics[characteristic];
        }

  		public static bool Hidden
		{
			get;
			set;
		}

		public static bool Attacked
		{
			get;
			set;
		}


	}
}


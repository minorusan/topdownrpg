using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;


namespace Core.Inventory
{
	public static class InventoryImagesLoader
	{
		#region Private

		private const string kConsumablesPath = "Sprites/Items/Consumables/";
		private static Dictionary<string, Sprite> _cachedImages = new Dictionary<string, Sprite> ();

		#endregion

		public static Sprite GetConsumableSprite (string spriteId)
		{
			Sprite cashedSprite;
			_cachedImages.TryGetValue (spriteId, out cashedSprite);
			if (cashedSprite == null)
			{
				var sprite = Resources.Load<Sprite> (kConsumablesPath + spriteId);
				_cachedImages.Add (spriteId, sprite);
				return sprite;
			}
			return cashedSprite;
		}
	}
}


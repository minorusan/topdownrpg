using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Utils
{
	[RequireComponent (typeof(Image))]
	public class NoiseEffect : MonoBehaviour
	{
		private Image _image;

		private void Awake ()
		{
			_image = GetComponent <Image> ();
		}

		public void ChangeOpacity (float opacity)
		{
			_image.color = new Color (1f, 1f, 1f, opacity);
		}
	}
}


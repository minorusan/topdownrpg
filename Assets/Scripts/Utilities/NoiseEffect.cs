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

		public void ChangeOpacity (float opacity)
		{
		    if (_image == null)
		    {
                _image = GetComponent<Image>();
            }
			_image.color = new Color (1f, 1f, 1f, opacity);
		}
	}
}


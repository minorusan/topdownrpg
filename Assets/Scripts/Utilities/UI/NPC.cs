using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Characters.Player;
using Core.Map;
#if UNITY_EDITOR
using UnityEditor;
#endif



namespace Utilities.UI
{
	[RequireComponent(typeof(SpriteRenderer))]
	public class NPC : MonoBehaviour
	{
	    private string kSFXPAth = "Prefabs/NPC/BlurredSFX";
		private SpriteRenderer _renderer;
	    private DayNight _dayNight;
		// Use this for initialization
		void Start()
		{
			_renderer = GetComponent<SpriteRenderer>();
		}

	    public void AddDuplicate()
	    {
            _renderer = GetComponent<SpriteRenderer>();
	        _renderer.sortingOrder = 2;
            var sfx = Instantiate(Resources.Load<GameObject>(kSFXPAth), transform);
	        sfx.GetComponent<SpriteRenderer>().sprite = _renderer.sprite;
	        sfx.transform.localPosition = Vector2.zero;
            sfx.transform.localScale = new Vector2(1.1f,1.1f);
	    }

		// Update is called once per frame
		void Update()
		{
			if(PlayerBehaviour.CurrentPlayer.isActiveAndEnabled && transform.position.y > PlayerBehaviour.CurrentPlayer.transform.position.y)
			{
				_renderer.sortingOrder = PlayerBehaviour.CurrentPlayer.Renderer.sortingOrder - 1;
			}
			else
			{
				_renderer.sortingOrder = PlayerBehaviour.CurrentPlayer.Renderer.sortingOrder + 1;
			}

		   
		}
	}

#if UNITY_EDITOR
    [CustomEditor(typeof(NPC))]
    [CanEditMultipleObjects]
    class DecalMeshHelperEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            if (GUILayout.Button("AddSFXDuplicate"))
                (target as NPC).AddDuplicate();
        }
    }
#endif
}

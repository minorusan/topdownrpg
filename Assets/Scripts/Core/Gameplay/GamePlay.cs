using UnityEngine;
using UnityEngine.SceneManagement;


namespace Core.Gameplay
{
	public class GamePlay : MonoBehaviour
	{
		public void RestartLevel ()
		{
			SceneManager.LoadSceneAsync (SceneManager.GetActiveScene ().buildIndex);
		}
	}

}

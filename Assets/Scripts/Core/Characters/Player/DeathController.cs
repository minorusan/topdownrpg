using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core.Characters.Player
{
    public class DeathController : MonoBehaviour
    {
        private Transform _latestCheckpoint;
        [Range(0, 100)]
        public int DeathPossibility = 20;


        public void Kill()
        {
            var minDeathChance = PlayerQuirks.GetCharactheristic(EPlayerCharachteristic.Empathy) +
                PlayerQuirks.GetCharactheristic(EPlayerCharachteristic.Prowlness) +
                PlayerQuirks.GetCharactheristic(EPlayerCharachteristic.Reflection);

            if (Random.Range(Mathf.Clamp(minDeathChance,0, 100), 100) > DeathPossibility)
            {
                StartCoroutine(DeathEffect());
               
            }else
            {
                SceneManager.LoadScene(0);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "CheckPoint")
            {
                _latestCheckpoint = collision.transform;
            }
        }

        private IEnumerator DeathEffect()
        {
            for (float i = 1; i >= 0; i-= 0.01f)
            {
                Time.timeScale = i;
                Camera.main.orthographicSize -= 0.01f;
                yield return new WaitForEndOfFrame();
            }
            StartCoroutine(DeathEffectContinue());
            PlayerBehaviour.CurrentPlayer.transform.position = _latestCheckpoint.position;
        }

        private IEnumerator DeathEffectContinue()
        {
            for (float i = 0; i <= 1; i += 0.01f)
            {
                Time.timeScale = i;
                Camera.main.orthographicSize += 0.01f;
                yield return new WaitForEndOfFrame();
            }
            PlayerBehaviour.CurrentPlayer.transform.position = _latestCheckpoint.position;
           
        }
    }

}
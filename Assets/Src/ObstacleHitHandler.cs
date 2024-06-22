using UnityEngine;
using UnityEngine.SceneManagement;

namespace Src
{
    public class ObstacleHitHandler : MonoBehaviour
    {
        [SerializeField] private ObstacleHitTrigger trigger;

        private void Awake()
        {
            trigger.ObstacleHit += OnHit;
        }

        private void OnHit()
        {
            SceneManager.LoadScene("Scenes/SampleScene");
        }
    }
}
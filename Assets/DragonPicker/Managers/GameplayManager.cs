using TMPro;
using UnityEngine;

namespace DragonPicker
{
    public class GameplayManager : MonoBehaviour
    {
        public static GameplayManager Instance { get; private set; }

        [SerializeField] private TextMeshProUGUI scoreLabel;

        private int score;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
        }

        public void IncreaseScore()
        {
            score++;
            scoreLabel.text = $"Score: {score}";
        }

        public void ClearEggsOnScreen()
        {
            var eggs = GameObject.FindGameObjectsWithTag("Dragon Egg");
            foreach(var egg in eggs)
            {
                egg.GetComponent<EggExplosion>().Explode();
            }
        }
    }
}

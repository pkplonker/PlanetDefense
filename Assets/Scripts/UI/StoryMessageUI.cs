using TMPro;
using UnityEngine;

namespace UI
{
    public class StoryMessageUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI senderText;
        [SerializeField] private TextMeshProUGUI messgaeText;

        public void Init(string sender, string message)
        {
            senderText.text = sender;
            messgaeText.text = message;
        }
    }
}

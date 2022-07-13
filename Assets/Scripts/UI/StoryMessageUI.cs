using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class StoryMessageUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI senderText;
        [SerializeField] private TextMeshProUGUI messageText;
        [SerializeField] private Color senderColor;
        public void Init(string sender, string message,string playername)
        {
            senderText.text = sender;
            messageText.text = message;
            if (sender.ToLower() != playername.ToLower()) return;
            var image = GetComponent<Image>();
            image.color = senderColor;
            senderText.alignment = TextAlignmentOptions.Right;
            messageText.alignment = TextAlignmentOptions.Right;

        }
    }
}

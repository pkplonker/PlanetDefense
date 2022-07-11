using UnityEngine;
using UnityEngine.UI;

namespace UI
{
	public class ShopVerticalButton : MonoBehaviour
	{
		[SerializeField] private Sprite selectedSprite;
		[SerializeField] private Sprite unselectedSprite;
		[SerializeField] private Image image;

		private void Awake() => image.sprite = unselectedSprite;
		public void Select() => image.sprite = selectedSprite;
		public void Deselect() => image.sprite = unselectedSprite;
	}
}
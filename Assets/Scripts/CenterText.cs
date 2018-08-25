using UnityEngine.UI;

public partial class Game {

	public Text centerText;

	void ShowMessage(string message) {
		centerText.text = message;
		Fade();
		centerText.gameObject.SetActive(true);
	}

	void HideMessage() {
		centerText.gameObject.SetActive(false);
		Unfade();
	}

}

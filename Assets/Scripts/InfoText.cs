using UnityEngine.UI;
using VRTK;

public partial class Game {

	public Text infoText;

	void ShowInfoText(string message) {
		infoText.text = message + "\nPress right trigger to continue.";
		infoText.gameObject.SetActive(true);
		running = false;
		rightControllerEvents.TriggerClicked += HideInfoTextHandler;
	}

	void HideInfoTextHandler(object s, ControllerInteractionEventArgs e) {
		HideInfoText();
		rightControllerEvents.TriggerClicked -= HideInfoTextHandler;
	}

	void HideInfoText() {
		infoText.gameObject.SetActive(false);
		running = true;
	}

}

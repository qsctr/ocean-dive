using UnityEngine;
using UnityEngine.UI;
using VRTK;

public partial class Game {

	public Text elevationText;

	Vector3 deltaMotion;

	void InitPlayer() {
		playAreaTransform.position = Vector3.down * 3;
	}

	void MovePlayer() {
		if (running) {
			deltaMotion = headsetTransform.forward * Time.deltaTime * 2;
			if (!inSimulator) {
				deltaMotion *= leftControllerEvents.GetTriggerAxis();
				var leftControllerY = leftController.transform.forward.y;
				if (Mathf.Abs(leftControllerY) < 0.3f) {
					deltaMotion.y = 0;
				} else {
					deltaMotion.y =
						(leftControllerY - 0.3f * Mathf.Sign(leftControllerY)) / 15;
				}
			}
			playAreaTransform.position += deltaMotion;
		} else {
			deltaMotion = Vector3.zero;
		}
	}

	void UpdateElevationText() {
		elevationText.text = string.Format(
			"Elevation: {0:0.0}", playAreaTransform.position.y);
	}

}

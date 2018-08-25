using UnityEngine;

public partial class Game {

	float oxygenLevel = 100;

	void UpdateOxygenLevel() {
		if (running) {
			oxygenLevel -= (Time.deltaTime / 3 + deltaMotion.magnitude / 2) / 3;
			if (oxygenLevel < 0) {
				Die();
			}
		}
	}

}

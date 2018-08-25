using UnityEngine;

public partial class Game {

	void EnableFog() {
		RenderSettings.fog = true;
		RenderSettings.fogColor = new Color(0.1f, 0.3f, 0.4f);
		RenderSettings.fogDensity = 0.07f;
		RenderSettings.fogMode = FogMode.Exponential;
		RenderSettings.ambientLight = new Color(0.5f, 0.5f, 0.5f);
	}

}

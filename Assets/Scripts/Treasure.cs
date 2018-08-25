using UnityEngine;
using VRTK;

public partial class Game {

	// public GameObject treasure;

	// void SetupTreasure() {
	// 	var treasureExtent = treasure.GetComponent<MeshRenderer>().bounds.extents.x;
	// 	var maxRadius = waterSurface.localScale.x - treasureExtent;
	// 	var position2d = Random.insideUnitCircle * maxRadius;
	// 	var position = new Vector3(position2d.x, 0, position2d.y);
	// 	position.y = seabedTerrain.SampleHeight(position) + seabedTerrainYPosition;
	// 	treasure.transform.position = position;
	// 	treasure.transform.Rotate(Random.Range(-25f, 0f), 0, 0);

	// 	rightControllerEvents.TriggerClicked += (s, e) => {
	// 		if (running) {
	// 			if (rightInteractTouch.GetTouchedObject() == treasure) {
	// 				points += 1000;
	// 				treasure.SetActive(false);
	// 			}
	// 		}
	// 	};
	// }

}

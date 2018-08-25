using System;
using System.Linq;
using UnityEngine.UI;

public partial class Game {

	void CheckEnd() {
		if (headsetTransform.position.y >= waterSurface.position.y) {
			waterSurface.gameObject.SetActive(false);
			EndGame();
			ShowMessage($@"You reached the surface!
Your score is {points} points.
You discovered: {String.Join(", ", discoveredFish.Select(GetFishName))}
You collected: {String.Join(", ", collected.GroupBy(GetCollectibleName,
	(name, xs) => $"{xs.Count()} {name}" + (xs.Count() > 1 ? "s" : "")))}");
		}
	}

	void Die() {
		EndGame();
		points = 0;
		ShowMessage(@"Game Over
You didn't reach the surface in time.
Better luck next time!");
	}

	void EndGame() {
		HideInfoText();
		running = false;
	}

}

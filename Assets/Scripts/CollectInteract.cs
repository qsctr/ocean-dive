using System.Collections.Generic;
using System.Linq;

public partial class Game {

	List<CollectibleType> collected = new List<CollectibleType>();

	void SetupCollectInteract() {
		rightControllerEvents.TriggerClicked += (s, e) => {
			if (running) {
				var touchedObject = rightInteractTouch.GetTouchedObject();
				if (touchedObject != null) {
					var collectible = touchedObject.GetComponent<Collectible>();
					if (collectible != null) {
						print("Collected object");
						string name = GetCollectibleName(collectible.type);
						string article = "aeiou".Contains(name[0]) ? "an" : "a";
						ShowInfoText($"You found {article} {name}!");
						points += GetCollectiblePoints(collectible.type);
						if (collectible.type == CollectibleType.OxygenTank) {
							oxygenLevel = 100;
						}
						touchedObject.SetActive(false);
						collected.Add(collectible.type);
					}
				}
			}
		};
	}

	string GetCollectibleName(CollectibleType t) {
		switch (t) {
			case CollectibleType.Anchor:
				return "anchor";
			case CollectibleType.Coral:
				return "coral";
			case CollectibleType.LifePreserver:
				return "life preserver";
			case CollectibleType.OxygenTank:
				return "oxygen tank";
			case CollectibleType.Plant:
				return "plant";
			case CollectibleType.Rock:
				return "rock";
			case CollectibleType.Treasure:
				return "treasure chest";
			case CollectibleType.Vase:
				return "vase";
			case CollectibleType.WoodPlank:
				return "wood plank";
			default:
				return null;
		}
	}

	int GetCollectiblePoints(CollectibleType t) {
		switch (t) {
			case CollectibleType.Anchor:
				return 800;
			case CollectibleType.Coral:
				return 20;
			case CollectibleType.LifePreserver:
				return 500;
			case CollectibleType.OxygenTank:
				return 0;
			case CollectibleType.Plant:
				return 20;
			case CollectibleType.Rock:
				return 10;
			case CollectibleType.Treasure:
				return 2000;
			case CollectibleType.Vase:
				return 800;
			case CollectibleType.WoodPlank:
				return 100;
			default:
				return 0;
		}
	}

}

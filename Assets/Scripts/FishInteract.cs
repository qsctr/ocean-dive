using System.Collections.Generic;

public partial class Game {

    List<FishType> discoveredFish = new List<FishType>();

    void SetupFishInteract() {
        rightPointer.DestinationMarkerEnter += (s, e) => {
            if (running) {
                var fishComponent =
                    e.target.gameObject.GetComponentInParent<Fish>();
                if (fishComponent != null) {
                    var fishType = fishComponent.type;
                    if (!discoveredFish.Contains(fishType)) {
                        points += GetPointsForFishType(fishType);
                        discoveredFish.Add(fishType);
                        ShowInfoText($@"You discovered a {GetFishName(fishType)}!
{GetFishTypeDescription(fishType)}");
                    }
                }
            }
        };
    }

    string GetFishName(FishType t) {
        switch (t) {
            case FishType.Herring:
                return "Herring";
            case FishType.Tuna:
                return "Tuna";
            case FishType.Barracuda:
                return "Barracuda";
            default:
                return null;
        }
    }

    int GetPointsForFishType(FishType t) {
        switch (t) {
            case FishType.Herring:
                return 300;
            case FishType.Tuna:
                return 500;
            case FishType.Barracuda:
                return 700;
            default:
                return 0;
        }
    }

    string GetFishTypeDescription(FishType t) {
        switch (t) {
            case FishType.Herring:
                return "Herring are a type of small forage fish that travel in large schools. They usually live in shallow, temperate waters near the coast, and can are most commonly found in the North Atlantic and North Pacific Oceans. Their sizes can vary between species; the most common Atlantic herring can grow up to 45.0 centimeters in length.";
            case FishType.Tuna:
                return "Tuna are a type of saltwater commercial fish related to the mackerel. While specific traits vary largely across species, biologically, tuna are unique for being able to maintain body temperatures higher than the surrounding water. Tuna migrate over long distances and thus can be found in most parts of the world, whether in tropical, subtropical, or even cooler waters. Tuna feed on everything from zooplankton to other bony fish.";
            case FishType.Barracuda:
                return "Barracuda are a type of predatory fish known for their snake-like appearance. They prey on other fish species, such as herrings, smaller tunas, and snappers, with their large jaws and high burst speeds of up to 27 mph. Barracudas typically live solitarily, and are found in tropical and subtropical areas including the Caribbean Sea, Red Sea, and parts of the Atlantic and Pacific Oceans.";
            default:
                return null;
        }
    }

}

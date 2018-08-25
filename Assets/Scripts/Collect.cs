using System.Collections.Generic;
using UnityEngine;
using VRTK;

public partial class Game {

    public GameObject anchorPrefab;
    public GameObject coralPrefab;
    public GameObject lifePreserverPrefab;
    public GameObject oxygenTankPrefab;
    public GameObject plantPrefab;
    public GameObject rock1Prefab;
    public GameObject rock2Prefab;
    public GameObject rock3Prefab;
    public GameObject rock4Prefab;
    public GameObject rock5Prefab;
    public GameObject treasurePrefab;
    public GameObject vasePrefab;
    public GameObject woodPlankPrefab;

    void SetupCollectibles() {
        SetupCollectible(anchorPrefab, CollectibleType.Anchor, false);
        SetupCollectible(lifePreserverPrefab, CollectibleType.LifePreserver,
			false);
        SetupCollectible(oxygenTankPrefab, CollectibleType.OxygenTank, false);
        for (int i = 0; i < 8; i++) {
            SetupCollectible(rock1Prefab, CollectibleType.Rock, false);
            SetupCollectible(rock2Prefab, CollectibleType.Rock, false);
            SetupCollectible(rock3Prefab, CollectibleType.Rock, false);
            SetupCollectible(rock5Prefab, CollectibleType.Rock, false);
        }
        for (int i = 0; i < 3; i++) {
            SetupCollectible(rock4Prefab, CollectibleType.Rock, false);
        }
        for (int i = 0; i < 20; i++) {
            SetupCollectible(coralPrefab, CollectibleType.Coral, false);
            SetupCollectible(plantPrefab, CollectibleType.Plant, true);
        }
        SetupCollectible(treasurePrefab, CollectibleType.Treasure, true);
        SetupCollectible(vasePrefab, CollectibleType.Vase, false);
        for (int i = 0; i < 5; i++) {
            SetupCollectible(woodPlankPrefab, CollectibleType.WoodPlank, false);
        }
    }

    void SetupCollectible(GameObject prefab, CollectibleType type, bool yIsUp) {
        var extents = prefab.GetComponentInChildren<MeshRenderer>()
            .bounds.extents;
        var maxExtent = Mathf.Sqrt(
            Mathf.Pow(extents.x, 2) + Mathf.Pow(extents.z, 2));
        var maxRadius = waterSurface.localScale.x - maxExtent;
        var position2d = Random.insideUnitCircle * maxRadius;
        var position = new Vector3(position2d.x, 0, position2d.y);
        position.y = seabedTerrain.SampleHeight(position)
            + seabedTerrainYPosition;
        var instance = Instantiate(prefab, position, prefab.transform.rotation);
        if (yIsUp) {
            instance.transform.Rotate(Random.Range(-25f, 25f),
                Random.Range(-180f, 180f), 0);
        } else {
            instance.transform.Rotate(Random.Range(-25f, 25f),
                0, Random.Range(-180f, 180f));
        }
        var interactableObject =
            instance.AddComponent<VRTK_InteractableObject>();
        interactableObject.touchHighlightColor = highlightColor;
        interactableObject.allowedTouchControllers =
            VRTK_InteractableObject.AllowedController.RightOnly;
        var haptics = instance.AddComponent<VRTK_InteractHaptics>();
        haptics.strengthOnTouch = 0.5f;
        haptics.durationOnTouch = 0.2f;
        var collectible = instance.AddComponent<Collectible>();
        collectible.type = type;
    }

}

using UnityEngine;

public partial class Game : MonoBehaviour {

    bool running = false;

    void Start() {
        AddSetupChangedListener();
        EnableFog();
        ZeroGravity();
        InitOxygenBar();
        GenerateSeabed();
        SetupCollectibles();
        SetupCollectInteract();
        PopulateFlocks();
        SetupFishInteract();
    }

    void OnFindReady() {
        CheckStartInSimulator();
        InitStart();
        InitUI();
        InitPlayer();
    }

    void Update() {
        if (!Find()) return;
        MovePlayer();
        UpdateElevationText();
        CheckEnd();
        UpdateOxygenLevel();
        UpdateOxygenBar();
        UpdatePointsText();
        UpdateFlocks();
    }

}

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public partial class Game {

    public GameObject herringPrefab;
    public GameObject tunaPrefab;
    public GameObject barracudaPrefab;

    public RuntimeAnimatorController tunaAnimatorController;
    public RuntimeAnimatorController barracudaAnimatorController;

    List<Fish> herringFlock1;
    List<Fish> herringFlock2;
    List<Fish> tunaFlock1;
    List<Fish> tunaFlock2;
    List<Fish> barracudaFlock;

    GameObject GetFishPrefab(FishType t) {
        switch (t) {
            case FishType.Herring:
                return herringPrefab;
            case FishType.Tuna:
                return tunaPrefab;
            case FishType.Barracuda:
                return barracudaPrefab;
            default:
                return null;
        }
    }

    RuntimeAnimatorController GetFishAnimatorController(FishType t) {
        switch (t) {
            case FishType.Tuna:
                return tunaAnimatorController;
            case FishType.Barracuda:
                return barracudaAnimatorController;
            default:
                return null;
        }
    }

    void PopulateFlocks() {
        PopulateFlock(ref herringFlock1, FishType.Herring, false, new Fish.Settings {
            maxSpeed = 5,
            maxForce = 0.05f,
            separationRadius = 1,
            neighborRadius = 5,
            separationWeight = 0.2f,
            alignmentWeight = 1,
            cohesionWeight = 1
        }, 50, 3);
        PopulateFlock(ref herringFlock2, FishType.Herring, false, new Fish.Settings {
            maxSpeed = 5,
            maxForce = 0.05f,
            separationRadius = 1,
            neighborRadius = 5,
            separationWeight = 0.2f,
            alignmentWeight = 1,
            cohesionWeight = 1
        }, 50, 3);
        PopulateFlock(ref tunaFlock1, FishType.Tuna, true, new Fish.Settings {
            maxSpeed = 5,
            maxForce = 0.05f,
            separationRadius = 1,
            neighborRadius = 5,
            separationWeight = 0.2f,
            alignmentWeight = 1,
            cohesionWeight = 1
        }, 100, 3);
        PopulateFlock(ref tunaFlock2, FishType.Tuna, true, new Fish.Settings {
            maxSpeed = 5,
            maxForce = 0.05f,
            separationRadius = 1,
            neighborRadius = 5,
            separationWeight = 0.2f,
            alignmentWeight = 1,
            cohesionWeight = 1
        }, 100, 3);
        PopulateFlock(ref barracudaFlock, FishType.Barracuda, true, new Fish.Settings {
            maxSpeed = 8,
            maxForce = 0.05f,
            separationRadius = 10,
            neighborRadius = 20,
            separationWeight = 2,
            alignmentWeight = 1,
            cohesionWeight = 1
        }, 20, 20);
    }

    void PopulateFlock(ref List<Fish> flock, FishType fishType, bool addMeshCollider,
        Fish.Settings settings, int fishCount, int initialFlockRadius) {
        flock = new List<Fish>(fishCount);
        var fishPrefab = GetFishPrefab(fishType);
        var maxRadius = waterSurface.localScale.x - initialFlockRadius;
        var center2d = Random.insideUnitCircle * maxRadius;
        var minHeight = seabedTerrainYPosition + seabedTerrainHeight + initialFlockRadius;
        var centerY = Random.Range(minHeight, -initialFlockRadius);
        var center = new Vector3(center2d.x, centerY, center2d.y);
        for (var i = 0; i < fishCount; i++) {
            var position = center + Random.insideUnitSphere * initialFlockRadius;
            var fishGameObject = Instantiate(fishPrefab, position, Quaternion.identity);
            if (addMeshCollider) {
                var fishBody = fishGameObject.transform.GetChild(1).gameObject;
                var meshCollider = fishBody.AddComponent<MeshCollider>();
                meshCollider.sharedMesh =
                    fishBody.GetComponent<SkinnedMeshRenderer>().sharedMesh;
            }
            var animator = fishGameObject.GetComponent<Animator>();
            if (animator != null) {
                animator.runtimeAnimatorController = GetFishAnimatorController(fishType);
            }
            var fish = fishGameObject.AddComponent<Fish>();
            fish.settings = settings;
            fish.type = fishType;
            flock.Add(fish);
        }
    }

    void UpdateFlocks() {
        UpdateFlock(herringFlock1);
        UpdateFlock(herringFlock2);
        UpdateFlock(tunaFlock1);
        UpdateFlock(tunaFlock2);
        UpdateFlock(barracudaFlock);
    }

    void UpdateFlock(List<Fish> flock) {
        foreach (var fish in flock) {
            fish.Calculate(flock, waterSurface.localScale.x);
        }
        foreach (var fish in flock) {
            fish.Write();
        }
    }

}

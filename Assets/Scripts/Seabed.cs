using System;
using System.Linq;
using UnityEngine;

using Random = UnityEngine.Random;

public partial class Game {

	const int seabedGridSize = 257; // should be 2^n + 1
	const float initialSeabedHeightVariation = 0.5f;
	const float seabedHeightVariationDivide = 2;
	const float seabedTerrainSize = 256;
	const float seabedTerrainHeight = 40;
	const float seabedTerrainYPosition = -80;

	public Texture2D seabedFlatTexture;
	public Texture2D seabedSteepTexture;

	Terrain seabedTerrain;

    void GenerateSeabed() {
		GenerateSeabedTerrain(GenerateSeabedGrid());
	}

	float[,] GenerateSeabedGrid() {
		var grid = new float[seabedGridSize, seabedGridSize];
		var x = seabedGridSize - 1;
		grid[0, 0] = Random.value;
		grid[x, 0] = Random.value;
		grid[0, x] = Random.value;
		grid[x, x] = Random.value;
		Func<int, int, float?> getGrid = (r, c) => {
			if (r >= 0 && r < seabedGridSize && c >= 0 && c < seabedGridSize) {
				return grid[r, c];
			}
			return null;
		};
		for (var heightVariation = initialSeabedHeightVariation; x > 1;
		x /= 2, heightVariation /= seabedHeightVariationDivide) {
			Func<float, float> addVariation = average =>
				Mathf.Clamp01(average + Random.Range(-heightVariation, heightVariation));
			for (var r = x / 2; r < grid.GetLength(0); r += x) {
				for (var c = x / 2; c < grid.GetLength(1); c += x) {
					grid[r, c] = addVariation((
						grid[r - x / 2, c - x / 2] +
						grid[r + x / 2, c - x / 2] +
						grid[r - x / 2, c + x / 2] +
						grid[r + x / 2, c + x / 2]
					) / 4);
				}
			}
			for (var r = 0; r < grid.GetLength(0); r += x / 2) {
				for (var c = r % x == 0 ? x / 2 : 0; c < grid.GetLength(1); c += x) {
					grid[r, c] = addVariation(new [] {
						getGrid(r - x / 2, c),
						getGrid(r + x / 2, c),
						getGrid(r, c - x / 2),
						getGrid(r, c + x / 2)
					}.Average().Value);
				}
			}
		}
		return grid;
	}

	void GenerateSeabedTerrain(float[,] grid) {
		var terrainData = new TerrainData {
			heightmapResolution = seabedGridSize,
			alphamapResolution = seabedGridSize,
			splatPrototypes = new[] {
				new SplatPrototype { texture = seabedFlatTexture },
				new SplatPrototype { texture = seabedSteepTexture }
			},
			size = new Vector3(seabedTerrainSize, seabedTerrainHeight, seabedTerrainSize)
		};
		terrainData.SetHeights(0, 0, grid);
		terrainData.RefreshPrototypes();
		var splatMap = new float[terrainData.alphamapHeight, terrainData.alphamapWidth, 2];
		for (var y = 0; y < splatMap.GetLength(0); y++) {
			var normY = (float) y / (splatMap.GetLength(0) - 1);
			for (var x = 0; x < splatMap.GetLength(1); x++) {
				var normX = (float) x / (splatMap.GetLength(1) - 1);
				var steepness = terrainData.GetSteepness(normX, normY) / 90;
				splatMap[x, y, 0] = 1 - steepness;
				splatMap[x, y, 1] = steepness;
			}
		}
		terrainData.SetAlphamaps(0, 0, splatMap);
		var seabedObject = Terrain.CreateTerrainGameObject(terrainData);
		seabedObject.transform.position = new Vector3(-seabedTerrainSize / 2,
			seabedTerrainYPosition, -seabedTerrainSize / 2);
		seabedTerrain = seabedObject.GetComponent<Terrain>();
		seabedTerrain.Flush();
	}

}

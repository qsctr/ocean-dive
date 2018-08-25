using System.Linq;
using UnityEngine;

public class FlipBoundary : MonoBehaviour {

	void Start() {
		var mesh = GetComponent<MeshFilter>().mesh;
		mesh.triangles = mesh.triangles.Reverse().ToArray();
	}

}

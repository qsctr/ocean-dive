using UnityEngine;

public partial class Game {

    public Canvas canvas;

    void InitUI() {
        canvas.worldCamera = Camera.main;
        canvas.planeDistance = 1;
    }

}

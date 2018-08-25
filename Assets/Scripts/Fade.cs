using UnityEngine;
using UnityEngine.UI;
using VRTK;

public partial class Game {

    public Image background;

    void Fade() {
        background.enabled = true;
    }

    void Unfade() {
        background.enabled = false;
    }

}

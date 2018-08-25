using UnityEngine.UI;

public partial class Game {

    int points = 0;

    public Text pointsText;

    void UpdatePointsText() {
        pointsText.text = points + " points";
    }

}
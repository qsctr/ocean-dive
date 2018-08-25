using UnityEngine;
using UnityEngine.UI;

public partial class Game {

    public GameObject oxygenBar;

    Image oxygenBarImage;
    RectTransform oxygenBarTransform;
    float initialOxygenBarWidth;

    void InitOxygenBar() {
        oxygenBarImage = oxygenBar.GetComponent<Image>();
        oxygenBarTransform = (RectTransform) oxygenBar.transform;
        initialOxygenBarWidth = oxygenBarTransform.sizeDelta.x;
    }

    void UpdateOxygenBar() {
        oxygenBarImage.color = new Color(
            Mathf.Clamp01((100 - oxygenLevel) / 50),
            Mathf.Clamp01(oxygenLevel / 50),
            0
        );
        var sizeDelta = oxygenBarTransform.sizeDelta;
        sizeDelta.x = initialOxygenBarWidth * (oxygenLevel / 100);
        oxygenBarTransform.sizeDelta = sizeDelta;
        if (inSimulator) {
            print(oxygenLevel);
        }
    }

}

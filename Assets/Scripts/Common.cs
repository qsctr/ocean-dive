using UnityEngine;
using VRTK;

public partial class Game {

    public Transform waterSurface;

    public VRTK_ControllerEvents leftControllerEvents;
    public VRTK_ControllerEvents rightControllerEvents;
    public VRTK_ControllerHighlighter leftControllerHighlighter;
    public VRTK_ControllerHighlighter rightControllerHighlighter;
    public VRTK_InteractTouch rightInteractTouch;
    public VRTK_Pointer rightPointer;

    readonly Color highlightColor = new Color(255, 216, 0);

}

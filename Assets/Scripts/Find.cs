using UnityEngine;
using VRTK;

public partial class Game {

    Transform playAreaTransform;
    Transform headsetTransform;
    GameObject leftController;
    GameObject rightController;

    bool findReady = false;

    bool Find() {
        if (findReady) return true;
        playAreaTransform = VRTK_DeviceFinder.PlayAreaTransform();
        headsetTransform = VRTK_DeviceFinder.HeadsetTransform();
        leftController = VRTK_DeviceFinder.GetControllerLeftHand();
        rightController = VRTK_DeviceFinder.GetControllerRightHand();
        findReady = playAreaTransform && headsetTransform && leftController && rightController;
        if (findReady) {
            OnFindReady();
        }
        return findReady;
    }

}

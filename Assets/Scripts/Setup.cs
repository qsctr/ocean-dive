using System;
using VRTK;

public partial class Game {

    bool startInSimulator;
    bool inSimulator;

    void CheckStartInSimulator() {
        var setup = VRTK_SDKManager.instance.loadedSetup;
        if (setup != null) {
            startInSimulator =
                setup.systemSDKInfo.type == typeof(SDK_SimSystem);
        }
        print(setup);
    }

    void AddSetupChangedListener() {
        VRTK_SDKManager.instance.LoadedSetupChanged += (s, e) => {
            inSimulator =
                e.currentSetup?.systemSDKInfo.type == typeof(SDK_SimSystem);
        };
    }

}

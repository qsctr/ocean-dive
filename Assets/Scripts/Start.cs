using UnityEngine;
using UnityEngine.UI;
using VRTK;
using ControllerElements = VRTK.SDK_BaseController.ControllerElements;

public partial class Game {

	bool rightControllerTriggerHighlighted;

    void InitStart() {
		if (startInSimulator) {
			StartGame();
		} else {
			rightControllerHighlighter.HighlightElement(ControllerElements.Trigger,
				highlightColor);
			ShowMessage(@"Welcome!
You will go on a dive in the ocean.
Look down at your controllers. Your right hand trigger button is highlighted.
Press that button to continue.");
			var step = 0;
			rightControllerEvents.TriggerClicked += (s, e) => {
				switch (step) {
					case 0:
						rightControllerHighlighter.UnhighlightElement(
							ControllerElements.Trigger);
						leftControllerHighlighter.HighlightController(
							highlightColor);
						centerText.text = @"To move up and down, point your left controller.
If it is pointed upwards, you will float.
If it is pointed downwards, you will sink.
Press your right trigger to continue.";
						break;
					case 1:
						leftControllerHighlighter.UnhighlightController();
						leftControllerHighlighter.HighlightElement(
							ControllerElements.Trigger,
							highlightColor);
						centerText.text = @"Your left trigger button is highlighted.
To move forward in the direction that you are looking, hold your left trigger button.
Press your right trigger to continue.";
						break;
					case 2:
						leftControllerHighlighter.UnhighlightElement(
							ControllerElements.Trigger);
						rightControllerHighlighter.HighlightElement(
							ControllerElements.Touchpad,
							highlightColor);
						centerText.text = @"You will see fish in the ocean.
Your right touchpad is highlighted.
In the game, press and hold your right touchpad to activate your beam.
When the beam crosses a fish, you can learn more about the fish, and get points.
Press your right trigger to continue.";
						break;
					case 3:
						rightControllerHighlighter.UnhighlightElement(
							ControllerElements.Touchpad);
						rightControllerHighlighter.HighlightElement(
							ControllerElements.Trigger,
							highlightColor);
						centerText.text = @"You will discover many things on the sea floor.
To collect an object, touch the object with your right controller and press the right trigger button.
Different objects are worth different points.
Press your right trigger to continue.";
						break;
					case 4:
						centerText.text = @"The green bar at the bottom is your oxygen level.
You have a limited amount of oxygen, so make sure you return to the surface before it runs out!
Press your right trigger to start the game.";
						break;
					case 5:
						rightControllerHighlighter.UnhighlightElement(
							ControllerElements.Trigger);
						StartGame();
						break;
				}
				step++;
			};
		}
    }

	void StartGame() {
		HideMessage();
		running = true;
	}

}

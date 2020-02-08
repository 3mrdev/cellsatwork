using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour {

	public Text distanceLabel, velocityLabel;

    public void SetValues (float distanceTraveled, float velocity) {
        velocityLabel.text = ((int)(distanceTraveled * 10f)).ToString();
        distanceLabel.text = "";
	}
}
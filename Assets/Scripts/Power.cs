using UnityEngine;
using UnityEngine.UI;

public class Power : MonoBehaviour {
    public Sprite[] HeartSprites;
    public Image HeartUI;
    public Player player;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
	
	// Update is called once per frame
	void Update () {
        HeartUI.sprite = HeartSprites[player.power];
    }
}

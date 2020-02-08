using UnityEngine;
using UnityEngine.UI;

public class Hearts : MonoBehaviour {
    public Sprite[] HeartSprites;
    public Image HeartUI;
    public Player player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    void Update()
    {
        HeartUI.sprite = HeartSprites[player.life];
    }
}

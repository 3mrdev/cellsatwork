using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenu : MonoBehaviour {

	public Player player;
    public GameObject menu, board;
    public Text score;

	public Text scoreLabel;

	private void Awake () {
		Application.targetFrameRate = 1000;
	}

	public void StartGame (int mode) {
		player.StartGame(mode);
		gameObject.SetActive(false);
		Cursor.visible = false;
	}

	public void EndGame (float distanceTraveled) {
		scoreLabel.text = "Score "+((int)(distanceTraveled * 10f)).ToString();
		gameObject.SetActive(true);
		Cursor.visible = true;
	}

    public void GetBoard()
    {
        menu.SetActive(false);
        board.SetActive(true);
        score.text = PlayerPrefs.GetFloat("score_king") + "";
    }

    public void GetMenu()
    {
        menu.SetActive(true);
        board.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class menuScript : MonoBehaviour {

	public Canvas quitMenu;
	public Button startText;
	public Button storeText;
	public Button settingsText;
	public Button exitText;

	// Use this for initialization
	void Start () {

		quitMenu = quitMenu.GetComponent<Canvas> ();
		startText = startText.GetComponent<Button> ();
		storeText = storeText.GetComponent<Button> ();
		settingsText = settingsText.GetComponent<Button> ();
		exitText = exitText.GetComponent<Button> ();
		quitMenu.enabled = false;
	}

	public void ExitPress()
	{
		quitMenu.enabled = true;
		startText.enabled = false;
		storeText.enabled = false;
		settingsText.enabled = false;
		exitText.enabled = false;
	}

	public void NoPress()
	{
		quitMenu.enabled = false;
		startText.enabled = true;
		storeText.enabled = true;
		settingsText.enabled = true;
		exitText.enabled = true;
	}

	public void StartGame()
	{
		Application.LoadLevel (1);
	}

	public void ExitGame()
	{
		Application.Quit ();
	}
}

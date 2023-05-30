using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour {

	Transform menuPanel;
	Event keyEvent;
	Text buttonText;
	KeyCode newKey;
	public static MenuScript MS;

	public bool waitingForKey = false;


	void Start ()
	{
		//Assign menuPanel to the Panel object in our Canvas
		//Make sure it's not active when the game starts
		menuPanel = transform;
		MS = this;
		menuPanel.gameObject.SetActive(false);
		waitingForKey = false;

		/*iterate through each child of the panel and check
		 * the names of each one. Each if statement will
		 * set each button's text component to display
		 * the name of the key that is associated
		 * with each command. Example: the ForwardKey
		 * button will display "W" in the middle of it
		 */
		for(int i = 0; i < menuPanel.childCount; i++)
		{
			if(menuPanel.GetChild(i).name == "LeftKey")
				menuPanel.GetChild(i).GetComponentInChildren<Text>().text = GameManager.GM.left.ToString();
			else if(menuPanel.GetChild(i).name == "RightKey")
				menuPanel.GetChild(i).GetComponentInChildren<Text>().text = GameManager.GM.right.ToString();
			else if(menuPanel.GetChild(i).name == "JumpKey")
				menuPanel.GetChild(i).GetComponentInChildren<Text>().text = GameManager.GM.jump.ToString();
			else if(menuPanel.GetChild(i).name == "PowerKey")
				menuPanel.GetChild(i).GetComponentInChildren<Text>().text = GameManager.GM.power.ToString();
			else if(menuPanel.GetChild(i).name == "TransfoKey")
				menuPanel.GetChild(i).GetComponentInChildren<Text>().text = GameManager.GM.transfo.ToString();
			else if(menuPanel.GetChild(i).name == "PauseKey")
				menuPanel.GetChild(i).GetComponentInChildren<Text>().text = GameManager.GM.pause.ToString();
		}
	}


	void Update ()
	{
		//Escape key will open or close the panel
		/*if(Input.GetKeyDown(KeyCode.Escape) && !menuPanel.gameObject.activeSelf)
			menuPanel.gameObject.SetActive(true);
		else if(Input.GetKeyDown(KeyCode.Escape) && menuPanel.gameObject.activeSelf)
			menuPanel.gameObject.SetActive(false);*/
	}

	void OnGUI()
	{
		/*keyEvent dictates what key our user presses
		 * bt using Event.current to detect the current
		 * event
		 */
		keyEvent = Event.current;

		//Executes if a button gets pressed and
		//the user presses a key
		if(keyEvent.isKey && waitingForKey)
		{
			newKey = keyEvent.keyCode; //Assigns newKey to the key user presses
			waitingForKey = false;
		}
	}

	/*Buttons cannot call on Coroutines via OnClick().
	 * Instead, we have it call StartAssignment, which will
	 * call a coroutine in this script instead, only if we
	 * are not already waiting for a key to be pressed.
	 */
	public void StartAssignment(string keyName)
	{
		if(!waitingForKey)
			StartCoroutine(AssignKey(keyName));
	}

	//Assigns buttonText to the text component of
	//the button that was pressed
	public void SendText(Text text)
	{
		buttonText = text;
	}

	//Used for controlling the flow of our below Coroutine
	IEnumerator WaitForKey()
	{
		while(!keyEvent.isKey)
			yield return null;
	}

	/*AssignKey takes a keyName as a parameter. The
	 * keyName is checked in a switch statement. Each
	 * case assigns the command that keyName represents
	 * to the new key that the user presses, which is grabbed
	 * in the OnGUI() function, above.
	 */
	public IEnumerator AssignKey(string keyName)
	{
		waitingForKey = true;

		yield return WaitForKey(); //Executes endlessly until user presses a key

		switch(keyName)
		{
			case "left":
			GameManager.GM.left = newKey; //set left to new keycode
			buttonText.text = GameManager.GM.left.ToString(); //set button text to new key
			PlayerPrefs.SetString("leftKey", GameManager.GM.left.ToString()); //save new key to playerprefs
			break;
		case "right":
			GameManager.GM.right = newKey; //set right to new keycode
			buttonText.text = GameManager.GM.right.ToString(); //set button text to new key
			PlayerPrefs.SetString("rightKey", GameManager.GM.right.ToString()); //save new key to playerprefs
			break;
		case "jump":
			GameManager.GM.jump = newKey; //set jump to new keycode
			buttonText.text = GameManager.GM.jump.ToString(); //set button text to new key
			PlayerPrefs.SetString("jumpKey", GameManager.GM.jump.ToString()); //save new key to playerprefs
			break;
		case "power":
			GameManager.GM.power = newKey; //set jump to new keycode
			buttonText.text = GameManager.GM.power.ToString(); //set button text to new key
			PlayerPrefs.SetString("powerKey", GameManager.GM.power.ToString()); //save new key to playerprefs
			break;
		case "transfo":
			GameManager.GM.transfo = newKey; //set jump to new keycode
			buttonText.text = GameManager.GM.transfo.ToString(); //set button text to new key
			PlayerPrefs.SetString("transfoKey", GameManager.GM.transfo.ToString()); //save new key to playerprefs
			break;
		case "pause":
			GameManager.GM.pause = newKey; //set jump to new keycode
			buttonText.text = GameManager.GM.pause.ToString(); //set button text to new key
			PlayerPrefs.SetString("pauseKey", GameManager.GM.pause.ToString()); //save new key to playerprefs
			break;
		}

		yield return null;
	}
}
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneButton : MonoBehaviour 
{
	public void OnTitleButton()
	{
		SceneManager.LoadScene("Title");
	}

	public void OnGameButton()
	{
		SceneManager.LoadScene("Temp");
	}

	public void OnQuestButton()
	{
		SceneManager.LoadScene("Quest");
	}

	public void OnQuitButton()
	{
		Application.Quit ();
	}

}

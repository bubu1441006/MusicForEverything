using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

	public GameObject loadingScreen;
	public Slider slider;
	public Text progresstext;

	public void QuitApplication()
	{
		Application.Quit ();
		//for testing 
	}

	public void StudyingMusic()
	{
		//SceneManager.LoadSceneAsync (1);
		LoadLevel(1);
	}

	public void PlayingGamesMusic()
	{
		//SceneManager.LoadSceneAsync (2);
		LoadLevel(2);
	}

	public void ExercisingMusic()
	{
		//SceneManager.LoadSceneAsync (3);
		LoadLevel(3);
	}

	public void RelaxingMusic()
	{
		//SceneManager.LoadSceneAsync (4);
		LoadLevel(4);
	}

	public void LoadLevel(int sceneIndex)
	{
		StartCoroutine (LoadAsychronously (sceneIndex));
	}

	IEnumerator LoadAsychronously (int sceneIndex)
	{
		AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

		loadingScreen.SetActive (true);

		while (!operation.isDone) {
			float progress = Mathf.Clamp01 (operation.progress / .9f);

			slider.value = progress;
			progresstext.text = progress * 100f + "%";

			yield return null;
		}
	}

	public void Back()
	{
		SceneManager.LoadScene (0);
	}
}

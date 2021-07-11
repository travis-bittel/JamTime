using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CanvasScript : MonoBehaviour
{
	#region Singleton Code
	private static CanvasScript _instance;

	public static CanvasScript Instance { get { return _instance; } }

	private void Awake()
	{
		if (_instance != null && _instance != this)
		{
			Debug.LogError("Attempted to Instantiate multiple GameManager in one scene!");
			Destroy(gameObject);
		}
		else
		{
			_instance = this;
			SceneTransitionOverlay = GetComponent<RawImage>();
			SceneTransitionOverlay.CrossFadeAlpha(0.0f, SceneTransitionTime / 2, false);
		}
	}

	private void OnDestroy()
	{
		if (this == _instance) { _instance = null; }
	}
	#endregion

	private bool changingScene;
	private RawImage SceneTransitionOverlay;
	public float SceneTransitionTime = 2.0f;
	private float transitionProgress;
	public String nextScene;
	public bool ready;

	public void Update()
	{
		if (changingScene)
		{
			if (transitionProgress > SceneTransitionTime / 2.0f)
			{
				changingScene = false;
				SceneManager.LoadScene(nextScene);
				nextScene = null;
			} else
			{
				transitionProgress += Time.deltaTime;
			}
		}
	}

	public void OnAdvanceText()
	{
		if (nextScene != null && ready)
		{
			ready = false;
			SceneChange(nextScene);
		}
	}
	public void SceneChange(String nextScene)
	{
		SceneTransitionOverlay.CrossFadeAlpha(1.0f, SceneTransitionTime / 2, false);
		changingScene = true;
		transitionProgress = 0.0f;
		this.nextScene = nextScene;
	}
}

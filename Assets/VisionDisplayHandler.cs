using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Handles the slider display on the screen that shows what vision mode the player is in and the remaining duration.
public class VisionDisplayHandler : MonoBehaviour
{
	#region Singleton Code
	private static VisionDisplayHandler _instance;

	public static VisionDisplayHandler Instance { get { return _instance; } }

	private void Awake()
	{
		if (_instance != null && _instance != this)
		{
			Debug.LogError("Attempted to Instantiate multiple VisionDisplayHandlers in one scene!");
			Destroy(gameObject);
		} else
        {
			_instance = this;
        }
	}

	private void OnDestroy()
	{
		if (this == _instance) { _instance = null; }
	}
	#endregion

	private Slider slider;
	[SerializeField]
	private Image sliderFill;

	// Start is called before the first frame update
	void Start()
    {
		slider = GetComponent<Slider>();
		slider.value = 0;
		if (sliderFill != null)
        {
			sliderFill.color = GameManager.GetColorFromCurrentVisionMode();
		} else
        {
			Debug.LogError("VisionDisplayHandler's sliderFill reference was null! Assign it in the inspector");
        }
	}

    public void UpdateFillColor()
    {
		sliderFill.color = GameManager.GetColorFromCurrentVisionMode();
	}

    private void Update()
    {
        if (GameManager.Instance.CurrentVisionMode != VisionMode.DEFAULT)
        {
			slider.value = Player.Instance.RemainingVisionDuration;
        }
    }

	public void ResetSliderToEmpty()
    {
		slider.value = 0;
		sliderFill.color = GameManager.ConvertVisionModeToColor(VisionMode.DEFAULT);
    }
}

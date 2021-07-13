using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hive : MonoBehaviour
{
    public PlayerTrigger playerTrigger;
    public GameObject bugPrefab;

    // Start is called before the first frame update
    void Start()
    {
        if (playerTrigger != null)
        {
            playerTrigger.TriggerPublish += onPlayerTriggersHive;
        } else
        {
            Debug.LogError("Hive with name " + gameObject.name + " was not assigned a trigger");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	void OnDestroy()
	{
        playerTrigger.TriggerPublish -= onPlayerTriggersHive;
    }

	private void onPlayerTriggersHive(Player player)
	{
        spawnBug();
	}

    private void spawnBug(){
        Bug bug = Instantiate(bugPrefab).GetComponent<Bug>();
        bug.home = transform;
        bug.transform.position = transform.position;
    }
}

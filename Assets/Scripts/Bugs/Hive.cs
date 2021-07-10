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
        playerTrigger.TriggerPublish += onPlayerTriggersHive;
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
        bug.transform.position = transform.position;
        bug.home = transform;
    }
}

using UnityEngine;
using System.Collections;

public class EnemySpawn : MonoBehaviour {

	public GameObject enemy;
    public bool fromRight = true;

    private float lastTime;
    private float delay;

	// Use this for initialization
	void Start () {
	}

    void Update()
    {
        if(Time.time - lastTime > delay)
        {
            Quaternion angle = (fromRight) ? transform.rotation : Quaternion.AngleAxis(180, Vector3.up);
            GameObject enemySpawner = (GameObject)Instantiate(enemy, transform.position, angle);
            enemySpawner.GetComponent<EnemyMovement>().fromRight = fromRight;
            lastTime = Time.time;
            delay = Random.Range(Mathf.Max(1, 10 - Time.time/15), Mathf.Max(1, 10 - Time.time / 15) + 2);
        }
    }


}

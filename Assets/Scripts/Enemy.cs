using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public GameObject FireballPrefab;
    public int shotFrequencyInSeconds = 0;
    public int shotSpeed;

	// Use this for initialization
	void Start () {
        if (shotFrequencyInSeconds > 0)
        {
            InvokeRepeating("LaunchBullet", 0, shotFrequencyInSeconds);
        }
    }
	
	// Update is called once per frame
	void Update () {

    }

    private void LaunchBullet()
    {
        Fireball fireball = Instantiate(FireballPrefab).GetComponent<Fireball>();
        fireball.setShotSpeed(shotSpeed);
        fireball.setMoveDirection(Vector3.back);
    }
}

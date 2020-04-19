using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    public float HP;
    public int goldWorth;
    private PlayerResources _playerResources;
    private EnemySpawn _enemySpawn;
    public GameObject clickParticles;

    // Start is called before the first frame update
    void Start()
    {
        _playerResources = GameObject.FindObjectOfType<PlayerResources>();
        _enemySpawn = GameObject.FindObjectOfType<EnemySpawn>();
        gameObject.GetComponent<SpriteRenderer>().color = new Color(Random.Range(0f,1f), Random.Range(0f,1f), Random.Range(0f,1f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        Vector3 clickedPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        clickedPosition.z = 20;
        var tmp = Instantiate(clickParticles, clickedPosition, Quaternion.identity);
        GameObject.Destroy(tmp, 2.0f);
        HP -= 1;
        if (HP <= 0)
        {
            _playerResources.AddGold(goldWorth);
            _enemySpawn.SpawnEnemy();
            Destroy(gameObject);
        }
    }
}

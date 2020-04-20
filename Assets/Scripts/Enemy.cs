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
    private Vector3 _originalScale;
    private float _scaleSpeed = 20f;
    
    // Start is called before the first frame update
    void Start()
    {
        _playerResources = GameObject.FindObjectOfType<PlayerResources>();
        _enemySpawn = GameObject.FindObjectOfType<EnemySpawn>();
        gameObject.GetComponent<SpriteRenderer>().color = new Color(Random.Range(0f,1f), Random.Range(0f,1f), Random.Range(0f,1f));
        _originalScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.localScale != _originalScale)
        {
            transform.localScale = Vector3.Lerp (transform.localScale, _originalScale, _scaleSpeed * Time.deltaTime);
        }
    }

    private void OnMouseDown()
    {
        Vector3 clickedPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        clickedPosition.z = 20;
        var tmp = Instantiate(clickParticles, clickedPosition, Quaternion.identity);
        transform.localScale = _originalScale * 0.75f;
        GameObject.Destroy(tmp, 2.0f);
        HP -= 1;
        if (HP <= 0)
        {
            _playerResources.AddGold(goldWorth);
            _enemySpawn.SpawnEnemy(new Vector3(0,-1,0));
            Destroy(gameObject);
        }
    }
}

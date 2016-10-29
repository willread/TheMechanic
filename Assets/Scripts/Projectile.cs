﻿using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {
    private bool dead = false;
    private Vector3 startPosition;
    private float maxDistance = 150f;

    [HideInInspector]
    public Vector3 direction;

    [HideInInspector]
    public float speed;

    [HideInInspector]
    public float minDamage;

    [HideInInspector]
    public float maxDamage;

    // Use this for initialization
    void Start () {
        startPosition = transform.position;
    }
	
	// Update is called once per frame
	public void Update () {
        if (!GameManager.Instance.gameActive) return;

        if (!dead) {
            transform.position += direction * speed * Time.deltaTime;

            if (Vector3.Distance(startPosition, transform.position) >= maxDistance) {
                Remove();
            }
        }
	}

    public void OnTriggerEnter(Collider collider) {
        if (!dead) {
            if (collider.tag == "Enemy" || collider.tag == "Player") {
                collider.transform.GetComponent<HealthBar>().Hit(Random.Range(minDamage, maxDamage));
                Explode();
            } else if (collider.name == "Shield") {
                Explode();
            }else {
                Remove();
            }
        }
    }

    public void Remove() {
        dead = true;
        iTween.FadeTo(gameObject, iTween.Hash("alpha", 0f, "time", 0.15f));
        GameObject.Destroy(gameObject, 0.15f);
    }

    private void Explode() {
        GetComponent<Renderer>().enabled = false;
        GetComponent<ParticleSystem>().Play();
        GameObject.Destroy(gameObject, 0.5f);
        dead = true;
    }
}

﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TankHealth : MonoBehaviour {
    private float health = 100f;
    private float totalHealth = 100f;

    private GameObject healthCanvas;
    private Image healthBar;
    private GameObject damageNumberPrefab;

    public delegate void OnDieEvent(GameObject gameObject);
    public event OnDieEvent OnDie;

    void Start() {
        healthCanvas = transform.Find("HealthCanvas").gameObject;
        healthBar = transform.Find("HealthCanvas/Health").GetComponent<Image>();
        damageNumberPrefab = Resources.Load<GameObject>("Prefabs/DamageNumber");
    }

    void Update() {
        healthBar.fillAmount = health / totalHealth;

        healthCanvas.SetActive(BattleManager.Instance.BattleActive); // Only show in battle mode
    }

    public void Hit(float damage) {
        health -= damage;

        GameObject damageNumber = GameObject.Instantiate(damageNumberPrefab);
        damageNumberPrefab.GetComponent<DamageNumber>().Init(damage);
        damageNumber.transform.position = transform.position + Vector3.up * 5f;

        if (health <= 0f) {
            GameObject.Destroy(gameObject);
            OnDie(gameObject);
        }
    }
}

﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Dialog : MonoBehaviour {
    private Text textObject;
    private GameObject canvasObject;
    private bool saying = false;
    private bool done = false;

    private AudioSource audio;
    private AudioClip sound;

    void Start() {
        textObject = transform.Find("Text").GetComponent<Text>();
        audio = GetComponent<AudioSource>();
        sound = Resources.Load<AudioClip>("Sounds/TextBeep");

        canvasObject = gameObject;
    }

    public void Say(string what) {
        if (saying) {
            return;
        }

        GameManager.Instance.gameActive = false;

        Debug.Log("Saying");

        saying = true;
        done = false;

        canvasObject.transform.localScale = Vector3.zero;
        iTween.ScaleTo(canvasObject, iTween.Hash("scale", new Vector3(1, 1, 1), "time", 0.5f));
        textObject.text = "";
        StartCoroutine(TypeText(what));
    }

    void Update() {
        if (saying && done && Input.GetKeyDown(KeyCode.Space)) {
            saying = false;
            iTween.ScaleTo(canvasObject, iTween.Hash("scale", Vector3.zero, "time", 0.5f));
            GameManager.Instance.gameActive = true;
        }

        if (Input.GetKeyDown(KeyCode.P)) {
            Say("Be not afraid of greatness: some are born great, some achieve greatness, and some have greatness thrust upon them.\n\nTo thine own self be true.");
        }
    }

    IEnumerator TypeText(string what) {
        Debug.Log("Say what? : " + what);
        foreach (char letter in what.ToCharArray()) {
            textObject.text += letter;
            if (char.IsLetter(letter)) {
                audio.PlayOneShot(sound);
                yield return new WaitForSeconds(0.02f);
            } else {
                yield return new WaitForSeconds(0.1f);
            }
        }

        done = true;
    }
}

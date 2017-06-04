using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Enemy : MonoBehaviour {

    public GameObject deathEffect;
    public float health = 4f;
    public static int enemiesAlive = 0;
    public string nextLevel = "Level 2";
    public bool enableToSwitchLevel;
    public Text levelCompletedText;
    public GameObject NextLevelButton;
    public AudioSource audio;

    void Start() {
        enemiesAlive++;
        enableToSwitchLevel = false;
        levelCompletedText.text = "";
    }

    private void OnCollisionEnter2D(Collision2D colInfo) {
        if (colInfo.relativeVelocity.magnitude > health) {
            Die();
        }
    }

    private void Die() {
        Destroy(gameObject);
        enemiesAlive--;
        audio.Play();
        Debug.Log("Enemy died, enemies alive: " + enemiesAlive.ToString());
        if (enemiesAlive <= 0) {
            levelCompletedText.text = "Level completed!";
            Win();
        }

        Instantiate(deathEffect, transform.position, Quaternion.identity);
    }

    public void Win() {
        levelCompletedText.text = "Level completed!";
        enableToSwitchLevel = true;
        NextLevelButton.gameObject.SetActive(true);
        if (nextLevel.Equals("Level 2")) {
            Application.Quit();
        }
    }
}

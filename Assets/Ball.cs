using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ball : MonoBehaviour {

    public Rigidbody2D rigidbody;
    public Rigidbody2D hook;

    public float releaseTime = .15f;
    public float maxDragDistane = 2f;

    private bool isFlying = false;
    private bool isPaused = false;
    private bool isPressed = false;

    public GameObject nextBall;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.P)) {
            isPaused = !isPaused;
            Time.timeScale = isPaused ? 0 : 1;
        }

        if (!isFlying && !isPaused && isPressed) {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (Vector3.Distance(mousePosition, hook.position) > maxDragDistane) 
                rigidbody.position = hook.position + (mousePosition - hook.position).normalized * maxDragDistane;         
            else 
                rigidbody.position = mousePosition;                  
        }
    }

    private void OnMouseDown() {
        if (isFlying)
            return;

        isPressed = true;
        rigidbody.isKinematic = true;        
    }

    private void OnMouseUp() {
        if (isFlying)
            return;

        isPressed = false;
        rigidbody.isKinematic = false;
        StartCoroutine(Release());
    }

    IEnumerator Release() {
        yield return new WaitForSeconds(releaseTime);
        GetComponent<SpringJoint2D>().enabled = false;
        isFlying = true;

        yield return new WaitForSeconds(2f);
        if (nextBall != null)
            nextBall.SetActive(true);
        else {
            Enemy.enemiesAlive = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}

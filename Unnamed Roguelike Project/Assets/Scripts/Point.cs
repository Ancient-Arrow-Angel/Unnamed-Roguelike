using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    public GameObject Front;
    public GameObject Back;

    void Update()
    {
        Cursor.lockState = CursorLockMode.Confined;

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 direction = mousePosition - transform.position;
        float angle = Vector2.SignedAngle(Vector2.right, direction);

        transform.position = GameObject.Find("Player").transform.position;


        if (mousePosition.x < GameObject.Find("Player").transform.position.x && GameObject.Find("Player").GetComponent<Player>().InputActive)
        {
            GameObject.Find("Player").transform.localScale = new Vector2(-1, GameObject.Find("Player").transform.localScale.y);
            transform.localScale = new Vector2(-1, transform.localScale.y);
            transform.eulerAngles = new Vector3(0, 0, angle + 180);
            GameObject.Find("Player").GetComponent<Player>().FacingRight = false;
        }
        else if (GameObject.Find("Player").GetComponent<Player>().InputActive)
        {
            GameObject.Find("Player").transform.localScale = new Vector2(1, GameObject.Find("Player").transform.localScale.y);
            transform.localScale = new Vector2(1, transform.localScale.y);
            transform.eulerAngles = new Vector3(0, 0, angle);
            GameObject.Find("Player").GetComponent<Player>().FacingRight = true;
        }

        if (mousePosition.y < GameObject.Find("Player").transform.position.y && GameObject.Find("Player").GetComponent<Player>().InputActive)
        {
            Front.SetActive(true);
            Back.SetActive(false);
        }
        else if (GameObject.Find("Player").GetComponent<Player>().InputActive)
        {
            Back.SetActive(true);
            Front.SetActive(false);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraMover : MonoBehaviour
{
    
    public Vector3 move = new Vector3(-1, 0,1);

    public float timeToPan = 30f;
    public float speed = 0.1f;
    public float moveTime = 0;

    // Update is called once per frame
    void Update()
    {
        if (moveTime >= timeToPan)
        {
            moveTime = 0;
            move *= -1;
        }
        
        moveTime += Time.deltaTime;
        var delta = move * (speed * Time.deltaTime);
        transform.position  += delta;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(ExampleCoroutine(3));
        }
    }
    
  
    IEnumerator ExampleCoroutine(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene(1); //Load scene called Game.
        
    }
}

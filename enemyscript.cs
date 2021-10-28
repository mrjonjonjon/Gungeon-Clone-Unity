using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyscript : MonoBehaviour
{
    public float enemydashsize = 0.05f;
    public float enemydashtimer = 60f;
    private System.Random rand;//= new System.Random();
    
    public GameObject enemybullet;
    // Start is called before the first frame update
    void Start()
    {
        rand = new System.Random((int)Mathf.Floor(100 * transform.position.x));
            moveenemy();
            enemyshoot();
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
         if (collision.collider.CompareTag("playerbullet"))
        {
            Debug.Log("sdfghjkjhgfdsfghj");
            Destroy(this.gameObject);
        }
    }
   
    // Update is called once per frame
    void Update()
    {
       
    }

    void moveenemy()
    {
        
           
            StartCoroutine("enemydash");
        
    }
    void enemyshoot()
    {
        StartCoroutine("shoot");
    }

    IEnumerator shoot()
    {
        for (; ; )
        {
            enemybullet.transform.position = transform.position;
            GameObject.Instantiate(enemybullet);

            yield return new WaitForSecondsRealtime(1);
        }
    }
    IEnumerator enemydash()
    {

        for(; ; ) {

            int dir =rand.Next(2);
           
            for (int i = 0; i < 5; i++)
            {
                if (dir == 0)
                {
                    transform.position += Vector3.left * enemydashsize;
                }
                else
                {
                    transform.position += Vector3.right * enemydashsize;
                }
                yield return null;

            }
             enemydashtimer = 60f;
            yield return new WaitForSecondsRealtime(3*(float)rand.NextDouble());
           
        }
        
    }

    
}

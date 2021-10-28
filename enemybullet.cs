using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemybullet : MonoBehaviour
{
    public float bulletspeed=1f;
    public GameObject enemy;
   // public Slider hpbar;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       // transform.position += Vector3.down * Time.deltaTime * bulletspeed;
    }

    void OnTriggerEnter2D(Collider2D collision)
 {

     if(collision.gameObject.CompareTag("Player") && playerscript.sharedInstance.isvulnerable==true){
         playerscript.sharedInstance.currenthp-=0.5f;
     }
 }

}

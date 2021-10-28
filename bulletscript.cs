using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletscript : MonoBehaviour
{
   

    void Awake(){
        DontDestroyOnLoad(this.gameObject);
       

    }

     void OnEnable()
    {  StartCoroutine(DestroySelfAfterSeconds(0.5f));
        //Debug.Log("PrintOnEnable: script was enabled");
    }

    
    IEnumerator DestroySelfAfterSeconds(float destroyTime){
     yield return new WaitForSeconds(destroyTime);
     gameObject.SetActive(false);
 }

void OnTriggerEnter2D(Collider2D collision)
 {
Debug.Log(collision.gameObject.tag);
     if(collision.gameObject.CompareTag("wall")){
          gameObject.SetActive(false);
     }

    if(this.gameObject.CompareTag("playerbullet") && collision.gameObject.CompareTag("enemy")){

        BOSS1AI boss = collision.gameObject.GetComponent<BOSS1AI>(); 
        boss.currenthp = boss.currenthp-10;
        gameObject.SetActive(false);
}

 
}


}

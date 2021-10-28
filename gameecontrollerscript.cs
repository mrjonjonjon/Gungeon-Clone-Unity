using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameecontrollerscript : MonoBehaviour
{

    public GameObject dashmeter;
    public GameObject player;
   
    // Start is called before the first frame update
    void Start()
    {
 
    }

    // Update is called once per frame
    void Update()
    {
UnityEngine.UI.Image dashm= dashmeter.GetComponent<UnityEngine.UI.Image>();
        playerscript pscript = player.GetComponent<playerscript>();
        if (pscript.dashtimer <= 0f)
        {
            
            dashm.color = Color.green;
        }
        else { dashm.color = Color.white; }
    }
}

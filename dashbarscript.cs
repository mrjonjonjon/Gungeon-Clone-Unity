using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dashbarscript : MonoBehaviour
{ public playerscript player;

    public Slider meterImage;
    void Awake(){
       DontDestroyOnLoad(this.gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {   meterImage.value=1;
        player=playerscript.sharedInstance;
    }

    // Update is called once per frame
    void Update()
    {
        meterImage.value=(60-player.dashtimer)/60;
    }
}

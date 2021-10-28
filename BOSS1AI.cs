using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BOSS1AI : MonoBehaviour
{
    public Slider hpbar;

    //public boo
    bool aggro;

    public float maxhp;

    public float currenthp;

    GameObject player;

    public float attackinterval;

    Coroutine currentRoutine = null;

    //public weapon wpn;
    // Start is called before the first frame update
    void beginattack()
    {
        aggro = true;
    }

    void Start()
    {
        player = playerscript.sharedInstance.gameObject;

        // StartCoroutine(AttackPlayer2(10));
        //StartCoroutine(AttackPlayer3(10));
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(player.transform.position);
        hpbar.value = currenthp / maxhp;

        if (currenthp <= 0)
        {
            StopAllCoroutines();

            this.gameObject.GetComponent<weapon>().DestroyBullets();
            Debug.Log("DONE");

            this.gameObject.SetActive(false);

            // this.gameObject.GetComponent<BOSS1AI>().enabled=false;
            return;
        }
        if (aggro)
        {
            if (currentRoutine == null)
            {
                int x = Random.Range(1, 15);
                if (x <= 5)
                {
                    currentRoutine = StartCoroutine(AttackPlayer());
                }
                else if (x <= 10)
                {
                    currentRoutine = StartCoroutine(AttackPlayer3(6));
                }
                else
                {
                    currentRoutine = StartCoroutine(AttackPlayer2(10));
                }
            }
        }
    }

    //shoot directly at player
    IEnumerator AttackPlayer()
    {
        for (int j = 0; j < 5; j++)
        {
            GetComponent<weapon>().FireAmmo(player.transform.position);
            yield return new WaitForSeconds(attackinterval);
        }
        currentRoutine = null;
    }

    void OnEnable()
    {
        this.GetComponent<AudioSource>().Play();
    }

    //shoot in a ring
    IEnumerator AttackPlayer2(float numdivs)
    {
        for (int j = 0; j < 2; j++)
        {
            for (int i = 0; i < numdivs; i++)
            {
                var angle = (360 / numdivs) * i;
                GetComponent<weapon>()
                    .FireAmmo(20 *
                    new Vector3(Mathf.Sin(Mathf.Deg2Rad * angle),
                        Mathf.Cos(Mathf.Deg2Rad * angle),
                        0));
            }

            yield return new WaitForSeconds(attackinterval);
        }
        currentRoutine = null;
    }

    //shoot in a ring timeshifted
    IEnumerator AttackPlayer3(float numdivs)
    {
        for (int i = 0; i < numdivs; i++)
        {
            var angle = (360 / numdivs) * i;
            GetComponent<weapon>()
                .FireAmmo(20 *
                new Vector3(Mathf.Sin(Mathf.Deg2Rad * angle),
                    Mathf.Cos(Mathf.Deg2Rad * angle),
                    0));
            yield return new WaitForSeconds(0.7f);
        }

        currentRoutine = null;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SimpleAiMoviment : MonoBehaviour
{   
    public bool acordado;
    [SerializeField] Transform target;
    public PlayerMoviment player;
    NavMeshAgent agent;
    //Rigidbody2D rb;
    public Transform lanterna;
    Vector3 diffPos;
    // Start is called before the first frame update
    void Start()
    {
        agent= GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis =false;
        //rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {   
        
        diffPos = (lanterna.position - transform.position);

        if(Mathf.Abs(diffPos.x) < 0.5f && Mathf.Abs(diffPos.y) < 0.5f){
            StartCoroutine(WaitAndDoSomething());    
        }
        if(acordado && player.luzAcessa){
            agent.SetDestination(target.position);}
        else{
            agent.SetDestination(transform.position);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "espinho")
        {
            Destroy(gameObject);
        }else if (collision.gameObject.tag == "Player"){
            Application.LoadLevel(Application.loadedLevel);
        }
        print("collision: "+ collision.gameObject.tag);
    }

    IEnumerator WaitAndDoSomething() {
        yield return new WaitForSeconds(2f);
        acordado=true;
    }

}

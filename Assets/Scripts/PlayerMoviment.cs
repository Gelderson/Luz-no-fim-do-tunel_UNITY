using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoviment : MonoBehaviour
{   
    private bool isMoving;
    private Vector3 origPos, targetPos;
    private float timeToMove = 0.2f;
    private string facing = "right";
    public MovimentacaoLuz light;
    private Vector3 luzFuturePos;
    public Light luzPlayer;
    private bool canGoUp=true;
    private bool canGoLeft=true;
    private bool canGoRight=true;
    private bool canGoDown=true;
    
    public AudioSource audio_vela_on;
    public AudioSource audio_vela_off;

    public bool luzAcessa=true;
    void Start() {
        isMoving=false;
        audio_vela_on.Play();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && !isMoving){
            callMoveLight(Vector3.up);    
            if((facing=="up" || !luzAcessa ) && canGoUp){
                callMoveLight(Vector3.up*2);
                StartCoroutine(MovePlayer(Vector3.up));}
            facing="up";
        }if (Input.GetKeyDown(KeyCode.A) && !isMoving){
            callMoveLight(Vector3.left); 
            if((facing=="left" || !luzAcessa ) && canGoLeft){
                callMoveLight(Vector3.left*2); 
                StartCoroutine(MovePlayer(Vector3.left));}
            facing="left";
        }if (Input.GetKeyDown(KeyCode.S) && !isMoving){
            callMoveLight(Vector3.down); 
            if((facing=="down" || !luzAcessa ) && canGoDown){
                callMoveLight(Vector3.down*2); 
                StartCoroutine(MovePlayer(Vector3.down));}
            facing="down";
        }if (Input.GetKeyDown(KeyCode.D) && !isMoving){
            callMoveLight(Vector3.right);
            if((facing=="right" || !luzAcessa ) && canGoRight){
                callMoveLight(Vector3.right*2); 
                StartCoroutine(MovePlayer(Vector3.right));}
            facing="right";}
        if(Input.GetKeyDown(KeyCode.Q)){
            if(luzAcessa){
                    luzAcessa=false;
                    light.gameObject.active = false;
                    luzPlayer.spotAngle = 57;
                    audio_vela_off.Play();
            }
            else{   
                    audio_vela_on.Play();
                    luzAcessa=true;
                    light.gameObject.active = true;
                    luzPlayer.spotAngle = 84;
            }
        }
    }

    public void callMoveLight(Vector3 direction){
            luzFuturePos = transform.position + direction;
            luzFuturePos.z=-0.62f;
            StartCoroutine(light.MoveLight(luzFuturePos));
    }

    private IEnumerator MovePlayer(Vector3 direction){
        isMoving = true;

        float elapsedTime = 0;

        origPos = transform.position;
        targetPos = origPos + direction;

        while (elapsedTime < timeToMove)
        {
            transform.position = Vector3.Lerp(origPos, targetPos, (elapsedTime / timeToMove));
            elapsedTime+= Time.deltaTime;

            yield return null;
        }
            transform.position = targetPos;
            isMoving = false; 
    }
    
    private void FixedUpdate() {

        RaycastHit2D hitUp = Physics2D.Raycast(transform.position, Vector2.up);
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, Vector2.left);
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position, Vector2.right);
        RaycastHit2D hitDown = Physics2D.Raycast(transform.position, Vector2.down);
        Debug.DrawLine (transform.position, hitUp.point,Color.red);
        Debug.DrawLine (transform.position, hitLeft.point,Color.red);
        Debug.DrawLine (transform.position, hitRight.point,Color.red);
        Debug.DrawLine (transform.position, hitDown.point,Color.red);
        
        canGoUp = Mathf.Abs(hitUp.point.y-transform.position.y)>0.6f;
        canGoLeft = Mathf.Abs(hitLeft.point.x-transform.position.x)>0.6f;  
        canGoRight = Mathf.Abs(hitRight.point.x-transform.position.x)>0.6f;  
        canGoDown = Mathf.Abs(hitDown.point.y-transform.position.y)>0.6f;               

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "espinho")
        {
            Application.LoadLevel(Application.loadedLevel);
        }
        else if (collision.gameObject.tag == "passagem")
        {
            Application.LoadLevel(1);
        }
    }


}

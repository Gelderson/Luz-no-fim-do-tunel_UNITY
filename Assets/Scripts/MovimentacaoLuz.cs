using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentacaoLuz : MonoBehaviour
{
    private Vector3 origPos;
    private float timeToMove = 0.2f;
    public bool blockAhead=false;
    public IEnumerator MoveLight(Vector3 lightFuturePosition){

        float elapsedTime = 0;
        origPos = transform.position;

        while (elapsedTime < timeToMove)
        {
            transform.position = Vector3.Lerp(origPos, lightFuturePosition, (elapsedTime / timeToMove));
            elapsedTime+= Time.deltaTime;

            yield return null;
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundsCheck : MonoBehaviour
{

    [System.Flags]
    public enum eScreenLocs {
        onScreen = 0,
        offRight = 1,
        offLeft = 2,
        offUp = 4,
        offDown = 8
    }

    public enum eType { center, inset, outset };

    [Header("Inscribed")]
    public eType boundsType = eType.center;
    public float radius = 1f;
    public bool keepOnScreen = true;

    [Header("Dynamic")]
    public eScreenLocs screenLocs = eScreenLocs.onScreen;
    //public bool isOnScreen = false;
    public float camWidth;
    public float camHeight;

    void Awake() {
        camHeight = Camera.main.orthographicSize;
        camWidth = camHeight * Camera.main.aspect;
    }

    void LateUpdate() {

        float checkRadius = 0;
        if (boundsType == eType.inset) checkRadius = -radius;
        if (boundsType == eType.outset) checkRadius = radius;

        Vector3 pos = transform.position;
        //isOnScreen = true;

        if (pos.x > camWidth + checkRadius) {
            pos.x = camWidth + checkRadius;
            //isOnScreen = false;
        }

        if (pos.x < -camWidth - checkRadius) {
            pos.x = -camWidth - checkRadius;
            //isOnScreen = false;
        }

        if (pos.y > camHeight + checkRadius) {
            pos.y = camHeight + checkRadius;
            //isOnScreen = false;
        }

        if (pos.y < -camHeight - checkRadius) {
            pos.y = -camHeight - checkRadius;
            //isOnScreen = false;
        }

        if ( keepOnScreen && !isOnScreen ) {
            transform.position = pos;
            //isOnScreen = true;
        }
    }

    public bool isOnScreen {
        get { return ( screenLocs == eScreenLocs.onScreen ); }
    }

    public bool LocIs( eScreenLocs checkLoc ) {
        if( checkLoc == eScreenLocs.onScreen ) return isOnScreen;
        return(( screenLocs & checkLoc) == checkLoc);
    }
}

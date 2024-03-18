using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parallexEffect : MonoBehaviour
{
    public Camera Cam;
    public Transform followTarget;
    
    //starting position for the parallex game object
    Vector2 startingPos;

    //start z value of the parallex game object
    float startingZ;

    //distance that the camera has moved from the starting position of the parallex object
    Vector2 camMoveSinceStart=>(Vector2)Cam.transform.position-startingPos;

    float ZDistancefromTarget=>transform.position.z-followTarget.transform.position.z;

    //if object is infront of target,use near clip plane.If behind target ,use far clip plane
    float clippingPlane=>(Cam.transform.position.z+(ZDistancefromTarget>0?Cam.farClipPlane:Cam.nearClipPlane));

    //The further the object from the player , thr faster the parallex effect object till move.Drag it's Z value closer to the target to make it move slower
    float parallexFactor => Mathf.Abs(ZDistancefromTarget) / clippingPlane;
    
   
    // Start is called before the first frame update
    void Start()
    {
        startingPos=transform.position;
        startingZ=transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        //when the target moves ,move the parallex object the same distance times a multiplayer
        Vector2 newPosition = startingPos + camMoveSinceStart * parallexFactor;

        //The X/Y pos changes based on target travel speed times the parallex factor , but z stays consista=ent
        transform.position = new Vector3(newPosition.x, newPosition.y, startingZ);
    }
}

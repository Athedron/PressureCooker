using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class ClimbController : MonoBehaviour
{
    public void Climb()
    {
        
        Player.instance.transform.position -= Player.instance.rightHand.transform.position;
    }
}

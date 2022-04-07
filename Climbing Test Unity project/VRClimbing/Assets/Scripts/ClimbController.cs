using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(Rigidbody))]
public class ClimbController : MonoBehaviour
{
    public ClimberHand RightHand;
    public ClimberHand LeftHand;
    public SteamVR_Action_Boolean ToggleGripButton;
    public SteamVR_Action_Pose position;
    public ConfigurableJoint ClimberHandle;

    private bool Climbing;
    private ClimberHand ActiveHand;

    void Update()
    {
        updateHand(RightHand);
        updateHand(LeftHand);
        if (Climbing)
        {
            Player.instance.transform.position -= ActiveHand.transform.localPosition;
            //ClimberHandle.targetPosition = -ActiveHand.transform.localPosition;//update collider for hand movment
        }
    }

    void updateHand(ClimberHand Hand)
    {
        if (Climbing && Hand == ActiveHand)//if is the hand used for climbing check if we are letting go.
        {
            if (ToggleGripButton.GetStateUp(Hand.Hand))
            {
                ClimberHandle.connectedBody = null;
                Climbing = false;

                GetComponent<Rigidbody>().useGravity = true;
            }
        }
        else
        {
            if (ToggleGripButton.GetStateDown(Hand.Hand) && Hand.TouchedCount > 0)
            {
                ActiveHand = Hand;
                Climbing = true;
                ClimberHandle.transform.position = Hand.transform.position;
                GetComponent<Rigidbody>().useGravity = false;
                ClimberHandle.connectedBody = GetComponent<Rigidbody>();
                Hand.grabbing = false;
                
            }
        }
    }
}
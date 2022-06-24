using UnityEngine;

public class CarWheel : MonoBehaviour {

    public WheelCollider targetWheel;
    private Vector3 wheelPosition = new();
    private Quaternion wheelRotation = new();
	
	private void Update () {
        targetWheel.GetWorldPose(pos: out wheelPosition, quat: out wheelRotation);
        transform.position = wheelPosition;
        transform.rotation = wheelRotation;
        
	}
}

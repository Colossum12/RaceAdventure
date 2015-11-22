using UnityEngine;
using System.Collections;

public class Kart : MonoBehaviour {
	public WheelCollider[] WheelColliders = new WheelCollider[4];
	public Transform[] tireMeshes = new Transform[4];
	//public float brakeTorque;
	private WheelFrictionCurve sidewaysFriction;
	Transform centerOfMass;

	public float maxTorque = 100f;
	private Rigidbody a_rigidbody;

	void Start () {
		centerOfMass = GameObject.Find ("centerofMass").GetComponent<Transform>();
		a_rigidbody = GetComponent<Rigidbody>();
		a_rigidbody.centerOfMass = centerOfMass.localPosition;
	}

	void FixedUpdate()
	{

		WheelFrictionCurve sidewaysFriction_brake = new WheelFrictionCurve();
		sidewaysFriction_brake.extremumSlip = 0.2F;
		sidewaysFriction_brake.extremumValue = 1.0F;
		sidewaysFriction_brake.asymptoteSlip = 0.5F;
		sidewaysFriction_brake.asymptoteValue = 0.75F;
		sidewaysFriction_brake.stiffness = 0.2F;
		WheelFrictionCurve sidewaysFriction = new WheelFrictionCurve();
		sidewaysFriction.extremumSlip = 0.2F;
		sidewaysFriction.extremumValue = 1.0F;
		sidewaysFriction.asymptoteSlip = 0.5F;
		sidewaysFriction.asymptoteValue = 0.75F;
		sidewaysFriction.stiffness = 2.0F;
		WheelFrictionCurve forwardFriction = new WheelFrictionCurve ();
		forwardFriction.extremumSlip = 1.0F;
		forwardFriction.extremumValue = 1.0F;
		forwardFriction.asymptoteSlip = 0.8F;
		forwardFriction.asymptoteValue = 0.5F;
		forwardFriction.stiffness = 2.0F;
		WheelFrictionCurve forwardFriction_brake = new WheelFrictionCurve ();
		forwardFriction_brake.extremumSlip = 1.0F;
		forwardFriction_brake.extremumValue = 1.0F;
		forwardFriction_brake.asymptoteSlip = 0.8F;
		forwardFriction_brake.asymptoteValue = 0.5F;
		forwardFriction_brake.stiffness = 0.8F;

		float steer = Input.GetAxis ("Horizontal");
		float accelerate = Input.GetAxis ("Accelerate");
		float brake = Input.GetAxis ("Brake");
		bool brake_sting = Input.GetButton ("Brake_sting");
		Debug.Log (brake_sting);

		float finalAngle = steer * 25f;

		WheelColliders [2].steerAngle = finalAngle;
		WheelColliders [3].steerAngle = finalAngle;

		for (int i = 0; i < 4; i++) 
		{
			WheelColliders[i].motorTorque = accelerate * 2F  * maxTorque;
			WheelColliders[i].brakeTorque = brake * 1000;
			if (brake_sting){

				WheelColliders[i].sidewaysFriction = sidewaysFriction_brake ;
				WheelColliders[i].forwardFriction = forwardFriction_brake;
			}else{
				WheelColliders[i].forwardFriction = forwardFriction;
				WheelColliders[i].sidewaysFriction = sidewaysFriction;
			}
				
		}

		centerOfMass.localPosition = Vector3.forward * accelerate /2;
	}

	void Update () {
		UpdateMeshesPositions();
	//	Debug.Log (WheelColliders[1].motorTorque);
	}

	void UpdateMeshesPositions()
	{
		for(int i = 0; i < 4; i++)
		{
			Quaternion quat;
			Vector3 pos;
			WheelColliders[i].GetWorldPose(out pos, out quat);

			tireMeshes[i].position = pos;
			tireMeshes[i].rotation = quat;
		}
	}
}

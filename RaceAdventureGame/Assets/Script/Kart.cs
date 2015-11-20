using UnityEngine;
using System.Collections;

public class Kart : MonoBehaviour {
	public WheelCollider[] WheelColliders = new WheelCollider[4];

	public Transform[] tireMeshes = new Transform[4];
	//public float brakeTorque;

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
		float steer = Input.GetAxis ("Horizontal");
		float accelerate = Input.GetAxis ("360_Triggers_D");
		float brake = Input.GetAxis ("360_Triggers_G");
	

		float finalAngle = steer * 25f;

		WheelColliders [2].steerAngle = finalAngle;
		WheelColliders [3].steerAngle = finalAngle;

		for (int i = 0; i < 4; i++) 
		{
			WheelColliders[i].motorTorque = accelerate * maxTorque;
			WheelColliders[i].brakeTorque = brake * 1000;
		}

		centerOfMass.localPosition = Vector3.forward * accelerate /2;
	}

	void Update () {
		UpdateMeshesPositions();
		Debug.Log (WheelColliders[1].motorTorque);
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

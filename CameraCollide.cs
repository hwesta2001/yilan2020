using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollide : MonoBehaviour
{

	[SerializeField] Transform follow;
	[SerializeField] private float distanceUp, distanceAway, smooth, growY;
	public LayerMask layerMask;
	
	private Vector3 targetPosition;


	[SerializeField] private float lookdist;
	//private Vector3 lookPos;

	//private Vector3 velocity = Vector3.zero;


	private void Start()
	{
		float up = distanceUp + Main.main.sizeCount * .5f * growY;
		float back = distanceAway + Main.main.sizeCount * .125f * growY;
		Vector3 offset = follow.position + follow.forward * lookdist;
		transform.position =new Vector3(offset.x, offset.y + up, offset.z - back);
	}

	private void LateUpdate()
	{
		float up = distanceUp + Main.main.sizeCount * .5f * growY;
		float back = distanceAway + Main.main.sizeCount * .125f * growY*0;
		Vector3 offset = follow.position + follow.forward * lookdist;

		targetPosition = new Vector3(offset.x, offset.y + up, offset.z - back);

		//Debug.DrawRay(follow.position, Vector3.up * distanceUp, Color.red);
		//Debug.DrawRay(follow.position, -1 * follow.forward * distanceAway, Color.blue);
		//Debug.DrawLine(follow.position, targetPosition, Color.magenta);

		CampForWall(offset, ref targetPosition);

		transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smooth);
		//transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, 0.1f);

		Vector3 targetPostition = new Vector3(transform.position.x, offset.y, offset.z);
		transform.LookAt(targetPostition);

	}

	private void CampForWall(Vector3 fromObject, ref Vector3 toTarget)
	{

		if (Physics.Linecast(fromObject, toTarget, out RaycastHit wallHit, layerMask))
		{
			//	Debug.DrawRay(wallHit.point, Vector3.left, Color.red);
			toTarget = new Vector3(wallHit.point.x, toTarget.y, wallHit.point.z);

		}
	}
}
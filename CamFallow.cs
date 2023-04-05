using UnityEngine;

public class CamFallow : MonoBehaviour
{
	[SerializeField] Transform target;
	[SerializeField] float sap=2;
	[SerializeField] float speed = 1f,yx,zx;
	[SerializeField] Vector3 offset;
	private Vector3 tempoffset, desiredPos;

	private void Start()
	{

		transform.position = target.position + target.forward * sap + offset;
	}

	private void LateUpdate()
	{
		
		if(Hareket.hareket.movementSpeed!=0)
		{
			float x = Hareket.hareket.BodyParts.Count;
			x = Mathf.Clamp(x, 0, 80f);

			tempoffset = target.position + target.forward * sap + offset;

			desiredPos = new Vector3(tempoffset.x, tempoffset.y + x * yx, tempoffset.z - x * zx);

			transform.position = Vector3.Lerp(transform.position, desiredPos, Time.deltaTime * speed);
		}


		
	}
}

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;
using System.Collections;

public class Hareket : MonoBehaviour
{
	[SerializeField] DynamicJoystick Joystick;
	public static Hareket hareket;

	public GameObject bodyprefab;
	public List<Transform> BodyParts = new List<Transform>();
	public Transform kuyruk;
	[SerializeField]
	private GameObject DeathPrefab;
	[SerializeField] private float mindistance;
	public float turnSpeed;
	[SerializeField] private int beginSize;
	public int sizeMax = 60;
	public int frenzySize = 10;
	private int frenzyControl;
	public float movementSpeed;
	private float movementSpeedInit;
	[SerializeField]
	private ParticleSystem simsek1, simsek2,simsek3;
	[HideInInspector]
	public bool canMove;
	[HideInInspector]
	public float v, h;
	private float dis, basla;
	private Transform curBodyPart;
	private Transform PrevBodyPart;


	private void Awake()
	{
		hareket = this;
		canMove = false;

	}

	private void Start()
	{
		for (int i=0; i < beginSize; i++)
		{
			AddBodyPart();
		}
		movementSpeedInit = movementSpeed;
	}
	private void Update()
	{


		if (basla > 2f && movementSpeed > 0) // bellir bir süre gecikme ile hareket baslasın
		{
			MoveFirst();
		}
		else
		{
			basla += Time.deltaTime;
		}

		




		if (Input.GetKeyDown(KeyCode.Q)) // bilgisayarda kontrol için
		{
			AddBodyPart();

			Main.main.ChangeScore(25);
		}

	}

	private void LateUpdate()
	{
		if (basla > 2f && movementSpeed > 0)
		{
			MoveBodyParts();
		}


		mindistance = Mathf.Lerp(mindistance, (float)BodyParts.Count*.3f, .2f * Time.deltaTime);


	}





	void MoveFirst()
	{
		BodyParts[0].Translate(BodyParts[0].forward * movementSpeed * Time.smoothDeltaTime, Space.World);
	}



	private void MoveBodyParts ()
	{

		for (int i = 1; i < BodyParts.Count; i++)
		{

			curBodyPart = BodyParts[i];
			PrevBodyPart = BodyParts[i - 1];

			Vector3 offset = PrevBodyPart.position - curBodyPart.position;
			dis = offset.sqrMagnitude;

			Vector3 newpos = PrevBodyPart.position;

			float T = Time.deltaTime * (dis * dis) / mindistance / mindistance * movementSpeed;
			T = Mathf.Clamp(T, 0, 0.6f);
			curBodyPart.position = Vector3.Slerp(curBodyPart.position, newpos, T);
			
			curBodyPart.localScale = Vector3.Slerp(curBodyPart.localScale, PrevBodyPart.localScale, 1);
			curBodyPart.LookAt(PrevBodyPart);
		}

		kuyruk.position = BodyParts[BodyParts.Count - 1].position;
		kuyruk.localScale = Vector3.Slerp(kuyruk.localScale, BodyParts[BodyParts.Count - 1].localScale, 1);
		kuyruk.rotation = BodyParts[BodyParts.Count - 1].rotation;
	}

	public void AddBodyPart()
	{

		Transform newpart = Instantiate(bodyprefab, BodyParts[BodyParts.Count - 1].position, BodyParts[BodyParts.Count - 1].rotation,transform).transform;
		
		BodyParts.Add(newpart);

		if (BodyParts.Count > beginSize+1)
		{
			canMove = true;
		}


		GrowSize(.2f); // burada her part eklendiğinde büyüyecek

		Main.main.sizeCount++;

		frenzyControl++;
		if (frenzyControl == frenzySize)
		{
			StartCoroutine("FrenzyTime");
		}
	}


	public void GrowSize(float gs) //burda boyutu ayarlanıyor
	{


		if (BodyParts.Count < sizeMax) //  büyüklük maksimum.
		{

			Vector3 targetScale = BodyParts[0].localScale + Vector3.one * gs;
			BodyParts[0].DOScale(targetScale, 1f).SetEase(Ease.InSine);
			//BodyParts[0].localScale = Vector3.Lerp(BodyParts[0].localScale, targetScale, 1 * Time.deltaTime);
			

		}

	}

	public void AllPartDeathEffect()
	{

		for (int i =0; i < BodyParts.Count; i++)
		{
			GameObject go = Instantiate(DeathPrefab, BodyParts[i].position, Quaternion.identity, BodyParts[i]);
			go.transform.localPosition = Vector3.up*2;
			go.transform.localScale = go.transform.localScale+Vector3.one*Random.Range(1f,2.3f);
			go.SetActive(true);

		}
	}


	IEnumerator FrenzyTime()
	{
		frenzyControl = 0;
		movementSpeed = movementSpeedInit*1.5f;
		simsek1.gameObject.SetActive(true);
		simsek2.gameObject.SetActive(true);
		simsek3.gameObject.SetActive(true);

		yield return new WaitForSeconds(5);

		movementSpeed = movementSpeedInit;
		simsek1.gameObject.SetActive(false);
		simsek2.gameObject.SetActive(false);
		simsek3.gameObject.SetActive(false);

		yield break;
	}

}
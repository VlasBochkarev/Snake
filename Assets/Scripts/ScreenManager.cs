using UnityEngine;


public class ScreenManager : MonoBehaviour
{
	public Camera MainCam;

	public Transform TopWall;
	public Transform BottomWall;
	public Transform LeftWall;
	public Transform RightWall;
	public BoxCollider2D Area;


	private void Start()
	{
		LeftWall.transform.position = MainCam.ScreenToWorldPoint(new Vector3(28f, Screen.height / 2, 10f));
		LeftWall.transform.localScale = new Vector3(1f, (Screen.height / 10 / 2) - 2, 1f);

		RightWall.transform.position = MainCam.ScreenToWorldPoint(new Vector3(Screen.width - 28f, Screen.height / 2, 10f));
		RightWall.transform.localScale = new Vector3(1f, (Screen.height / 10 / 2) - 2, 1f);

		TopWall.transform.position = MainCam.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height - 56, 10f));
		TopWall.transform.localScale = new Vector3((Screen.width / 10 / 2) + 2, 1f, 1f);

		BottomWall.transform.position = MainCam.ScreenToWorldPoint(new Vector3(Screen.width / 2, 56, 10f));
		BottomWall.transform.localScale = new Vector3((Screen.width / 10 / 2) + 2, 1f, 1f);

		Area.size = new Vector2((Screen.width / 10 / 2) - 2, (Screen.height / 10 / 2) - 6);


		Debug.Log("width : " + Screen.width);
		Debug.Log("height : " + Screen.height);
	}

}


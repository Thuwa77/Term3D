using System.Collections;
﻿using UnityEngine;

public class addElementBehaviour : Bolt.EntityBehaviour<IPlayerState>
{
	public Transform spawnPoint;
	public Transform hook;

	public float lenghtRay;



	private GameObject modelsMenu;
	private GameObject filesMenu;
	private FilesMenu filesMenuScript;

	public override void Attached()
	{
		modelsMenu = GameObject.Find("ModelsMenu");
		modelsMenu.SetActive(false);
		filesMenu = GameObject.Find ("FilesMenu");
		filesMenuScript = filesMenu.GetComponent<FilesMenu>();
		filesMenu.SetActive (false);
	}

	public override void SimulateOwner()
	{
		if (Input.GetKeyDown(KeyCode.E))
		{
			Debug.Log ("E des barres");
			modelsMenu.SetActive(true);
		}

		RaycastHit hit;
		Ray intersectionRay = Camera.main.ScreenPointToRay(new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0.0f));

		if (filesMenu.activeSelf == false)
			if (Input.GetMouseButtonDown(0))
			{
				Debug.Log("ça appuie");
				if (Physics.Raycast(intersectionRay, out hit, lenghtRay))
				{
				switch (hit.collider.tag)
				{
				case "OtherObject":
					Debug.Log("ça otuche" + hit.collider.tag);
					hit.transform.SendMessage("pickUp", true, SendMessageOptions.DontRequireReceiver);
					break;
				case "AudioObject":
					Debug.Log("ça otuche" + hit.collider.tag);
					hit.transform.SendMessage("PlayAndPause",SendMessageOptions.DontRequireReceiver);
					break;
				case "TextObject":
					break;
				case "VideoObject":
					break;
				case "ImageObject":
					break;
				case "LinkObject":
					break;
				}
				}
			}
		//pas propre ici a refaire
		else if (Input.GetMouseButtonDown(1))
		{
			if (Physics.Raycast(intersectionRay, out hit, lenghtRay))
			{
				switch (hit.collider.tag)
				{
				case "ImageObject":
					filesMenu.SetActive(true);
					filesMenuScript.Model = hit.collider.gameObject;
					filesMenuScript.FileType = ModelsUtils.FilesTypes.Image;
					filesMenuScript.CreateFileList();
					break;
				case "AudioObject":
					filesMenu.SetActive(true);
					filesMenuScript.Model = hit.collider.gameObject;
					filesMenuScript.FileType = ModelsUtils.FilesTypes.Audio;
					filesMenuScript.CreateFileList();
					break;
				case "VideoObject":
					filesMenu.SetActive(true);
					filesMenuScript.Model = hit.collider.gameObject;
					filesMenuScript.FileType = ModelsUtils.FilesTypes.Video;
					filesMenuScript.CreateFileList();
					break;
				case "TextObject":
					filesMenu.SetActive(true);
					filesMenuScript.Model = hit.collider.gameObject;
					filesMenuScript.FileType = ModelsUtils.FilesTypes.Text;
					filesMenuScript.CreateFileList();
					break;
				case "LinkObject":
					filesMenu.SetActive(true);
					filesMenuScript.Model = hit.collider.gameObject;
					filesMenuScript.FileType = ModelsUtils.FilesTypes.Link;
					filesMenuScript.CreateFileList();
					break;
				case "OtherObject":
					Debug.Log("ça otuche" + hit.collider.tag);
					hit.transform.SendMessage("pickUp", false, SendMessageOptions.DontRequireReceiver);
					break;
				}
			}
		}
		else if (Input.GetKey(KeyCode.O))
			if (Physics.Raycast(intersectionRay, out hit, lenghtRay))
				if (hit.collider.tag == "OtherObject")
					hit.transform.SendMessage("Destroy", true, SendMessageOptions.DontRequireReceiver);
		base.SimulateOwner();
	}
}

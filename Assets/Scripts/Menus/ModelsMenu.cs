﻿using UnityEngine;
using UnityEngine.UI;

public class ModelsMenu : Bolt.GlobalEventListener
{
	private const int MARGIN = 300;

	public GameObject Btn;
	public GameObject Content;
	public GameObject TypesMenu;
	public GameObject ModelsSubMenu;
	public ModelsUtils.ModelList[] ModelsList;
	public GameObject Player;

	private void Start() {}

	private void Update() {}

	private void createContent(GameObject[] models, int height, int fileType)
	{
		if (models.Length % 3 != 0)
			height += MARGIN;

		RectTransform rectTransform = Content.gameObject.GetComponent<RectTransform>();
		rectTransform.sizeDelta = new Vector2(0, height);
		rectTransform.anchoredPosition = new Vector2(0, -(height / 2 - MARGIN));

		for (int i = 0; i < models.Length; ++i)
		{
			GameObject model = models[i];
			GameObject btn = Instantiate(Btn) as GameObject;

			btn.GetComponent<RectTransform>().anchoredPosition = new Vector2((i % 3 * MARGIN) - MARGIN, (height / 2 - MARGIN / 2) - (i / 3 * MARGIN));
			int nb = i; // it looks VERY stupid, but there's an explication...
			btn.GetComponent<Button>().onClick.AddListener(delegate { ModelsBtns(fileType, nb); });
			btn.GetComponentInChildren<Image>().sprite = TextureUtils.GetSpriteFromAsset(model);
			btn.GetComponentInChildren<Text>().text = model.name;
			btn.transform.SetParent(Content.transform, false);
		}
	}

	public void TypesBtns(int fileType) // see enum FilesTypes in ModelsUtils
	{
		TypesMenu.SetActive(false);

		GameObject[] models = ModelsList[fileType].Models;
		createContent(models, models.Length / 3 * MARGIN, fileType);

		ModelsSubMenu.SetActive(true);
	}

	public void ModelsBtns(int fileType, int nb)
	{
		Transform spawn = Player.GetComponentInChildren<AddElementBehaviour>().Hook.transform;

		spawn.Rotate(Vector3.up, 180);
		BoltNetwork.Instantiate(ModelsList[fileType].Models[nb], spawn.position, spawn.rotation);
		spawn.Rotate(Vector3.up, 180);

		CloseBtn();
	}

	public void BackBtn()
	{
		ModelsSubMenu.SetActive(false);

		foreach (Transform child in Content.transform)
			GameObject.Destroy(child.gameObject);

		TypesMenu.SetActive(true);
	}

	public void CloseBtn()
	{
		BackBtn();
		gameObject.SetActive(false);
	}
}

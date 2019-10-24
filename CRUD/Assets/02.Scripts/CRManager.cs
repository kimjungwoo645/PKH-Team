using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;

public class CRManager : MonoBehaviour
{
    //[SerializeField] TextAsset dataFile;
    [SerializeField] RectTransform contentRectTransform;
    [SerializeField] GameObject cellPrefab;

    [SerializeField] InputField userName;
    [SerializeField] InputField userPhoneNumber;
    [SerializeField] InputField userMail;

    private GameObject panelF;
    private GameObject panelS;

    private GameObject addPanel;
    private string jsonStr = "";
    const string path = "Assets\\Data\\data.json";
    const float cellHeight = 200;

    private bool panelMoveOn = false;
    private float panelSpeed = 1f;


    void Start()
    {
        panelF = GameObject.Find("PanelF");
        panelS = GameObject.Find("PanelS");

        addPanel = GameObject.Find("ADDPanel");
        addPanel.SetActive(false);

        LinkData();
    }

    private void LinkData()
    {
        PersonList personList = CRPerson.LoadData(path);
        jsonStr = JsonUtility.ToJson(personList);

        contentRectTransform.sizeDelta = new Vector2(0, personList.persons.Count * cellHeight);

        for (int i = 0; i < personList.persons.Count; i++)
        {
            Person person = personList.persons[i];

            Cell cell = Instantiate(cellPrefab, contentRectTransform.transform).GetComponent<Cell>();
            cell.SetCellData(person);
        }
    }

    private void Save()
    {
        PersonList personList = JsonUtility.FromJson<PersonList>(jsonStr);
        Person personADD = new Person(userName.text, userPhoneNumber.text, userMail.text);

        personList.persons.Add(personADD);
        jsonStr = JsonUtility.ToJson(personList);

        using (StreamWriter streamWriter = new StreamWriter(path))
        {
            streamWriter.Write(jsonStr);
        }

        contentRectTransform.sizeDelta = new Vector2(0, personList.persons.Count * cellHeight);

        Cell cell = Instantiate(cellPrefab, contentRectTransform.transform).GetComponent<Cell>();
        cell.SetCellData(personADD);
    }

    public void OnADDClick()
    {
        addPanel.SetActive(true);
    }

    public void OnADDCancelClick()
    {
        addPanel.SetActive(false);
    }

    public void OnSaveClick()
    {
        Save();
    }

    public void OnClickData()
    {
        if (panelMoveOn) return;
        StartCoroutine(PanelMove(panelF, panelF.transform.position, new Vector3(-1080, 0, 0), panelSpeed));
        StartCoroutine(PanelMove(panelS, panelS.transform.position, new Vector3(0, 0, 0), panelSpeed));

        
    }

    public void OnClickDataBack()
    {
        if (panelMoveOn) return;
        StartCoroutine(PanelMove(panelF, panelF.transform.position, new Vector3(0, 0, 0), panelSpeed));
        StartCoroutine(PanelMove(panelS, panelS.transform.position, new Vector3(1080, 0, 0), panelSpeed));
    }


    IEnumerator PanelMove(GameObject gameObject, Vector3 start, Vector3 end, float time)
    {
        panelMoveOn = true;
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / time;
            gameObject.transform.position = Vector3.Lerp(start, end, t);
            yield return null;
        }
        panelMoveOn = false;
    }
}

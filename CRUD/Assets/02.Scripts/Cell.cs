using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    public Text name;
    public Text phoneNumber;
    public Text email;

    public void SetCellData(Person cellData)
    {
        this.name.text = cellData.Name;
        this.phoneNumber.text = cellData.PhoneNumber;
        this.email.text = cellData.Email;
    }

    public void OnClick()
    {
        GetCellData();
        GameObject.Find("CRManager").GetComponent<CRManager>().OnClickData();
    }

    public void GetCellData()
    {
        Text nameT = GameObject.Find("SName").GetComponent<Text>();
        nameT.text = name.text;
        nameT = GameObject.Find("SPhoneNumber").GetComponent<Text>();
        nameT.text = phoneNumber.text;
        nameT = GameObject.Find("SEmail").GetComponent<Text>();
        nameT.text = email.text;
    }
}

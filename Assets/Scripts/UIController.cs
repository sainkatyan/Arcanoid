using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Text text;
    private string dataText;
    private float waitingTime;

    private void Start()
    {
        text.text = "";
    }
    public void UpdateText(string _dateText, float _waitingTime)
    {
        dataText = _dateText;
        waitingTime = _waitingTime;
        StartCoroutine(StartVisibleText(dataText, waitingTime));
    }

    private IEnumerator StartVisibleText(string _dataText, float _waitingTime)
    {
        text.text = _dataText;
        yield return new WaitForSeconds(waitingTime);
        text.text = "";
    }
}

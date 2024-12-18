using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.EventSystems;

public class Task5 : MonoBehaviour
{
    public Text test;
    public Text year;
    public Text nm;
    public string jsonURL;
    public Jsonclass jsnData;

    void Start()
    {
        StartCoroutine(getData());
    }

    IEnumerator getData()
    {
        Debug.Log("Загрузка...");
        var uwr = new UnityWebRequest(jsonURL);
        uwr.method = UnityWebRequest.kHttpVerbGET;
        var resultFile = Path.Combine(Application.persistentDataPath, "result.json");
        var dh = new DownloadHandlerFile(resultFile);
        dh.removeFileOnAbort = true;
        uwr.downloadHandler = dh;
        yield return uwr.SendWebRequest();
        Debug.Log("Файл сохранён по пути:" + resultFile);
        jsnData = JsonUtility.FromJson<Jsonclass>(File.ReadAllText(Application.persistentDataPath + "/result.json"));
        test.text = jsnData.Test.ToString();
        year.text = jsnData.Year.ToString();
        nm.text = jsnData.Name.ToString();
        yield return StartCoroutine(getData());
    }
    [System.Serializable]

    public class Jsonclass
    {
        public int Test;
        public int Year;
        public string Name;
    }
}

 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class HttpManager : MonoBehaviour
{

    [SerializeField]
    private string URL;
    public Text myText;
    // Start is called before the first frame update
    void Start()
    {
       
}

    public void ClickGetScores()
    {
        StartCoroutine(GetScores());
    }

    IEnumerator GetScores()
    {
        string url = URL + "/leaders";
        UnityWebRequest www = UnityWebRequest.Get(url);

        yield return www.SendWebRequest();

        if (www.isNetworkError)
        {
            Debug.Log("NETWORK ERROR " + www.error);
        }
        else if(www.responseCode == 200){
            //Debug.Log(www.downloadHandler.text);
            Scores rData = JsonUtility.FromJson<Scores>(www.downloadHandler.text);    

            foreach (Scoree s in rData.scores)
            {
                Debug.Log(s.name + " | " + s.value);
                myText.text=(s.name +" | "+s.value);
            }
        }
        else
        {
            Debug.Log(www.error); 
        }
    }
   
}


[System.Serializable]
public class Scoree
{
    public int userId;
    public string name;
    public int value;

}

[System.Serializable]
public class Scores
{
    public Scoree[] scores;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HttpManagerAct2 : MonoBehaviour
{
    [SerializeField]
    private string URL;

    public string Token;

    private string Username;

    // Start is called before the first frame update
    void Start()
    {
        Token = PlayerPrefs.GetString("token");
        Username = PlayerPrefs.GetString("username");
        StartCoroutine(GetPerfil());

        Debug.Log("TOKEN" + Token);
    }

   

    private string GetInputData()
    {
        AuthData data = new AuthData();

        data.username = GameObject.Find("InputFieldUsername").GetComponent<InputField>().text;
        data.password = GameObject.Find("InputFieldPassword").GetComponent<InputField>().text;

        string postData = JsonUtility.ToJson(data);
        return postData;
    }

    public void ClickSignUp()
    {
         string postData = GetInputData();
        
        
        StartCoroutine(SignUp(postData));
    }

  
    public void ClickLogIn()
    {
        string postData = GetInputData();
        
        StartCoroutine(LogIn(postData));
    }

  

    IEnumerator SignUp(string postData)
    {
        Debug.Log("SignUP"+postData);


        string url = URL + "/api/usuarios";
        UnityWebRequest www = UnityWebRequest.Put(url, postData);
        www.method = "POST";
        www.SetRequestHeader("content-type", "application/json");

        yield return www.SendWebRequest();

        if (www.isNetworkError)
        {
            Debug.Log("NETWORK ERROR " + www.error);
        }
        else if (www.responseCode == 200)
        {
            //Debug.Log(www.downloadHandler.text);
            AuthData resData = JsonUtility.FromJson<AuthData>(www.downloadHandler.text);

            Debug.Log("Bienvenido " + resData.usuario.username + ", id:" + resData.usuario._id);

        }
        else
        {
            Debug.Log(www.error);
            Debug.Log(www.downloadHandler.text);
        }
    }

    IEnumerator LogIn(string postData)
    {
        Debug.Log("LogIn"+postData);


        string url = URL + "/api/auth/login"; 
        UnityWebRequest www = UnityWebRequest.Put(url, postData);
        www.method = "POST";
        www.SetRequestHeader("content-type", "application/json");

        yield return www.SendWebRequest();

        if (www.isNetworkError)
        {
            Debug.Log("NETWORK ERROR " + www.error);
        }
        else if (www.responseCode == 200)
        {
            //Debug.Log(www.downloadHandler.text);
            AuthData resData = JsonUtility.FromJson<AuthData>(www.downloadHandler.text);

            Debug.Log("Autenticado " + resData.usuario.username + ", id:" + resData.usuario._id);
            Debug.Log("TOKEN" + resData.token);

            PlayerPrefs.SetString( "token", resData.token);
            PlayerPrefs.SetString("username", resData.usuario.username);
            SceneManager.LoadScene("Game");
            

        }
        else
        {
            Debug.Log(www.error);
            Debug.Log(www.downloadHandler.text);
        }
    }

    IEnumerator GetPerfil()
    {
       


        string url = URL + "/api/usuarios/" + Username;
        UnityWebRequest www = UnityWebRequest.Get(url);     
        www.SetRequestHeader("x-token", Token);

        yield return www.SendWebRequest();

        if (www.isNetworkError)
        {
            Debug.Log("NETWORK ERROR " + www.error);
        }
        else if (www.responseCode == 200)
        {
            //Debug.Log(www.downloadHandler.text);
            AuthData resData = JsonUtility.FromJson<AuthData>(www.downloadHandler.text);

            Debug.Log("Token Valido " + resData.usuario.username + ", id:" + resData.usuario._id);
            SceneManager.LoadScene("Game");

        }
        else
        {
            Debug.Log(www.error);
            Debug.Log(www.downloadHandler.text);
        }
    }
}




[System.Serializable]
public class AuthData
{
    public string username;
    public string password;
    public UserData usuario;
    public string token;
}

[System.Serializable]
public class UserData
{
    public string _id;
    public string username;
    public bool estado;
    public int score;
    public int highScore;
}
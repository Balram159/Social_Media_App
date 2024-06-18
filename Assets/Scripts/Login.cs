using System.Collections;
using System.Collections.Generic;
using TigerForge;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Login : MonoBehaviour
{
    public TextMeshProUGUI username;
    public TextMeshProUGUI password;
    //public UnityEvent login;

    public void UserLogin()
    {
        //login.Invoke();
        _ = UniRESTClient.Async.Login(username.text, password.text, (bool ok) =>
        {
            if (ok) Debug.Log(UniRESTClient.userAccount.username + " LOGGED IN!"); else Debug.Log("ERROR: " + UniRESTClient.ServerError);
        });
    }

}

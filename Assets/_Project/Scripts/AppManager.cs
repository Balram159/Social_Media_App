using UnityEngine;
using TigerForge;
using UnityEngine.Events;
using TMPro;

public class AppManager : MonoBehaviour
{

    [Header("Debug")]
    private bool m_Debug;

    [Header("Events")]
    public UnityEvent OnLoggedIn;

    [Header("UI Reference")]
    public TextMeshProUGUI empCode;
    public TextMeshProUGUI empName;
    public TextMeshProUGUI email;
    public TextMeshProUGUI contact;
    public TextMeshProUGUI designation;

    private void Start()
    {
        UniRESTClient.debugMode = m_Debug;

        //_ = UniRESTClient.Async.ApplicationLogin((result) =>
        //{

        _ = UniRESTClient.Async.Login("test8554", "pass5125", (bool ok) =>
        {

            if (ok) Debug.Log(UniRESTClient.userAccount.username + " LOGGED IN!"); else Debug.Log("ERROR: " + UniRESTClient.ServerError);
        });
        //});
        
    }

    public void DBRead()
    {
        var result = UniRESTClient.Async.ReadOne<DB.Details>(
            API.data_emp_data,
            new DB.Details { emp_code = empCode.text },
            (DB.Details result, bool ok) =>
            {
                if (ok)
                {
                    OnLoggedIn?.Invoke();
                    empName.text = result.emp_name;
                    email.text = result.email_Id;
                    contact.text = result.contact_no.ToString();
                    designation.text = result.designation;
                    Debug.Log("This player name:" + result.emp_name);
                }
                else
                {
                    Debug.LogWarning("No result!");
                }
            }
        );
    }
}

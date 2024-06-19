using UnityEngine;
using TigerForge;
using UnityEngine.Events;
using TMPro;
using System;

public class AppManager : MonoBehaviour
{

    [Header("Debug")]
    private bool m_Debug;

    [Header("Events")]
    public UnityEvent OnLoggedIn;
    public UnityEvent OnDoctorDetailSumbit;
    

    [Header("UI Reference")]
    public TextMeshProUGUI empCode;
    public TextMeshProUGUI empName;
    public TextMeshProUGUI email;
    public TextMeshProUGUI contact;
    public TextMeshProUGUI designation;
    public TextMeshProUGUI doctorName;
    public TextMeshProUGUI doctorQulification;
    public TextMeshProUGUI doctorCity;
    public TextMeshProUGUI doctorState;
    public TextMeshProUGUI doctorEmailID;

    [Header("Variables")]
    public static string imgURL;
    public static string videoURL;

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
        var result = UniRESTClient.Async.ReadOne<DB.Employee_details>(
            API.data_emp_data,
            new DB.Employee_details { emp_code = empCode.text },
            (DB.Employee_details result, bool ok) =>
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

    [ContextMenu("Write to Emp and Doctor table")]
    public void DBWrite()
    {
        if (ValidateNotNull(empCode,empName,contact,email,doctorName,doctorCity,doctorEmailID,doctorQulification,imgURL))
        {
            _ = UniRESTClient.Async.Write(API.data_write_doc, new DB.Emp_and_doctor_details { emp_code = empCode.text, emp_contact = contact.text, emp_email = email.text, emp_name = empName.text, doctor_name = doctorName.text, doctor_city = doctorCity.text, doctor_emailID = doctorEmailID.text, doctor_qualification = doctorQulification.text, doctor_state = doctorState.text, image_url = imgURL, video_url = videoURL }, (bool ok) =>
            {
                if (ok) Debug.Log("New record written. ID: " + UniRESTClient.DBresponse); else Debug.Log("ERROR: " + UniRESTClient.DBerror);
            });
        } 
    }

    public static bool ValidateNotNull(params object[] variables)
    {
        foreach (var variable in variables)
        {
            if (variable == null)
            {
                return false;
                throw new ArgumentNullException(nameof(variable), "A required variable was null.");
            }
        }

        return true;
    }
}

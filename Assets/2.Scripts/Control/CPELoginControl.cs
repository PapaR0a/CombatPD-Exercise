using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using TaggleTemplate.Core;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class CPELoginControl
{
    #region API
    private static CPELoginControl api;

    public static CPELoginControl Api
    {
        get { return api; }
        set { api = value; }
    }
    #endregion


    #region Session Flow: StartSession -> [SubmitSessionData,...,SubmitSessionData] -> CloseSession
    public void StartSession(JObject gameConfig)
    {
        long prescriptionConfID = -1;
        //  start session
        CoroutineHelper.Call(CPEAPIService.Api.StartGameSessionAsync(CPEModel.Api.GameUserID, CPEModel.Api.GameID, (gssResult) =>
        {
            if (gssResult.Success)
            {
                Debug.Log("Start session successful");

                CPEModel.Api.SessionID = gssResult.Data.ID; //Cache sessionID
            }
            else
            {
                Debug.LogError("Start session Fail");
            }

        }, gameConfig: gameConfig, prescriptionConfID: prescriptionConfID));
    }

    public void SubmitSessionData(JObject data, bool isCloseSession = false)
    {
        long sessionID = CPEModel.Api.SessionID;

        Utils.DebugLog("SubmitSessionData with data: " + data.ToString());
        Utils.DebugLog("SubmitSessionData: "+ isCloseSession);

        CoroutineHelper.Call(CPEAPIService.Api.PushGameSessionDataAsync(sessionID, JsonConvert.SerializeObject(data), (ssDataResult) =>
        {
            Debug.Log("Submitted successful? " + ssDataResult.Success);
            if (isCloseSession)
            {
                CloseSession(sessionID);
            }
        }, null));
    }

    public void CloseSession(long sessionID)
    {
        Debug.Log("Call CloseSession");
        CoroutineHelper.Call(CPEAPIService.Api.CloseGameSessionAsync(sessionID, 0, (ssResult) =>
        {
            Debug.Log("Submitted successful? " + ssResult.Success);
            if (ssResult.Success)
            {
                 //GetPrescriptionToday();
            }
        }));

    }
    #endregion

    public void Login(string username, string password)
    {
        CoroutineHelper.Call(CPEAPIService.Api.LoginAsync(username, password, (result) =>
            {
                // load playground
                CoroutineHelper.Call(CPEAPIService.Api.GetPlaygroundInfoByAppAsync("com.taggle.combatpd", (pgResult) =>
                {
                    if (pgResult.Success)
                    {
                        CPEModel.Api.GameID = pgResult.Data.Game.ID;
                        CPEModel.Api.AppID = pgResult.Data.Game.AppId;
                        CPEModel.Api.GameUserID = pgResult.Data.GameUser.ID;

                        StartSession(null);

                        InitializeCombatPDExercise();
                    }
                    else
                    {
                        Debug.Log("LoginAsync - Login Error");
                    }
                }));

                // load prescription today
                //GetPrescriptionToday();
            }));
    }

    public void Logout()
    {
        CoroutineHelper.Call(CPEAPIService.Api.LogoutAsync(callback:(result) =>
        {
            if (result.Success)
            {
                CloseSession(CPEModel.Api.SessionID);
            }
            else
            {
                Debug.Log("LoginAsync - Logout Error");
            }
        }));
    }

    public void InitializeCombatPDExercise()
    {
        SceneManager.UnloadSceneAsync(CPEConstant.SCENE_LOGIN);

        SceneManager.LoadSceneAsync("CombatPD_MainMenu", LoadSceneMode.Additive);
    }
}

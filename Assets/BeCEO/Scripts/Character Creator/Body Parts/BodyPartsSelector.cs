// Code written by tutmo (youtube.com/tutmo)
// For help, check out the tutorial - https://youtu.be/PNWK5o9l54w

using Firebase.Database;
using NUnit.Framework.Constraints;
using System.Threading.Tasks;
using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BodyPartsSelector : MonoBehaviour
{
    // ~~ 1. Handles Body Part Selection Updates

    // Full Character Body
    [SerializeField] private SO_CharacterBody characterBody;
    // Body Part Selections
    public BodyPartSelection[] bodyPartSelections;

    private int isFirstRunSelector = 1; // 1 - true, 0 - false 
    private int isFirstTimeSaveDataToFirebase = 1;
    /// <summary>
    /// Я намагався кожного разу змінювати дані в бд, проте не врахував, шо вона в мене не створена, адже кнопка реєстрації не нажата
    /// тому треба зберігати дані лише локально в плеєрпрефс
    /// а потім вже, коли натисну кнопку закінчення реєстрації, то перенести дані з локальних збережень в бд
    /// </summary>
    private int[] arrayIndex;

    private IEnumerator LoadCustData()
    {
        var db = FirebaseUserManager.CurrentDB;
        var user = FirebaseUserManager.CurrentUser;


        // Отримання даних поточного користувача з таблиці customize
        Task<DataSnapshot> getDataTask = db.Child("customize").Child(user.UserId).GetValueAsync();

        yield return new WaitUntil(predicate: () => getDataTask.IsCompleted);

        if (getDataTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to get customize values / task with {getDataTask.Exception}");
        }
        else
        {
            DataSnapshot snapshot = getDataTask.Result;
            if (snapshot.Exists)
            {
                // Отримано дані з таблиці customize
                /*foreach (DataSnapshot childSnapshot in snapshot.Children)
                {
                    // childSnapshot.Key - ключ, childSnapshot.Value - значення
                    //Debug.Log($"Key: {childSnapshot.Key}, Value: {childSnapshot.Value}");

                }*/
                int a0 = int.Parse(snapshot.Child("body").Value.ToString());
                int a1 = int.Parse(snapshot.Child("hair").Value.ToString());
                int a2 = int.Parse(snapshot.Child("torso").Value.ToString());
                int a3 = int.Parse(snapshot.Child("legs").Value.ToString());

                arrayIndex = new int[4]
                {
                    a0, a1, a2, a3
                };

                for (int i = 0; i < bodyPartSelections.Length; i++)
                {
                    bodyPartSelections[i].bodyPartCurrentIndex = arrayIndex[i];

                    Debug.Log("array[" + i + "] =  " + bodyPartSelections[i].bodyPartCurrentIndex);
                }
                for (int i = 0; i < bodyPartSelections.Length; i++)
                {
                    UpdateCurrentPart(i);
                }
                // Get All Current Body Parts
                for (int i = 0; i < bodyPartSelections.Length; i++)
                {
                    GetCurrentBodyParts(i);
                }
            }
            else
            {
                Debug.Log("No customize data found for the current user.");
            }
        }
    }
    private void Awake()
    {
        StartCoroutine(LoadCustData());

        /*arrayIndex = new int[4] { 
            PlayerPrefs.GetInt("indexBody"),
            PlayerPrefs.GetInt("indexHair"),
            PlayerPrefs.GetInt("indexTorso"),
            PlayerPrefs.GetInt("indexLegs")
        };*/

        //_ = FirebaseUserManager.LoadCustomizeDataCoroutine();
        // arrayIndex = FirebaseUserManager.GetCustomizeData();


        // далі встановлюємо значення 
        


        // var arrayIndex = FirebaseUserManager.dataCustomizeFromFirebase;
        // little bicycle
        /*var arrayIndex = new int[4] { 
            PlayerPrefs.GetInt("indexBody"),
            PlayerPrefs.GetInt("indexHair"),
            PlayerPrefs.GetInt("indexTorso"),
            PlayerPrefs.GetInt("indexLegs")
        };*/

        


        // update 
        //SetBodyPartIndexesFromFirebase(arrayIndex);
        
    }

   /* public void FromFirebaseManager(int[] array)
    {
        for (int i = 0; i < bodyPartSelections.Length; i++)
        {
            bodyPartSelections[i].bodyPartCurrentIndex = array[i];

            Debug.Log("array[" + i + "] =  " + bodyPartSelections[i].bodyPartCurrentIndex);
        }
        for (int i = 0; i < bodyPartSelections.Length; i++)
        {
            UpdateCurrentPart(i);
        }
        // Get All Current Body Parts
        for (int i = 0; i < bodyPartSelections.Length; i++)
        {
            GetCurrentBodyParts(i);
        }
    }*/

    public void NextBodyPart(int partIndex)
    {
        if (ValidateIndexValue(partIndex))
        {
            if (bodyPartSelections[partIndex].bodyPartCurrentIndex < bodyPartSelections[partIndex].bodyPartOptions.Length - 1)
            {
                bodyPartSelections[partIndex].bodyPartCurrentIndex++;
            }
            else
            {
                bodyPartSelections[partIndex].bodyPartCurrentIndex = 0;
            }

            UpdateCurrentPart(partIndex);
        }
    }

    public void PreviousBody(int partIndex)
    {
        if (ValidateIndexValue(partIndex))
        {
            if (bodyPartSelections[partIndex].bodyPartCurrentIndex > 0)
            {
                bodyPartSelections[partIndex].bodyPartCurrentIndex--;
            }
            else
            {
                bodyPartSelections[partIndex].bodyPartCurrentIndex = bodyPartSelections[partIndex].bodyPartOptions.Length - 1;
            }

            UpdateCurrentPart(partIndex);
        }    
    }

    private bool ValidateIndexValue(int partIndex)
    {
        if (partIndex > bodyPartSelections.Length || partIndex < 0)
        {
            Debug.Log("Index value does not match any body parts!");
            return false;
        }
        else
        {
            return true;
        }
    }

    private void GetCurrentBodyParts(int partIndex)
    {

        // Get Current Body Part Name
        bodyPartSelections[partIndex].bodyPartNameTextComponent.text = characterBody.characterBodyParts[partIndex].bodyPart.bodyPartName;
        // Get Current Body Part Animation ID
        bodyPartSelections[partIndex].bodyPartCurrentIndex = characterBody.characterBodyParts[partIndex].bodyPart.bodyPartAnimationID;
    }

    private void UpdateCurrentPart(int partIndex)
    {
        // update firebase data about customize
        FirebaseUserManager.UpdateCustomizationData(GetBodyPartArray());
        //bodyPartSelections[partIndex].bodyPartCurrentIndex = 2;
        // Update Selection Name Text
        bodyPartSelections[partIndex].bodyPartNameTextComponent.text = bodyPartSelections[partIndex].bodyPartOptions[bodyPartSelections[partIndex].bodyPartCurrentIndex].bodyPartName;
        // Update Character Body Part
        characterBody.characterBodyParts[partIndex].bodyPart = bodyPartSelections[partIndex].bodyPartOptions[bodyPartSelections[partIndex].bodyPartCurrentIndex];
    }

    public void UpdateCurrentPartJustFirebase()
    {
        FirebaseUserManager.UpdateCustomizationData(GetBodyPartArray());
    }
    /*    public void SetBodyPartIndexesFromFirebase(int[] indexes)
        {
            for (int i = 0; i < indexes.Length && i < bodyPartSelections.Length; i++)
            {
                if (indexes[i] >= 0 && indexes[i] < bodyPartSelections[i].bodyPartOptions.Length)
                {
                    bodyPartSelections[i].bodyPartCurrentIndex = indexes[i];
                    UpdateCurrentPart(i);
                }
                else
                {
                    Debug.LogError("Invalid index for body part " + i + " received from Firebase.");
                }
            }
        }

        private void SetBodia()
        {
            for (int i = 0; i < bodyPartSelections.Length; i++)
            {
                UpdateCurrentPart(i);
            }
        }
    */
    private void SetBodia()
    {
        for (int i = 0; i < bodyPartSelections.Length; i++)
        {
            UpdateCurrentPart(i);
        }
    }
    public int[] GetBodyPartArray()
    {
        int[] tempArray = new int[bodyPartSelections.Length];
        for (int i = 0; i < bodyPartSelections.Length; i++)
        {
            tempArray[i] = bodyPartSelections[i].bodyPartCurrentIndex;
        }

        // and save localy
        // little to reinvent the bicycle
        /*PlayerPrefs.SetInt("indexBody", tempArray[0]);
        PlayerPrefs.SetInt("indexHair", tempArray[1]);
        PlayerPrefs.SetInt("indexTorso", tempArray[2]);
        PlayerPrefs.SetInt("indexLeg", tempArray[3]);*/
        return tempArray;
    }
}

[System.Serializable]
public class BodyPartSelection
{
    public string bodyPartName;
    public SO_BodyPart[] bodyPartOptions;
    public Text bodyPartNameTextComponent;
    public int bodyPartCurrentIndex;
}

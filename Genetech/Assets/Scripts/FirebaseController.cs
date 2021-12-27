using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Firebase;
using Firebase.Auth;
using Firebase.Database;

public class FirebaseController : MonoBehaviour
{
    public DependencyStatus dependencyStatus;
    public DatabaseReference reference;
    public FirebaseAuth auth;

    private void Awake()
    {
        CheckFirebaseInititalization();
    }

    public void CheckFirebaseInititalization(){
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>{
            dependencyStatus = task.Result;
            if(dependencyStatus == DependencyStatus.Available){
                Initialize();
            }else{
                Debug.LogError(System.String.Format("Could not resolve all Firebase dependencies: {0}", dependencyStatus));
            }
        });
    }

    public void Initialize(){
        auth = FirebaseAuth.DefaultInstance;
        reference = FirebaseDatabase.DefaultInstance.RootReference;
        Debug.Log(reference);   
        reference.Child("Names").SetValueAsync("Aniel");
    }

}
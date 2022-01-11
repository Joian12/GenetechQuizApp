using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Firebase;
using Firebase.Extensions;
using Firebase.Auth;
using Firebase.Database;
using TMPro;

public class FirebaseController : MonoBehaviour
{   
    public static FirebaseController Instance;
    public DependencyStatus dependencyStatus;
    public DatabaseReference reference;
    public FirebaseAuth auth;
    public FirebaseUser user;
    public string message;
    private string username, email, password, erroMessage;
    private string emailLog, passwordLog;
    public bool loginComplete;
    private void Awake()
    {   
        CheckFirebaseInititalization();
        Instance = this;
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
        user = FirebaseAuth.DefaultInstance.CurrentUser;
        reference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void RegisterUser(string username, string email, string password){
        this.username = username;
        this.email = email;
        this.password = password;
        StartCoroutine("Register");
    }
    private IEnumerator Register(){

         var register = auth.CreateUserWithEmailAndPasswordAsync(email, password);

            yield return new WaitUntil(predicate: () => register.IsCompleted);

            Debug.Log("Done");
            if(register.Exception != null){
                FirebaseException firebaseEx = register.Exception.GetBaseException() as FirebaseException;
                AuthError error = (AuthError)firebaseEx.ErrorCode;

                message = "Registration Failed";
               
                switch (error){
                    case AuthError.MissingEmail:
                        message = "Missing Email";
                        break;
                    case AuthError.EmailAlreadyInUse:
                        message = "Email already in use";
                        break;
                    case AuthError.MissingPassword:
                        message = "Missing Password";
                        break;
                }
            }
            user = register.Result;

            if(user != null){
                UserProfile profile = new UserProfile{ DisplayName = username};
                        
                var profileTask = user.UpdateUserProfileAsync(profile);

                yield return new WaitUntil(predicate: () => profileTask.IsCompleted);
                Debug.Log(profile.DisplayName);
                
            }else{
                Debug.Log("User is null");
            }

    }

    public void LogInUser(string emailLog, string passwordLog){
        this.emailLog = emailLog;
        this.passwordLog = passwordLog;
        StartCoroutine("LogIn");
    }

    public IEnumerator LogIn(){
        if(emailLog.Length > 1 && passwordLog.Length > 1){
            var login = auth.SignInWithEmailAndPasswordAsync(emailLog, passwordLog);

            yield return new WaitUntil(predicate: () => login.IsCompleted);

            if(login.Exception != null){
                FirebaseException firebaseEx = login.Exception.GetBaseException() as FirebaseException;
                AuthError error = (AuthError)firebaseEx.ErrorCode;

                switch (error){
                    case AuthError.InvalidEmail:
                        erroMessage = "Invalid Email";
                        break;
                    case AuthError.MissingEmail:
                        erroMessage = "Missing Email";
                        break;
                    case AuthError.MissingPassword:
                        erroMessage = "Missing Password";
                        break;
                    case AuthError.UserMismatch:
                        erroMessage = "Mismatch Information";
                        break;
                }
            }

            auth.SignInWithEmailAndPasswordAsync(emailLog, passwordLog).ContinueWith(task => {
            if (task.IsCanceled) {
                Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted) {
                Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                return;
            }

            if(task.IsCompleted){
                Firebase.Auth.FirebaseUser newUser = task.Result;
                loginComplete = true;
                Debug.Log("Login Successful");
            }
            });
        }else
            erroMessage = "Fill Up Everything";     
    }
    public void GetOnlineScore(TextMeshProUGUI playerName, TextMeshProUGUI playerScore){
     reference.Child("Player").Child(FirebaseController.Instance.user.DisplayName).Child("Score").GetValueAsync().ContinueWithOnMainThread(task => {
        if (task.IsFaulted) {
          Debug.Log("Failed");
        }
        else if (task.IsCompleted) {
          DataSnapshot snapshot = task.Result;
          Debug.Log("Success");
          playerName.text = FirebaseController.Instance.user.DisplayName;
          playerScore.text = snapshot.GetRawJsonValue();
        }
      });
    }
}
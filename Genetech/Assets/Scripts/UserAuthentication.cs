using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserAuthentication : MonoBehaviour
{
    //Register
    public InputField username, email, password;
    public InputField emailLogin, passwordLogin;
    public Menu menu;
    FirebaseController firebaseController;
    void Start()
    {
        firebaseController = FirebaseController.Instance;
    }

    private void Update() {
        if(firebaseController.loginComplete)
            menu.GoToStart();
    }

    public void RegisterButton(){
        FirebaseController.Instance.RegisterUser(username.text, email.text, password.text);
    }

    public void LoginButton(){
        FirebaseController.Instance.LogInUser(emailLogin.text, passwordLogin.text);
        
    }
}

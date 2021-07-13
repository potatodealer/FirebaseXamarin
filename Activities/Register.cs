using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firebase.Auth;
using Firebase.Firestore;
using FirebaseTest.EventListeners;
using FirebaseTest.Helpers;
using Java.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FirebaseTest.Activities
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class Register : Activity
    {
        string fullname, email, password, confirmPassword;
        Button registerButton;
        EditText fullNameText, emailText, passwordText, confirmPasswordText;
        FirebaseAuth mAuth;
        TextView clickToLogin;
        TaskCompletionListeners taskCompletionListeners = new TaskCompletionListeners();
        FirebaseFirestore database;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Register);

            fullNameText = FindViewById<EditText>(Resource.Id.fullnameText);
            emailText = FindViewById<EditText>(Resource.Id.emailRegister);
            passwordText = FindViewById<EditText>(Resource.Id.passwordRegister);
            confirmPasswordText = FindViewById<EditText>(Resource.Id.confirmPassword);
            mAuth = AppDataHelper.GetFirebaseAuth();
            database = AppDataHelper.GetFirestore();
            registerButton = FindViewById<Button>(Resource.Id.registerBtn);
            registerButton.Click += RegisterButton_Click;
            clickToLogin = FindViewById<TextView>(Resource.Id.loginClick);
            clickToLogin.Click += ClickToLogin_Click;

            
        }

        private void ClickToLogin_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(Login));
            Finish();
        }

        private void RegisterButton_Click(object sender, EventArgs e)
        {
            fullname = fullNameText.Text;
            email = emailText.Text;
            password = passwordText.Text;
            confirmPassword = confirmPasswordText.Text;

            if(fullname.Length < 4)
            {
                Toast.MakeText(this, "Please enter a valid name", ToastLength.Short).Show();
                return;
            }
            else if (!email.Contains("@"))
            {
                Toast.MakeText(this, "Please enter a valid email", ToastLength.Short).Show();
                return;
            }
            else if(password.Length < 8)
            {
                Toast.MakeText(this, "Please enter a valid password", ToastLength.Short).Show();
                return;
            }
            else if(password != confirmPassword)
            {
                Toast.MakeText(this, "Passwords does not match", ToastLength.Short).Show();
                return;
            }

            mAuth.CreateUserWithEmailAndPassword(email, password).AddOnSuccessListener(this, taskCompletionListeners)
                .AddOnFailureListener(this, taskCompletionListeners);

            taskCompletionListeners.Success += (success, args) =>
            {
                HashMap userMap = new HashMap();
                userMap.Put("email", email);
                userMap.Put("fullname", fullname);
                userMap.Put("tipoUsuario", "vendedor");

                DocumentReference userReference = database.Collection("users").Document(mAuth.CurrentUser.Uid);
                userReference.Set(userMap);
                Toast.MakeText(this, "Registro exitoso!", ToastLength.Short).Show();
            };

            taskCompletionListeners.Failure += (failure, args) =>
            {
                Toast.MakeText(this, "Registration Failed : " + args.Cause, ToastLength.Short).Show();
            };
        }
    }
}
using Android.App;
using Android.Content;
using Android.Gms.Tasks;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firebase.Auth;
using FirebaseTest.EventListeners;
using FirebaseTest.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FirebaseTest.Activities
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = false)]
    public class Login : Activity
    {
        EditText emailText, passwordText;
        FirebaseAuth mAuth;
        Button loginButton;
        TextView clickToRegister;
        TaskCompletionListeners taskCompletionListeners = new TaskCompletionListeners();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Login);

            emailText = FindViewById<EditText>(Resource.Id.emailLogin);
            passwordText = FindViewById<EditText>(Resource.Id.passwordLogin);
            loginButton = FindViewById<Button>(Resource.Id.loginBtn);
            clickToRegister = FindViewById<TextView>(Resource.Id.signupClick);
            mAuth = AppDataHelper.GetFirebaseAuth();
            loginButton.Click += LoginButton_Click;
            clickToRegister.Click += ClickToRegister_Click;

            // Create your application here
        }

        private void ClickToRegister_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(Register));
            Finish();
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            string email, password;
            email = emailText.Text;
            password = passwordText.Text;

            if (!email.Contains("@"))
            {
                Toast.MakeText(this, "Please enter a valid email", ToastLength.Short).Show();
                return;
            }
            else if(password.Length < 8)
            {
                Toast.MakeText(this, "Please enter a valid password", ToastLength.Short).Show();
                return;
            }

            mAuth.SignInWithEmailAndPassword(email, password).AddOnSuccessListener(taskCompletionListeners)
                .AddOnFailureListener(taskCompletionListeners);

            taskCompletionListeners.Success += (success, args) =>
            {
                StartActivity(typeof(MainActivity));
                Toast.MakeText(this, "Login was successful", ToastLength.Short).Show();
            };

            taskCompletionListeners.Failure += (failure, args) =>
            {
                Toast.MakeText(this, "Login failed: " + args.Cause , ToastLength.Short).Show();
            };

        }
    }
}
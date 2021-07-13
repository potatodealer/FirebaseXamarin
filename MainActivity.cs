using Android.App;
using Android.Gms.Tasks;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;
using Firebase.Firestore;
using FirebaseTest.Helpers;
using Java.Lang;

namespace FirebaseTest
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = false)]
    public class MainActivity : AppCompatActivity, IOnSuccessListener
    {
        TextView tipoUsario;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            tipoUsario = FindViewById<TextView>(Resource.Id.tipoUsuario);
            setUserType();
        }

        public void setUserType()
        {
            if(AppDataHelper.GetFirebaseAuth().CurrentUser != null)
            {
                AppDataHelper.GetFirestore().Collection("users").Document(AppDataHelper.GetFirebaseAuth().CurrentUser.Uid).Get()
                    .AddOnSuccessListener(this);
            }
            else
            {
                Toast.MakeText(this, "mAuth Failed", Android.Widget.ToastLength.Short).Show();
            }
        }

        public void OnSuccess(Java.Lang.Object result)
        {
            DocumentSnapshot snap = (DocumentSnapshot)result;
            if (snap.Exists())
            {
                string userT = snap.Get("tipoUsuario").ToString();
                tipoUsario.Text = userT;
                Toast.MakeText(this, userT, ToastLength.Short).Show();
            }
            else
            {
                Toast.MakeText(this, "Snap Failed", ToastLength.Short).Show();
            }
        }
    }
}
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Firebase;
using Firebase.Firestore;
using Firebase.Auth;

namespace FirebaseTest.Helpers
{
    public static class AppDataHelper
    {
        public static FirebaseAuth GetFirebaseAuth()
        {
            var app = FirebaseApp.InitializeApp(Application.Context);
            FirebaseAuth mAuth;

            if(app == null)
            {
                var options = new FirebaseOptions.Builder()
                    .SetProjectId("fir-test-76116")
                    .SetApplicationId("fir-test-76116")
                    .SetApiKey("AIzaSyDB3Irr9Dk-tOpne6XaH5NlJ1SwLdQ4AJI")
                    .SetDatabaseUrl("fir-test-76116.firebaseapp.com")
                    .SetStorageBucket("fir-test-76116.appspot.com")
                    .Build();

                app = FirebaseApp.InitializeApp(Application.Context, options);
                mAuth = FirebaseAuth.Instance;
            }
            else
            {
                mAuth = FirebaseAuth.Instance;
            }

            return mAuth;
        }
        public static FirebaseFirestore GetFirestore()
        {
            var app = FirebaseApp.InitializeApp(Application.Context);
            FirebaseFirestore database;

            if (app == null)
            {
                var options = new FirebaseOptions.Builder()
                    .SetProjectId("fir-test-76116")
                    .SetApplicationId("fir-test-76116")
                    .SetApiKey("AIzaSyDB3Irr9Dk-tOpne6XaH5NlJ1SwLdQ4AJI")
                    .SetDatabaseUrl("fir-test-76116.firebaseapp.com")
                    .SetStorageBucket("fir-test-76116.appspot.com")
                    .Build();

                app = FirebaseApp.InitializeApp(Application.Context, options);
                database = FirebaseFirestore.Instance;
            }
            else
            {
                database = FirebaseFirestore.Instance;
            }

            return database;
        }
    }
}
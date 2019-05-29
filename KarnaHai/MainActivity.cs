using Android.App;
using Android.OS;
using System.Collections.Generic;
using Android.Content;
using Android.Views;
using Android.Runtime;
using Android.Widget;

namespace KarnaHai
{
    [Activity(Label = "Karna Hai", Theme = "@style/AppTheme", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : ListActivity
    {

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            // Set our view from the "main" layout resource
            //SetContentView(Resource.Layout.activity_main);
        }
    }
}
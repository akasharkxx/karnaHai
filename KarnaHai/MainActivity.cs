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
        //a list to hold items for list
        public List<string> Items { get; set; }

        //This array adapter is used to connect data to the listview
        ArrayAdapter<string> adapter;

        //setting shared references for saving todo list items
        ISharedPreferences prefs = Application.Context.GetSharedPreferences("TODO_DATA", FileCreationMode.Private);


        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            //Initalize list
            Items = new List<string>();

            //load any existing list items from shared prefrences
            LoadList();
            //Add the list of items to the listview
            adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItemMultipleChoice, Items);
            ListAdapter = adapter; //builtin list adapter


            //getting clicks form add button
            Button addButton = FindViewById<Button>(Resource.Id.newItemButton);
            addButton.Click += delegate
            {
                //button click code

            
                //add item in the text box


                //no null item should be added


                //add new item to the list


                //add this new item to adapter list

                //tell listview about adapter list

                //clear out textbox

                //update stored key/value pairs ni shared preferences
            };
        }//end of OnCreate
        
        //this method loads items from shared preferences and fills the list

        public void LoadList(){
            //first find out how many item are i shared preferences
            //use itemCount key to find out
            int count = prefs.GetInt("itemCount", 0); //saves 0

            //loop throught items and add them to the list
            if(count > 0)
            {
                Toast.MakeText(this, "Getting saved items...", Android.Widget.ToastLength.Short).Show();

                for(int i = 0; i <= count; i++)
                {
                    string item = prefs.GetString(i.ToString(), null);
                    if(item != null)
                    {
                        Items.Add(item);
                    }
                }//end of for loop
            }


        }//end of LoadList
    }//end of Class

}
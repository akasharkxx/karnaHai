using Android.App;
using Android.OS;
using System.Collections.Generic;
using Android.Content;
using Android.Views;
using Android.Runtime;
using Android.Widget;

namespace KarnaHai
{
    [Activity(Label = "Karna Hai", Theme = "@style/AppTheme", MainLauncher = true/*, Icon = "@drawable/icon"*/, WindowSoftInputMode = SoftInput.AdjustPan | SoftInput.StateHidden)]
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


                //add item from the text box
                EditText itemText = FindViewById<EditText>(Resource.Id.itemText);
                string item = itemText.Text;

                //no null item should be added so check for it
                if(item == "" || item == null)
                {
                    //this is blank item
                    return;
                }

                //add new item to the list
                Items.Add(item);

                //add this new item to adapter list
                adapter.Add(item);

                //tell listview about adapter list
                adapter.NotifyDataSetChanged(); 

                //clear out textbox
                itemText.Text = "";

                //update stored key/value pairs ni shared preferences
                UpdateStoredData();

            };
        }//end of OnCreate

        //this is a method that is called when a item in list is clicked

        protected override void OnListItemClick(ListView l, View v, int position, long id)
        {
            base.OnListItemClick(l, v, position, id);
            //when user clicks then we remove from shared preferences

            RunOnUiThread(() =>
            {
                AlertDialog.Builder builder;
                builder = new AlertDialog.Builder(this);
                builder.SetTitle("Comfirm");
                builder.SetMessage("Are you done with this task");
                builder.SetCancelable(true);

                builder.SetPositiveButton("OK", delegate
                {
                    //remove item
                    var item = Items[position];
                    Items.Remove(item);
                    adapter.Remove(item);

                    //reset the listview so non is checked
                    l.ClearChoices();
                    l.RequestLayout();

                    //Update the data stored in shared preferences
                    UpdateStoredData();
                });

                builder.SetNegativeButton("Cancel", delegate
                {
                    return;
                });

                //this launches "modal" popup
            });
        }

        //this method loads items from shared preferences and fills the list

        public void LoadList(){
            //first find out how many item are i shared preferences
            //use itemCount key to find out
            int count = prefs.GetInt("itemCount", 0); //saves 0

            //loop throught items and add them to the list
            if (count > 0)
            {
                Toast.MakeText(this, "Getting saved items...", Android.Widget.ToastLength.Short).Show();

                for (int i = 0; i <= count; i++)
                {
                    string item = prefs.GetString(i.ToString(), null);
                    if (item != null)
                    {
                        Items.Add(item);
                    }
                }//end of for loop
            }
        }//end of LoadList

        //This method updates the stored key/value pairs in the shared preferences
        public void UpdateStoredData()
        {
            //remove the current items in shared preferences
            ISharedPreferencesEditor editor = prefs.Edit();
            editor.Clear();
            editor.Commit();

            //add all items in the shared preferences
            //if app is close we can reopen the list
            editor = prefs.Edit();

            //ke that keep track of how many items is stores in SP
            editor.PutInt("itemCount", Items.Count);

            int counter = 0;
            //loop through each item and add it
            //list to be writtten
            foreach(string item in Items)
            {
                editor.PutString(counter.ToString(), item);
                counter++;
            }

            //Write to shared preferences

            editor.Apply();

        }//end of method
    }//end of Class

}
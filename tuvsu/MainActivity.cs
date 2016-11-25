using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using tuvsu.DataHelper;

namespace tuvsu
{
    [Activity(Label = "tuvsu", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {

        DataBase db;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);

            //SQLite
            db = new DataBase();
            db.createDataBase();
            //Проверка БД на активного пользователя
            bool resDB = db.selectTable();
            if (resDB)
            {
                
                Intent intent = new Intent(this, typeof(mActivity.NewsActivity));
                StartActivity(intent);
                Finish();
            }
            else
            {
                Intent intent = new Intent(this, typeof(mActivity.AuthActivity));
                StartActivity(intent);
                Finish();
            }

        }
    }
}


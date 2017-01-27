using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Text.Method;
using System.Net;
using Newtonsoft.Json;
using Android.Util;

namespace tuvsu.mActivity
{
    [Activity(Label = "AuthActivity")]
    public class AuthActivity : Activity
    {
        DataHelper.DataBase db;

        ProgressBar progres;
        EditText log;
        EditText pass;
        Button stud;
        Button guest;
        TextView errorText;
        CheckBox visiblePass;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Auth);

            progres = FindViewById<ProgressBar>(Resource.Id.progressBar1);
            log = FindViewById<EditText>(Resource.Id.login);
            pass = FindViewById<EditText>(Resource.Id.password);
            stud = FindViewById<Button>(Resource.Id.authStud);
            guest = FindViewById<Button>(Resource.Id.authGuest);
            errorText = FindViewById<TextView>(Resource.Id.errorText);
            visiblePass = FindViewById<CheckBox>(Resource.Id.checkBoxPass);

            progres.Visibility = ViewStates.Invisible;
            errorText.Visibility = ViewStates.Invisible;

            pass.InputType = Android.Text.InputTypes.TextVariationPassword | Android.Text.InputTypes.ClassText;

            //Показать пароль не работает
            visiblePass.CheckedChange += (sender, e) =>
            {
                if (e.IsChecked)
                {

                }
                else
                {
                    pass.TransformationMethod = PasswordTransformationMethod.Instance;
                }
            };

            //Войти как студент
            stud.Click += (object sender, EventArgs e) =>
            {
                if (log.Text.Length > 0 && pass.Text.Length > 0)
                {
                    progres.Visibility = ViewStates.Visible;

                    //Проверка пользователя
                    string mLogin = log.Text;
                    string mPass = pass.Text;
                    try
                    {
                        WebClient client = new WebClient();
                        Toast.MakeText(this, mLogin + " - " + mPass, ToastLength.Short);
                        Uri uri = new Uri("http://tuvsu.ru/mobile/auth.php?get=1&log=" + mLogin + "&pass=" + mPass);
                        client.DownloadDataAsync(uri);
                        client.DownloadDataCompleted += ConJson;
                    }
                    catch (Exception ex)
                    {
                        Toast.MakeText(this, ex.ToString() + " Проверьте подключение к интернету!", ToastLength.Short).Show();
                    }
                }

            };
            //End войти как студент

            //Войти как гость
            guest.Click += (sender, e) =>
            {
                Intent intent = new Intent(this, typeof(mActivity.NewsActivity));
                progres.Visibility = ViewStates.Gone;
                StartActivity(intent);
                Finish();
                //Intent intent = new Intent(this, typeof(Activitys.MenuActivity));
                // StartActivity(intent);
            };
        }
        //ПОлучение данных из сервера
        private void ConJson(object sender, DownloadDataCompletedEventArgs e)
        {
            RunOnUiThread(() =>
            {
                try
                {
                    string json = Encoding.UTF8.GetString(e.Result);
                    Toast.MakeText(this, json, ToastLength.Short);
                    
                    if (json == "1")
                    //if(json.Length > 0)
                    {
                        tuvsu.mModel.pUsers users = new tuvsu.mModel.pUsers { log_users = log.Text, pass_users = pass.Text };
                        //db.insertIntoTable(users); Ошибка работы с локальной бд

                        Intent intent = new Intent(this, typeof(mActivity.NewsActivity));
                        progres.Visibility = ViewStates.Gone;
                        StartActivity(intent);
                        Finish();
                    }
                    else
                    {
                        progres.Visibility = ViewStates.Gone;
                        errorText.Visibility = ViewStates.Visible;
                    }
                    
                }
                catch (Exception ex)
                {
                    Toast.MakeText(this, ex.ToString() + " Проверьте подключение к интернету!", ToastLength.Short).Show();
                    Log.Info("Ошибка 1", ex.ToString());
                }
                finally
                {
                    progres.Visibility = ViewStates.Gone;
                }


            });
        }

    }
}
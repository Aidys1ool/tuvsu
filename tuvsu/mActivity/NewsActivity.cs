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
using SQLite;
using System.Net;
using Newtonsoft.Json;


namespace tuvsu.mActivity
{
    [Activity(Label = "NewsActivity")]
    public class NewsActivity : Activity
    {
        ListAdapter.NewsAdapter adapter; //???????????Название папки не тот должен быль mAdapter не знаю почему
        private List<mModel.pNews> list;

        private ListView listView;
        private Button btnExit;

        DataHelper.DataBase db;

        private bool pos = false;

        List<mModel.pNews> a;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.News);

            listView = FindViewById<ListView>(Resource.Id.listView);
            btnExit = FindViewById<Button>(Resource.Id.btnExit);

            //SQLite
            //При нажатии btnExit
            db = new DataHelper.DataBase();
            btnExit.Click += (s, e) =>
            {
                mModel.pUsers user = new mModel.pUsers();
                bool res = db.deleteTable();
                if(res)
                {
                    Intent intent = new Intent(this, typeof(AuthActivity));
                    StartActivity(intent);
                    Finish();
                }
                else
                {
                    //Ошиба удаления данных
                }
            };
            //end

            WebClient client = new WebClient();
            Uri uri = new Uri("http://tuvsu.ru/mobile/news.php");
            client.DownloadDataAsync(uri);
            client.DownloadDataCompleted += NewsDataDownload;

            list = new List<mModel.pNews>();
            a = new List<mModel.pNews>();

            //Событие scroll listView
            
            listView.Scroll += (sender, e) =>
            {
                try
                {
                    int Sum = e.VisibleItemCount;
                    int Top = e.FirstVisibleItem;
                    int bottom = Sum + (Top - 1);

                    //Если мы достигли предпоследнего элемента то: =>
                    if (pos == false && bottom == e.TotalItemCount - 1)
                    {
                        WebClient clientMore = new WebClient();
                        Uri addUri = new Uri("http://tuvsu.ru/mobile/more_news.php?id=" + list[Convert.ToInt32(e.TotalItemCount) - 1].new_uid);
                        clientMore.DownloadDataAsync(addUri);
                        clientMore.DownloadDataCompleted += NewsAddDataDownload;
                        pos = true;
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine("Ошибка scroll" + ex.ToString());
                }
            };

            //События детального просмотра новостей
            listView.ItemClick += DetailNews;
            
        }

        //Детальный просмотр новостей
        private void DetailNews(object sender, AdapterView.ItemClickEventArgs e)
        {
            Intent intent = new Intent(this, typeof(NewsDetailActivity));
            intent.PutExtra("id", list[e.Position].new_uid);
            intent.PutExtra("title", list[e.Position].new_title);
            intent.PutExtra("content", list[e.Position].new_content_mobile);
            intent.PutExtra("photo", list[e.Position].new_photo);
            StartActivity(intent);
        }

        //Загрузка news при открытии страницы новостей
        private void NewsDataDownload(object sender, DownloadDataCompletedEventArgs e)
        {

            RunOnUiThread(() =>
            {
                string json = Encoding.UTF8.GetString(e.Result);
                list = JsonConvert.DeserializeObject<List<mModel.pNews>>(json);

                adapter = new ListAdapter.NewsAdapter(this, list);
                listView.Adapter = adapter;
            });
        }

        //Загрузка еще новостей
        private void NewsAddDataDownload(object sender, DownloadDataCompletedEventArgs e)
        {
            RunOnUiThread(() =>
            {
                string json = Encoding.UTF8.GetString(e.Result);
                a = JsonConvert.DeserializeObject<List<mModel.pNews>>(json);
                Toast.MakeText(this, a.Count.ToString(), ToastLength.Short).Show();
                for (int i = 0; i < a.Count; i++)
                {
                    list.Add(a[i]);
                    adapter.NotifyDataSetChanged(); //Обновлляем адаптер
                }
                pos = false;
            });
        }

    }
}
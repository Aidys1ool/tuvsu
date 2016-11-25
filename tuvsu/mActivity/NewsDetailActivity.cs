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

namespace tuvsu.mActivity
{
    [Activity(Label = "NewsDetailActivity")]
    public class NewsDetailActivity : Activity
    {
        private TextView title;
        private TextView content;
        private ImageView photo;
        private ListView listView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.NewsDetail);

            title = FindViewById<TextView>(Resource.Id.title);
            content = FindViewById<TextView>(Resource.Id.content);
            photo = FindViewById<ImageView>(Resource.Id.photo);
            listView = FindViewById<ListView>(Resource.Id.listImages);

            int id = Intent.GetIntExtra("id", 1);

            title.Text = Intent.GetStringExtra("title") ?? "Error";
            content.Text = Intent.GetStringExtra("content") ?? "Error";

            //Photo URL
            mModel.ImageDownload imageDownload = new mModel.ImageDownload();
            var imageBitmap = imageDownload.GetImageBitmapFromUrl("http://tuvsu.ru/files/news/" + Intent.GetStringExtra("photo")); //??
            photo.SetImageBitmap(imageBitmap);
        }
    }
}
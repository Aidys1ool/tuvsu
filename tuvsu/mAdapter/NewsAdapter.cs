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

using tuvsu.mModel;
using Android.Graphics;
using System.Net;

namespace tuvsu.ListAdapter
{
    class NewsAdapter : BaseAdapter<pNews>
    {
        private List<pNews> newsList;
        private Context mContext;

        public NewsAdapter(Context context, List<pNews> list)
        {
            newsList = list;
            mContext = context;
        }

        public override pNews this[int position]
        {
            get
            {
                return newsList[position];
            }
        }

        public override int Count
        {
            get
            {
                return newsList.Count;
            }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View row = convertView;
            if(row == null)
            {
                //row = LayoutInflater.From(mContext).Inflate(Resource.Layout.ListViewNewsRow, null, false);
                row = LayoutInflater.From(mContext).Inflate(Resource.Layout.NewsRow, null, false);
            }
            //Загрузка изображение по URL
            //ImageView image = row.FindViewById<ImageView>(Resource.Id.imageView1);
            //var imageBitmap = GetImageBitmapFromUrl("http://tuvsu.ru/files/news/" + newsList[position].new_photo);
            //image.SetImageBitmap(imageBitmap);

            //Android.Net.Uri url = Android.Net.Uri.Parse(newsList[position].new_photo);
            //image.SetImageURI(Android.Net.Uri.Parse("http://tuvsu.ru/files/news/" + newsList[position].new_photo));

            TextView title = row.FindViewById<TextView>(Resource.Id.titleNews);
            title.Text = newsList[position].new_title;

            //TextView content = row.FindViewById<TextView>(Resource.Id.contentNews);
            //content.Text = newsList[position].new_content;

            //TextView uid = row.FindViewById<TextView>(Resource.Id.news_id);
            //uid.Text = newsList[position].new_uid.ToString();

            return row;
        }

        /*
        public Bitmap GetImageBitmapFromUrl(string url)
        {
            Bitmap imageBitmap = null;

            using (var webClient = new WebClient())
            {
                var imageBytes = webClient.DownloadData(url);
                if (imageBytes != null && imageBytes.Length > 0)
                {
                    imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
                }
            }

            return imageBitmap;
        }
        */
    }
}
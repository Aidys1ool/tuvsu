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

namespace tuvsu.mModel
{
    public class pUsers
    {
        [PrimaryKey, AutoIncrement]
        public int id_users { get; set; }
        public string log_users { get; set; }
        public string pass_users { get; set; }
    }
}
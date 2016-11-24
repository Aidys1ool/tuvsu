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
using Android.Util;
using tuvsu.mModel;

namespace tuvsu.DataHelper
{
    public class DataBase
    {
        string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);

        //Создание БД
        public bool createDataBase()
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "pUsers.db")))
                {
                    connection.CreateTable<pUsers>();
                    return true;
                }
            }
            catch(SQLiteException ex)
            {
                Log.Info("SQLite Error", ex.ToString());
                return false;
            }
        }

        public bool insertIntoTable(pUsers user)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "pUsers.db")))
                {
                    connection.Insert(user);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLite Error", ex.ToString());
                return false;
            }
        }

        public List<pUsers> selectTable1(pUsers user)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "pUsers.db")))
                {
                    return connection.Table<pUsers>().ToList(); 
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLite Error", ex.ToString());
                return null;
            }
        }

        public bool selectTable()
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "pUsers.db")))
                {
                    List<pUsers> list = connection.Table<pUsers>().ToList();
                    if (list.Count > 0)
                    {
                        Log.Info(list.ToString(), list.Count.ToString());
                        return true;
                    }
                    else
                        return false;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLite Error", ex.ToString());
                return false;
            }
        }

        public bool deleteTable()
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "pUsers.db")))
                {
                    connection.DeleteAll<pUsers>();
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLite Error", ex.ToString());
                return false;
            }
        }

    }
}
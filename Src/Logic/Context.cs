﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;

namespace Logic
{
	/// <summary>
	/// Klasa odpowiadająca za przechowywanie danych użytkownika.
	/// Jest to Singleton w którym zawsze mają być aktualne(!) dane zalogowanego użytkownika.
	/// </summary>
    public class Context
    {
        private string userLogin;
        private string userPassword;
        private ICollection<string> roles;
        private static IDatabase database;
        private FormManager formManager;
        private ContentManager contentManager;
        private ViewToGridManager viewToGridManager;
        private DataGridViewCellEventArgs actualGridArguments;
        private DataGridView actualGridView;
        private DataTable actualSchema;

        public DataTable ActualSchema
        {
            get { return actualSchema; }
            set { actualSchema = value; }
        }

        public DataGridView ActualGridView
        {
            get { return actualGridView; }
            set { actualGridView = value; }
        }

        public DataGridViewCellEventArgs ActualGridArguments
        {
            get { return actualGridArguments; }
            set { actualGridArguments = value; }
        }

        public ContentManager ContentManager
        {
            get { return contentManager; }
            set { contentManager = value; }
        }

        public ViewToGridManager ViewToGridManager
        {
            get { return viewToGridManager; }
            set { viewToGridManager = value; }
        }

        public FormManager FormManager
        {
            get { return formManager; }
            set { formManager = value; }
        }

        public IDatabase Database
        {
            get { return Context.database; }
            set { Context.database = value; }
        }
		
		/// <summary>
		/// Konstruktor. 
		/// </summary>
		public Context(IDatabase dataBase ,FormManager formManager)
        {
			this.Database = dataBase;
			this.FormManager = formManager;
            this.contentManager = new ContentManager();
			this.ViewToGridManager = new ViewToGridManager(dataBase);
            this.userLogin = null;
            this.userPassword = null;
            this.roles = null;
        }

        public string UserLogin
        {
            get { return userLogin; }
            set { userLogin = value; }
        }

        public string UserPassword
        {
            get { return userPassword; }
            set { userPassword = value; }
        }

        public ICollection<string> UserRoles
        {
            get { return roles; }
            set { roles = value; }
        }


    }
}
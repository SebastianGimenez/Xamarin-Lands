using System;
using System.Collections.Generic;
using System.Text;

namespace Land.ViewModels
{
    public class MainViewModel
    {
        #region Singleton
        private static MainViewModel instance;
        public static MainViewModel getInstance()
        {
            if (instance == null)
            {
                return new MainViewModel();
            }
            return instance;
        }

        #endregion



        public LoginViewModel Login
        {
            get;
            set;
        }

        public LandsViewModel Lands
        {
            get;
            set;
        }

        public LandViewModel Land
        {
            get;
            set;
        }

        public MainViewModel()
        {
            instance = this;
            this.Login = new LoginViewModel();
        }

    }
}

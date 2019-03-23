using GalaSoft.MvvmLight.Command; //se instala con nuGets 
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;


namespace Land.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {

        #region Attributes
        private string email;
        private string password;
        private bool isRunning;
        private bool isEnabled;


        #endregion

        #region Propiedades
        public string Email
        {
            get { return email; }
            set
            {
                SetValue(ref email, value);
            }
        }

        public string Password
        {
            get { return password; }
            set
            {
                SetValue(ref password, value);
            }
        }

        public bool IsRunning
        {
            get { return isRunning; }
            set
            {
                SetValue(ref isRunning, value);
            }
        }

        public bool IsRemembered
        {
            get;
            set;
        }

        public bool IsEnabled
        {
            get { return isEnabled; }
            set
            {
                SetValue(ref isEnabled, value);
            }
        }
# endregion


        #region Comandos
        public ICommand LoginCommand
        {
            get { return new RelayCommand(Login); }
        }


        private async void Login()
        {
            if (string.IsNullOrEmpty(this.Email))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Debes ingresar un Email",
                    "Aceptar");
                return;
            }
            if (string.IsNullOrEmpty(this.Password))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Debes ingresar un Password",
                    "Aceptar");
                return;
            }

            this.IsRunning = true;
            this.IsEnabled = false;

            if (this.Email != "sebygimenez03@yahoo.com.ar" || this.Password != "1234")
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Email o psw incorrecto",
                    "Aceptar");
                this.Password = string.Empty;

                return;
            }
            this.IsRunning = false;
            this.IsEnabled = true;

            this.Email = string.Empty;
            this.Password = string.Empty;
            MainViewModel.getInstance().Lands = new LandsViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new Views.LandsPage());
        }
        #endregion


        #region constructores
        public LoginViewModel()
        {
            this.IsRemembered = true;
            this.IsEnabled = true;
            this.Email = "sebygimenez03@yahoo.com.ar";
            this.Password = "1234";

        }
        #endregion

    }
}

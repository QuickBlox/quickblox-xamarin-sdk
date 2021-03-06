﻿using QbChat.Pcl;
using QbChat.UWP.Views;
using System;
using System.Collections.Generic;
using Windows.UI.Popups;

namespace QbChat.UWP.ViewModels
{
    public class DefaultLoginViewModel : ViewModel
    {
        private string login;
        private string password;
        private List<DefaultUser> users;

        public DefaultLoginViewModel()
        {
            TappedCommand = new RelayCommand<DefaultUser>(this.TappedCommandExecute);
        }

        public string Login
        {
            get { return login; }
            set
            {
                login = value;
                this.RaisePropertyChanged();
            }
        }

        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                this.RaisePropertyChanged();
            }
        }

        public List<DefaultUser> Users
        {
            get { return users; }
            set
            {
                users = value;
                this.RaisePropertyChanged();
            }
        }

        public RelayCommand<DefaultUser> TappedCommand { get; set; }

        public override async void OnAppearing()
        {
            base.OnAppearing();

            this.IsBusy = true;

                var list = new List<DefaultUser>();
                var baseSesionResult = await App.QbProvider.GetBaseSession();
                if (baseSesionResult)
                {
                    var users = await App.QbProvider.GetUserByTag("XamarinChat");
                    foreach (var user in users)
                    {
                        list.Add(new DefaultUser() { Name = user.FullName, Login = user.Login, Password = user.Login });
                    }
                }

                Users = list;
                this.IsBusy = false;
        }



        private async void TappedCommandExecute(DefaultUser user)
        {
            this.IsBusy = true;

            var loginValue = user.Login;
            var passwordValue = user.Password;

            var userId = await App.QbProvider.LoginWithLoginValueAsync(loginValue, passwordValue, Quickblox.Sdk.GeneralDataModel.Models.Platform.windows_phone, new DeviceUid_Uwp().GetIdentifier());

            if (userId > 0)
            {
                this.IsBusy = false;
                App.NavigationFrame.Navigate(typeof(ChatsPage), userId);
            }
            else if (userId == 0)
            {
                new MessageDialog("Try to repeat login", "Error").ShowAsync();
            }

            this.IsBusy = false;
        }


        //InitDefault ();
        private void InitDefault()
        {
            var list = new List<DefaultUser>();
            list.Add(new DefaultUser() { Name = "Xamarin User 1", Login = "@xamarinuser1", Password = "@xamarinuser1" });
            list.Add(new DefaultUser() { Name = "Xamarin User 2", Login = "@xamarinuser2", Password = "@xamarinuser2" });
            list.Add(new DefaultUser() { Name = "Xamarin User 3", Login = "@xamarinuser3", Password = "@xamarinuser3" });
            list.Add(new DefaultUser() { Name = "Xamarin User 4", Login = "@xamarinuser4", Password = "@xamarinuser4" });
            list.Add(new DefaultUser() { Name = "Xamarin User 5", Login = "@xamarinuser5", Password = "@xamarinuser5" });

            this.Users = list;
        }
    }
}

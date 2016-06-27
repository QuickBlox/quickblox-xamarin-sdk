﻿using QbChat.Pcl.Repository;
using QbChat.UWP.ViewModels;
using System;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace QbChat.UWP.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PrivateChatPage : Page
    {
        private PrivateChatViewModel vm;
        private bool isLoading;

        public PrivateChatPage()
        {
            this.InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (isLoading)
                return;

            isLoading = true;

            var parameter = (string)e.Parameter;
            vm = new PrivateChatViewModel(parameter);
            this.DataContext = vm;
            vm.OnAppearing();

            //await Windows.UI.ViewManagement.StatusBar.GetForCurrentView().HideAsync();
        }

        public async void ScrollList()
        {
            var sorted = list.ItemsSource as ObservableCollection<MessageTable>;
            try
            {
                if (sorted != null && sorted.Count > 1)
                {
                    //await Task.Delay(500);
                    list.ScrollIntoView(sorted[sorted.Count - 1]);
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}

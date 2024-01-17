using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using todomobilne.classes;
using todomobilne.pages;
using Xamarin.Forms;


namespace todomobilne
{
    public partial class MainPage : ContentPage
    {
        JsonSO js = new JsonSO();
        public MainPage()
        {
            InitializeComponent();
            EventItem.List.Add(new EventItem(1, "pozycja 1 : ", "opis opis"));
            EventItem.List.Add(new EventItem(2, "pozycja 2 : ", "opis opis"));
            EventItem.List = js.OpenEventList();

            eventsItemListView.ItemsSource = EventItem.List;

        }

        private void eventsItemListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as EventItem;
            DisplayAlert(item.Subject, item.Info, "Zamknij");
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddEditItem(0));

        }

        private void MenuItem_ClickedFinish(object sender, EventArgs e)
        {
            var item = sender as MenuItem;
            var events = item.CommandParameter as EventItem;
            EventItem.List.Remove(events);
            int id = 0;
            if (EventItem.ListDone.Any())
                id = EventItem.ListDone.Max(x => x.Id);
            id++;
            events.Id = id;
            EventItem.ListDone.Add(events);
            js.SaveEventList();
            js.SaveEventListDone();
            
        }

        private void MenuItem_ClickedDelete(object sender, EventArgs e)
        {
            var item = sender as MenuItem;
            var events = item.CommandParameter as EventItem;
            EventItem.List.Remove(events);
            js.SaveEventList();
        }

        private async void MenuItem_ClickedEdit(object sender, EventArgs e)
        {
            var item = sender as MenuItem;
            var events = item.CommandParameter as EventItem;
            await Navigation.PushAsync(new AddEditItem(events.Id));
        }

        private async void ToolbarItem_Clicked_1(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ListDonePage());
        }
    }
}

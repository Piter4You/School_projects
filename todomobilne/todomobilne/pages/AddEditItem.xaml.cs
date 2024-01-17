using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using todomobilne.classes;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace todomobilne.pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddEditItem : ContentPage
    {
        JsonSO js = new JsonSO();
        public AddEditItem(int id)
        {
            InitializeComponent();
            if (id == 0)
                AddEditPage.Title = "Dodaj nową pozycję do listy";
            else
            {
                AddEditPage.Title = "Edyztuj pozycję z listy";
                EventItem item = EventItem.List.Single(x => x.Id == id);
                hiddenId.Text = id.ToString();
                subjectEntry.Text = item.Subject;
                infoEditor.Text = item.Info;
            }
        }

        private async void button_Clicked(object sender, EventArgs e)
        {
            int id = 0;
            if (hiddenId.Text != null)
            {
                EventItem item = EventItem.List.Single(x => x.Id == int.Parse(hiddenId.Text));
                id = item.Id;
                EventItem.List.Remove(item);
            }
            if(id == 0)
            {
                if (EventItem.ListDone.Any())
                    id = EventItem.ListDone.Max(x => x.Id);
                id++;
            }             
            EventItem.List.Add(new EventItem(id, subjectEntry.Text, infoEditor.Text));
            js.SaveEventList();
            await Navigation.PopAsync();
        }
    }
}
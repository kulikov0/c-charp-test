using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using KDZCore.CacheDataSource;
using KDZCore.CloudDataSource.Model;
using KDZCore.Repository;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace KDZ
{
    public class MainWindow : Window
    {
        private KDZRepository repository  = KDZRepositoryImpl.GetInstance();

        private TextBlock _nameText;
        private TextBlock _descriptionText;
        private TextBlock _emailText;

        private TextBox _dateText;
        private TextBox _offsetText;

        private ListBox _lessonsListBox;
        private User user { get => repository.GetUser(); set { } }

        public MainWindow()
        {
            InitializeComponent();
            this.AttachDevTools();
            _nameText = this.FindControl<TextBlock>("NameText");
            _descriptionText = this.FindControl<TextBlock>("DescriptionText");
            _emailText = this.FindControl<TextBlock>("EmailText");
            _dateText = this.FindControl<TextBox>("DateTextBox");
            _offsetText = this.FindControl<TextBox>("OffsetTextBox");
            _lessonsListBox = this.FindControl<ListBox>("LessonsListBox");
            CheckIfUserExists();
        }

        public void OnSelectNewUserClicked(object sender, RoutedEventArgs args)
        {
            SetNewUser();
        }

        public void OnSearchLessonsClicked(object sender, RoutedEventArgs args)
        {
            repository.SearchLessons(user.Type, user.Id, _dateText.Text, _offsetText.Text).ContinueWith(task =>
               Dispatcher.UIThread.Post(() => {
                   _lessonsListBox.Items = task.Result.ConvertAll(item =>
                   "Building: " + item.building + "\n" +
                   "Type: " + item.type + "\n" +
                   "Lecturer: " + item.lecturer + "\n" +
                   "Lecturer Email: " + item.lecturer_email + "\n" +
                   "Discipline: " + item.discipline + "\n" +
                   "Start: " + item.date_start + "\n" +
                   "End: " + item.date_end + "\n" + 
                   "Link: " + GetLectionStream(item.stream_links)
                   );
               }) 
            );
        }
        
        private string GetLectionStream(List<StreamLink> links)
        {
            if (links != null) return !links.Any() ? "" : links[0].link;
            return "";
        }
        private void CheckIfUserExists()
        {
            if (user == null) SetNewUser();
            else DrawUserInfo();
            
        }

        private void SetNewUser()
        {
            new SearchUserWindow().ShowDialog<User>(this).ContinueWith(user =>
            {
                repository.SaveUser(user.Result);
                DrawUserInfo();
            });
        }
        private void DrawUserInfo()
        {
            if (user != null)
            {
                Dispatcher.UIThread.Post(() => {
                    _nameText.Text = "Name: " + user.Name;
                    _descriptionText.Text = "Description: " + user.Description;
                    _emailText.Text = "Email: " + user.Email;
                });
            }
            
        }
       
        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}

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
    public class SearchUserWindow : Window
    {
        private KDZRepository repository = KDZRepositoryImpl.GetInstance();

        private TextBox _textBoxName;
        private ListBox _listBoxName;
        private Button _submitButton;

        private List<UserResponse> _userList;
        public SearchUserWindow()
        {
            this.InitializeComponent();
            this.AttachDevTools();
            _textBoxName = this.FindControl<TextBox>("NameTextBox");
            _listBoxName = this.FindControl<ListBox>("NameListBox");
            _submitButton = this.FindControl<Button>("ButtonSubmit");
            StartListeningField();
        }

        public void OnSubmitClicked(object sender, RoutedEventArgs args)
        {
            var selectedUserResponse = _userList.Find(item => item.label == _listBoxName.SelectedItem.ToString());
            Close(new User()
            {
                Name = selectedUserResponse.label,
                Description = selectedUserResponse.description,
                Id = selectedUserResponse.id,
                Type = selectedUserResponse.type,
                Email = selectedUserResponse.additional.email
            }) ;
        }
        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
        private void StartListeningField()
        {
            _textBoxName.GetObservable(TextBox.TextProperty).Subscribe(text => 
                repository.SearchUsers(text).ContinueWith(task => { SetItems(task.Result); })
            );
            _listBoxName.GetObservable(ListBox.SelectionChangedEvent).Subscribe(_ =>
                _submitButton.IsEnabled = true
            );
        }
        private void SetItems(List<UserResponse> UserList)
        {
            Dispatcher.UIThread.Post(() => {
                _userList = UserList.Where(item => item.type != "group" && item.type != "auditorium").ToList(); 
                _listBoxName.Items = _userList.ConvertAll(item => item.label);
            });
        }
      
    }
}

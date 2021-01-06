using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

namespace TextFileChallenge
{
    public partial class ChallengeForm : Form
    {
        private const string DataSetFile = "AdvancedDataSet.csv"; //"AdvancedDataSet.csv"

        private BindingList<UserModel> users = new BindingList<UserModel>();
        private IUsersService usersService = new UsersService();

        public ChallengeForm()
        {
            InitializeComponent();

            LoadUsersFromFile(
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                   DataSetFile));

            WireUpDropDown();
        }

        private void LoadUsersFromFile(string filePath)
        {
            foreach (var user in usersService.GetAllFromFile(filePath))
            {
                users.Add(user);
            }
        }

        private void WireUpDropDown()
        {
            usersListBox.DataSource = users;
            usersListBox.DisplayMember = nameof(UserModel.DisplayText);
        }

        private void addUserButton_Click(object sender, EventArgs e)
        {
            // TODO - validate railway-style

            var user = new UserModel
            {
                FirstName = firstNameText.Text,
                LastName = lastNameText.Text,
                Age = (int)agePicker.Value, // TODO - unsafe
                IsAlive = isAliveCheckbox.Checked
            };
            users.Add(user);
        }

        private void saveListButton_Click(object sender, EventArgs e)
        {
            usersService.SaveToFile(users, DataSetFile);
        }
    }
}
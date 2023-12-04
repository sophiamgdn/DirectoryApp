using DirectoryApp.Views;
using DirectoryApp.Models;
using DirectoryApp.ViewModel;
namespace DirectoryApp
{
    public partial class MainPage : ContentPage
    {
        StudentViewModel studentViewModel = new StudentViewModel();
        Student student = new Student();

        public MainPage()
        {
            InitializeComponent();
            Shell.Current.Title = "Login Page";
        }

        private void OnLoginClick(object sender, EventArgs e)
        {
            string ID = txtID.Text;
            string password = txtPasswordFld.Text;

            if (string.IsNullOrWhiteSpace(ID) || string.IsNullOrWhiteSpace(password))
            {
                DisplayAlert("Error!", "Student ID and password should not be empty. Kindly enter your credentials.", "OK");
            }
            else
            {
                // Assuming you have a StudentCollection with student information
                Student existingStudent = studentViewModel.StudentCollection.FirstOrDefault(student => student.StudentID == ID);

                if (existingStudent != null && existingStudent.StudentPassword == password)
                {
                    Navigation.PushAsync(new HomePage());
                }
                else
                {
                    DisplayAlert("Error!", "Invalid credentials. Please check your Student ID and password.", "OK");
                    txtID.Text = string.Empty;
                    txtPasswordFld.Text = string.Empty;
                }
            }
        }

        private async void OnLabelTapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegisterPage());
        }
    }
}

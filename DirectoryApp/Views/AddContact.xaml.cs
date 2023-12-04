using DirectoryApp.ViewModel;
using DirectoryApp.Models;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace DirectoryApp.Views;

public partial class AddContact : ContentPage
{
    private List<School> schoolList;
    ContactViewModel contactViewModel = new ContactViewModel();
    StudentViewModel studentViewModel = new StudentViewModel();
	public AddContact()
	{
		InitializeComponent();
        InitializeSchool();
        InitializeSchoolCourses();
        BindingContext = contactViewModel;
    }

	public void OnResetClicked (object sender, EventArgs e) {
        ResetForm();
    }

    public void OnSubmitClicked (object sender, EventArgs e)
    {
        ValidateForm();
    }

    public void ValidateForm()
    {
        if(!rdoStudent.IsChecked && !rdoFaculty.IsChecked)
        {
            DisplayAlert("ERROR", "Either of the radio buttons should be selected. Please select one","OK");
            return;
        }
        if (rdoStudent.IsChecked)
        {
            //accept a series of 5 digit numeric values
            string input = txtID.Text;
            if (pkrSchoolName.SelectedIndex == 0 || pkrCourse.SelectedIndex == 0)
            {
                DisplayAlert("ERROR", "Please select an option.", "OK");
                return;
            }
            if (input.Length != 5)
            {
                DisplayAlert("ERROR", "Invalid input for students. Please enter a 5-digit numeric value.", "OK");
                return;
            }
        }
        else if (rdoFaculty.IsChecked)
        {
            //accept a series of 4 digit numeric values
            string input = txtID.Text;
            if (input.Length != 4)
            {
                DisplayAlert("ERROR", "Invalid input for faculty. Please enter a 4-digit numeric value.", "OK");
                return;
            }
            pkrCourse.IsEnabled = false;

        }
        else
        {
            DisplayAlert("ERROR", "Please select whether the entry is for a student or faculty.", "OK");
            return;
        }
        if (String.IsNullOrEmpty(txtID.Text) || String.IsNullOrEmpty(txtFirstName.Text) || String.IsNullOrEmpty(txtLastName.Text) || String.IsNullOrEmpty(txtEmail.Text) || String.IsNullOrEmpty(txtMobile.Text))
        {
            DisplayAlert("ERROR", "Entry fields should not be empty. Please supply necessary information.", "OK");
            return;
        }
        string emailAddress = txtEmail.Text;
        if (!IsValidEmail(emailAddress))
        {
            DisplayAlert("Error", "Invalid email address. Please enter a valid email.", "OK");
            return;
        }

        
        Register();
    }

    public void Register()
    {
        ContactInfo contactInfo = new ContactInfo();
        Student student = new Student();
        if (rdoFaculty.IsChecked)
        {
            contactInfo.Type = "Faculty";
        }
        else
        {
            contactInfo.Type = "Student";
        }
        contactInfo.FirstName = txtFirstName.Text;
        contactInfo.LastName = txtLastName.Text;
        contactInfo.Email = txtEmail.Text;
        contactInfo.School = pkrSchoolName.SelectedItem as String;
        contactInfo.Course = pkrCourse.SelectedItem as String;
        contactInfo.Phone = txtMobile.Text; 
        contactInfo.ID = txtID.Text;

        if (contactViewModel.ContactList.Any(existingContact => existingContact.ID == contactInfo.ID))
        {
            DisplayAlert("ERROR", "ID exists", "OK");
            txtID.Text = string.Empty;
            return;
        }
        else
        {
            string fileName = $"S{contactInfo.ID}.txt";
            string maindir = FileSystem.Current.AppDataDirectory;
            string filePath = maindir + fileName;

            var json = JsonSerializer.Serialize(contactInfo);
            File.WriteAllText(filePath, json);
            DisplayAlert("Submission", "Successful registration!", "Proceed");
            contactViewModel.AddContact(contactInfo);
            ResetForm();
            Navigation.PushAsync(new HomePage());
        }
    }

    public void ResetForm()
    {
        rdoFaculty.IsChecked = false;
        rdoStudent.IsChecked = false;
        txtID.Text = string.Empty;
        txtFirstName.Text = string.Empty;
        txtLastName.Text = string.Empty;    
        txtEmail.Text = string.Empty;
        txtMobile.Text = string.Empty;
        pkrCourse.SelectedIndex = 0;
        pkrSchoolName.SelectedIndex = 0;
    }

    public void InitializeSchool()
    {
        schoolList = new List<School>
        {
             new School
            {
                SchoolName = "-SELECT-",
                Courses = new List<string> {"-SELECT-"}
            },
            new School
            {
                SchoolName = "School of Engineering",
                Courses = new List<string> { "-SELECT-", "BS Computer Engineering", "BS Electronics Engineering", "BS Civil Engineering", "BS Mechanical Engineering", "BS Industrial Engineering", "BS Electrical Engineering" }
            },
            new School
            {
                SchoolName = "School of Business and Management",
                Courses = new List<string> { "-SELECT-", "BS Accountancy", "BS Management Accounting", "BSBA - Financial Management", "BS Entrepreneurship", "BSBA - Operation Management", "BSBA - Marketing Management", "BSBA - HR Management", "BS Hospitality Management", "BS Hospitality Management - Food and Beverages", "BS Tourism Management"}
            },
            new School
            {
                SchoolName = "School of Computer Studies",
                Courses = new List<string> { "-SELECT-", "BS Information Technology", "BS Computer Science", "Bachelor of Science in Information Systems"}
            },
            new School
            {
                SchoolName = "School of Allied Medical Sciences",
                Courses = new List<string> { "-SELECT-", "BS Nursing"}
            },
            new School
            {
                SchoolName = "School of Education",
                Courses = new List<string> { "-SELECT-", "Bachelor of Elementary Education", "Bachelor of Early Childhood Education", "Bachelor of Physical Education","Bachelor of Secondary Education Major in English", "Bachelor of Secondary Education Major in Filipino", "Bachelor of Secondary Education Major in Mathematics", "Bachelor of Secondary Education Major in Science", "Bachelor of Special Needs Education - Generalist", "Bachelor of Special Needs Education - Early Childhood Education", "Bachelor of Special Needs Education - Elementary School Teaching"}
            },
            new School
            {
                SchoolName = "School of Arts and Sciences",
                Courses = new List<string> { "-SELECT-", "BA in Communication", "BA Journalism", "BA Marketing Communication", "BA English Language Studies", "BS Biology in Medical Biology", "BS Microbiology", "BS Psychology", "BS Library and Information Science", "BA International Studies", "BA Political Science"}
            },
            new School
            {
                SchoolName = "School of Law",
                Courses = new List<string> { "-SELECT-", "Bachelor of Laws"}
            }
        };
    }

    public void InitializeSchoolCourses()
    {
        foreach (var school in schoolList)
        {
            pkrSchoolName.Items.Add(school.SchoolName);
        }

        pkrCourse.Items.Add("-SELECT-");
        pkrSchoolName.SelectedIndex = 0; //to display "-SELECT-"
        pkrCourse.SelectedIndex = 0;

        pkrSchoolName.SelectedIndexChanged += (sender, args) =>
        {
            if (pkrSchoolName.SelectedIndex != -1)
            {
                var selectedSchool = schoolList[pkrSchoolName.SelectedIndex];
                pkrCourse.ItemsSource = selectedSchool.Courses; //initialize courses within the school of choice
                pkrCourse.SelectedIndex = 0; //set the default value
            }
        };
    }
    private bool IsValidEmail(string email)
    {
        // Use a regular expression to validate the email address.
        string pattern = @"^[\w\.-]+@[\w\.-]+\.\w+$";
        Regex regex = new Regex(pattern);

        return regex.IsMatch(email);
    }
}
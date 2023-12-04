using DirectoryApp.ViewModel;
using DirectoryApp.Views;
using DirectoryApp.Models;
using System.Collections.ObjectModel;
using System.Text.Json;

namespace DirectoryApp;

public partial class RegisterPage : ContentPage
{
    private List<School> schoolList;
    StudentViewModel studentModelView = new StudentViewModel();

    public RegisterPage()
    {
        InitializeComponent();
        Shell.Current.Title = "Register User";
        InitializeSchool();
        InitializeSchoolCourses();
        pkrYear.SelectedIndex = 0;
        BindingContext = studentModelView;

    }

    void OnSubmitClicked(object sender, EventArgs e)
    {
        ValidateForm();
    }

    void OnResetClicked(object sender, EventArgs e)
    {
        //textboxes will be cleared out, radio buttons will be deselected, dropdown will return to default values\
        ResetForm();

    }

    public void ValidateForm()
    {
        int rdoValid = 0;
        List<string> errors = new List<string>();
        //validate the form control requirements
        if (IDValidator.IsNotValid)
        {
            foreach (var error in IDValidator.Errors)
            {
                errors.Add(error.ToString());
            }
        }

        if (firstNameValidator.IsNotValid)
        {
            foreach (var error in firstNameValidator.Errors)
            {
                errors.Add(error.ToString());
            }
        }

        if (lastNameValidator.IsNotValid)
        {
            foreach (var error in lastNameValidator.Errors)
            {
                errors.Add(error.ToString());
            }
        }

        if (emailValidator.IsNotValid)
        {
            foreach (var error in emailValidator.Errors)
            {
                errors.Add(error.ToString());
            }
        }

        if (passwordValidator.IsNotValid)
        {
            foreach (var error in passwordValidator.Errors)
            {
                errors.Add(error.ToString());
            }
        }

        if (passwordValidator.IsValid && txtPassword.Text != txtConfirmPass.Text)
        {
            errors.Add("Passwords do not match.");
        }

        if (mobileValidator.IsNotValid)
        {
            foreach (var error in mobileValidator.Errors)
            {
                errors.Add(error.ToString());
            }
        }

        if (pkrSchoolName.SelectedIndex == 0 || pkrCourseName.SelectedIndex == 0 || pkrYear.SelectedIndex == 0 || (!rdoFemale.IsChecked && !rdoMale.IsChecked))
        {
            errors.Add("Please select an option");
        }
        else
        {
            rdoValid = 1;

        }

        if (errors.Any())
        {
            string errorMessage = string.Join(Environment.NewLine, errors);
            DisplayAlert("Error", errorMessage, "OK");
        }

        if (IDValidator.IsValid && firstNameValidator.IsValid && lastNameValidator.IsValid && emailValidator.IsValid && passwordValidator.IsValid && mobileValidator.IsValid && txtPassword.Text == txtConfirmPass.Text && mobileValidator.IsValid && pkrSchoolName.SelectedIndex != 0 && pkrCourseName.SelectedIndex != 0 && pkrYear.SelectedIndex != 0 && rdoValid == 1)
        {
            //DisplayAlert("Success!", "Form Validated", "Proceed");
            Register();
        }

    }

    public void Register()
    {
        //Once all input values are valid, a pop-up will display the values the user entered.
        Student student = new Student();
        student.StudentID = txtStudentID.Text;
        student.StudentFirstName = txtFirstName.Text;
        student.StudentLastName = txtLastName.Text;
        student.StudentEmail = txtEmail.Text;
        student.StudentPassword = txtPassword.Text;
        
        if (!rdoFemale.IsChecked)
        {
            student.StudentGender = "MALE";
        }
        else
        {
            student.StudentGender = "FEMALE";
        }
        student.StudentBirthday = dateBirthday.Date.ToString("dd/MM/yyyy");
        student.StudentMobile = txtMobileNo.Text;
        student.StudentCity = txtCity.Text;
        student.SchoolName = pkrSchoolName.SelectedItem as String;
        student.CourseName = pkrCourseName.SelectedItem as String;
        student.YearLevel = pkrYear.SelectedIndex.ToString();

        //var popup = new StudentFormResults();
        //popup.BindingContext = new { student };
        //this.ShowPopup(popup);

        //check if student ID exists

        if (studentModelView.StudentCollection.Any(existingStudent => existingStudent.StudentID == student.StudentID))
        {
            DisplayAlert("ERROR", "ID exists", "OK");
            txtStudentID.Text = string.Empty;
        }
        else
        {
            
            string fileName = $"S{student.StudentID}.txt";
            string maindir = FileSystem.Current.AppDataDirectory;
            string filePath = maindir + fileName;

            var json = JsonSerializer.Serialize(student);
            File.WriteAllText(filePath, json);

           
            DisplayAlert("Submission", "Successful registration!", "Proceed");
            studentModelView.AddStudent(student);
            ResetForm();
            Navigation.PushAsync(new HomePage());

        }

        //redirect to HomePage
    }
    public void ResetForm()
    {
        txtStudentID.Text = string.Empty;
        txtFirstName.Text = string.Empty;
        txtLastName.Text = string.Empty;
        txtEmail.Text = string.Empty;
        txtPassword.Text = string.Empty;
        txtConfirmPass.Text = string.Empty;
        rdoFemale.IsChecked = false;
        rdoMale.IsChecked = false;
        dateBirthday.Date = DateTime.Now;
        txtMobileNo.Text = string.Empty;
        txtCity.Text = string.Empty;
        pkrSchoolName.SelectedIndex = 0;
        pkrCourseName.SelectedIndex = 0;
        pkrYear.SelectedIndex = 0;
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

        // int defaultDisplay = 1;
        // pkrSchoolName.SelectedIndex = defaultDisplay;
    }

    public void InitializeSchoolCourses()
    {
        foreach (var school in schoolList)
        {
            pkrSchoolName.Items.Add(school.SchoolName);
        }

        pkrCourseName.Items.Add("-SELECT-");
        pkrSchoolName.SelectedIndex = 0; //to display "-SELECT-"
        pkrCourseName.SelectedIndex = 0;

        pkrSchoolName.SelectedIndexChanged += (sender, args) =>
        {
            if (pkrSchoolName.SelectedIndex != -1)
            {
                var selectedSchool = schoolList[pkrSchoolName.SelectedIndex];
                pkrCourseName.ItemsSource = selectedSchool.Courses; //initialize courses within the school of choice
                pkrCourseName.SelectedIndex = 0; //set the default value
            }
        };
    }


}


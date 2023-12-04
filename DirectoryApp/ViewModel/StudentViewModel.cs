using DirectoryApp.Models;
using DirectoryApp.Views;
using System.Collections.ObjectModel;
using System.Text.Json;
using System.Windows.Input;

namespace DirectoryApp.ViewModel
{
    public class StudentViewModel
    {
        private ObservableCollection<Student> studentCollection = new ObservableCollection<Student>();
        string maindir = FileSystem.Current.AppDataDirectory;
        string fileName = "Users.txt";

        public StudentViewModel()
        {
            SetStudentCollection();
        }


        public ObservableCollection<Student> StudentCollection
        {
            get { return studentCollection; }
            set { studentCollection = value; }
        }


        public void AddStudent(Student student)
        {
            studentCollection.Add(student);
            SaveToFile();
        }

        public void SaveToFile()
        {
            var json = string.Empty;
            json = JsonSerializer.Serialize(studentCollection);

            File.WriteAllText(maindir + fileName, json);
        }

        public void SetStudentCollection()
        {
            if (File.Exists(maindir + fileName)) { 
                string jsonData = File.ReadAllText(maindir + fileName);
                studentCollection = JsonSerializer.Deserialize<ObservableCollection<Student>>(jsonData);
            }
        }


    }
}

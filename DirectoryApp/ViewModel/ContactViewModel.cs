using System.Collections.ObjectModel;
using System.Text.Json;
using DirectoryApp.Models;


namespace DirectoryApp.ViewModel
{
    public class ContactViewModel
    {
        private ObservableCollection<ContactInfo> contactList = new ObservableCollection<ContactInfo>();
        string maindir = FileSystem.Current.AppDataDirectory;

        public ContactViewModel() {
            ConvertToContactList();
        }

        public ObservableCollection<ContactInfo> ContactList
        {
            get { return contactList; }
            set { contactList = value; }
        }

        public void AddContact(ContactInfo contact)
        {
            contactList.Add(contact);
            SaveToFile();
        }

        public void SaveToFile()
        {
            var json = string.Empty;
            json = JsonSerializer.Serialize(contactList);
            File.WriteAllText(maindir + "Contacts.txt", json);
        }

        public void ConvertToContactList()
        {
            if (File.Exists(maindir + "Contacts.txt"))
            {
                string jsonData = File.ReadAllText(maindir + "Contacts.txt");
                contactList = JsonSerializer.Deserialize<ObservableCollection<ContactInfo>>(jsonData);
            }
        }

        public bool ContactFileExists()
        {
            return File.Exists(maindir + "Contacts.txt");
        }

        

    }
}

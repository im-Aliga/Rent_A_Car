namespace CarRent.Areas.Client.ViewModels
{
    public class ContactViewModel
    {
        public ContactViewModel(string emailAdress, string? adress, string phoneNumber)
        {
            EmailAdress = emailAdress;
            Adress = adress;
            PhoneNumber = phoneNumber;
        }

        public ContactViewModel(string emailAdress, string phoneNumber, string name, string surname)
        {
            EmailAdress = emailAdress;
            PhoneNumber = phoneNumber;
            Name = name;
            Surname = surname;
        }
        public ContactViewModel()
        {

        }

        public string? Adress { get; set; }

        public string EmailAdress { get; set; }
        public string PhoneNumber { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}

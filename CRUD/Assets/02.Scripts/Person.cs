using System.Collections.Generic;

public class PersonList
{
    public List<Person> persons;
}


[System.Serializable]
public class Person
{
    public string Name;
    public string PhoneNumber;
    public string Email;

    public Person(string name, string phoneNumber, string email)
    {
        this.Name = name;
        this.PhoneNumber = phoneNumber;
        this.Email = email;
    }
}


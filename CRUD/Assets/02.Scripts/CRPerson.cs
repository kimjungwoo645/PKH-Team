using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class CRPerson
{
    static public PersonList LoadData(string path)
    {
        PersonList personList = new PersonList();
        if (File.Exists(path))
        {
            StreamReader streamReader = new StreamReader(path);
            string jsonStr = streamReader.ReadToEnd();

            personList = JsonUtility.FromJson<PersonList>(jsonStr);
            streamReader.Close();

            return personList;
        }
        else
        {
            Person person = new Person("이름", "010", "E@mail.com");
            List<Person> pList = new List<Person>();
            pList.Add(person);

            personList.persons = pList;

            string jsonStr = JsonUtility.ToJson(personList);
            using (StreamWriter streamWriter = new StreamWriter(path))
            {
                streamWriter.Write(jsonStr);
            }
            return personList;
        }
    }
}
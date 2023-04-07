using System;
using System.Collections;
using System.Collections.Generic;

public class Address
{
    public string City { get; set; }
    public string Street { get; set; }
    public int HouseNumber { get; set; }
}

public class Person
{
    public string LastName { get; set; }
    public string FirstName { get; set; }
    public string MiddleName { get; set; }
    public DateTime BirthDate { get; set; }
    public Address HomeAddress { get; set; }
    public string PhoneNumber { get; set; }

    public Person(string lastName, string firstName, string middleName, DateTime birthDate, Address homeAddress, string phoneNumber)
    {
        LastName = lastName;
        FirstName = firstName;
        MiddleName = middleName;
        BirthDate = birthDate;
        HomeAddress = homeAddress;
        PhoneNumber = phoneNumber;
    }

    public virtual void DisplayInfo()
    {
        Console.WriteLine($"Last Name: {LastName}");
        Console.WriteLine($"First Name: {FirstName}");
        Console.WriteLine($"Middle Name: {MiddleName}");
        Console.WriteLine($"Birth Date: {BirthDate.ToShortDateString()}");
        Console.WriteLine($"Home Address: {HomeAddress.City}, {HomeAddress.Street}, {HomeAddress.HouseNumber}");
        Console.WriteLine($"Phone Number: {PhoneNumber}");
    }
}

public class Student : Person, IComparable<Student>
{
    public List<int> Grades { get; } = new List<int>();

    public Student(string lastName, string firstName, string middleName, DateTime birthDate, Address homeAddress, string phoneNumber)
        : base(lastName, firstName, middleName, birthDate, homeAddress, phoneNumber)
    {
    }

    public Student(string lastName, string firstName, string middleName, DateTime birthDate, Address homeAddress, string phoneNumber, List<int> grades)
        : base(lastName, firstName, middleName, birthDate, homeAddress, phoneNumber)
    {
        Grades = grades;
    }

    public void AddGrade(int grade)
    {
        Grades.Add(grade);
    }

    public double CalculateAVG()
    {
        double sum = 0;
        foreach (int grade in Grades)
        {
            sum += grade;
        }
        return sum / Grades.Count;
    }

    public int CompareTo(Student other)
    {
        double avgGrade = CalculateAVG();
        double otherAvgGrade = other.CalculateAVG();

        if (avgGrade > otherAvgGrade)
            return -1;
        else if (avgGrade < otherAvgGrade)
            return 1;
        else
            return 0;
    }

    public override void DisplayInfo()
    {
        base.DisplayInfo();
        Console.WriteLine($"Grades: {string.Join(", ", Grades)}");
    }
}

public class Aspirant : Student
{
    public string ThesisTitle { get; set; }

    public Aspirant(string lastName, string firstName, string middleName, DateTime birthDate, Address homeAddress, string phoneNumber, string thesisTitle)
        : base(lastName, firstName, middleName, birthDate, homeAddress, phoneNumber)
    {
        ThesisTitle = thesisTitle;
    }

    public override void DisplayInfo()
    {
        base.DisplayInfo();
        Console.WriteLine($"Thesis Title: {ThesisTitle}");
    }
}

public class StudentEnumerator : IEnumerator<Student>
{
    private Student[] students;
    private int index = -1;

    public StudentEnumerator(Student[] students)
    {
        this.students = students;
    }

    public bool MoveNext()
    {
        if (index < students.Length - 1)
        {
            index++;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Reset()
    {
        index = -1;
    }

    public Student Current
    {
        get { return students[index]; }
    }

    object IEnumerator.Current
    {
        get { return Current; }
    }

    public void Dispose()//неуправляемы
    {
        
    }
}

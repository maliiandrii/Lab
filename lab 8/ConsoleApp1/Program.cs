using System;
using System.Collections.Generic;
using System.Data;

public class Person
{
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public Person(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }

    public override string ToString()
    {
        return $"{FirstName} {LastName}";
    }
}

public class Student : Person
{
    public string Group { get; set; }
    public string Dormitory { get; set; }

    /* public Student(string firstName, string lastName, string group, string dormitory) : base(firstName, lastName)
     {
         Group = group;
         Dormitory = dormitory;
     }*/
    public Student(string firstName, string lastName, string group) : base(firstName, lastName)
    {
        Group = group;
        Dormitory = "-";
    }

    public override string ToString()
    {
        return base.ToString() + $", {Group}, {Dormitory}";
    }
}

public class Group
{
    public string Name { get; set; }
    public List<Student> Students { get; set; }

    public Group(string name)
    {
        Name = name;
        Students = new List<Student>();
    }

    public void AddStudent(Student student)
    {
        Students.Add(student);
    }

    public void RemoveStudent(Student student)
    {
        Students.Remove(student);
    }

    public override string ToString()
    {
        return Name;
    }
}

public class Dormitory
{
    public string Name { get; set; }
    public int TotalCapacity { get; set; }
    public int AvailableCapacity { get; set; }
    public List<Student> Residents { get; set; }

    public Dormitory(string name, int totalCapacity)
    {
        Name = name;
        TotalCapacity = totalCapacity;
        AvailableCapacity = totalCapacity;
        Residents = new List<Student>();
    }

    public void AddStudent()
    {
        AvailableCapacity--;
    }
    public void RemoveStudent()
    {
        AvailableCapacity++;
    }

    /*public void AddResident(Student student)
    {
        if (AvailableCapacity > 0)
        {
            Residents.Add(student);
            AvailableCapacity--;
            student.Dormitory = Name;
            Console.WriteLine($"Студент {student.FirstName} {student.LastName} успішно поселений в гуртожиток {Name}.");
        }
        else
        {
            Console.WriteLine($"Увага! Гуртожиток {Name} вже заповнений.");
        }
    }

    public void RemoveResident(Student student)
    {
        Residents.Remove(student);
        AvailableCapacity++;
        student.Dormitory = null;
        Console.WriteLine($"Студент {student.FirstName} {student.LastName} виселений з гуртожитку {Name}.");
    }*/

    public override string ToString()
    {
        return $"{Name}, Total Capacity: {TotalCapacity}, Current Capacity: {AvailableCapacity}";
    }
}

public class Deanery
{
    public List<Student> Students { get; set; }
    public List<Group> Groups { get; set; }
    public List<Dormitory> Dormitories { get; set; }

    public Deanery()
    {
        Students = new List<Student>();
        Groups = new List<Group>();
        Dormitories = new List<Dormitory>();
    }

    public void AddStudent(Student student)
    {
        Students.Add(student);
    }

    public void RemoveStudent(Student student)
    {
        Students.Remove(student);
    }

    public void EditStudent(Student student, string newFirstName, string newLastName, string newGroup)
    {
        student.FirstName = newFirstName;
        student.LastName = newLastName;
        student.Group = newGroup;
    }

    public void AddGroup(Group group)
    {
        Groups.Add(group);
    }

    public void RemoveGroup(Group group)
    {
        Groups.Remove(group);
    }

    public void EditGroup(Group group, string newName)
    {
        group.Name = newName;
    }

    public void AddDormitory(Dormitory dormitory)
    {
        Dormitories.Add(dormitory);
    }

    public void RemoveDormitory(Dormitory dormitory)
    {
        Dormitories.Remove(dormitory);
    }

    public void EditDormitory(Dormitory dormitory, string newName, int newCapacity)
    {
        dormitory.Name = newName;
        dormitory.TotalCapacity = newCapacity;
    }

    public void AddStudentDorm(Student student, string Dorm, Dormitory dormitory)
    {
        student.Dormitory = Dorm;
        dormitory.AddStudent();
    }
    public void RemoveStudentDorm(Student student, Dormitory dormitory)
    {
        student.Dormitory = "-";
        dormitory.RemoveStudent();
    }

}

class Program
{
    static void Main(string[] args)
    {
        Deanery deanery = new Deanery();

        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("1. Manage Students");
            Console.WriteLine("2. Manage Groups");
            Console.WriteLine("3. Manage Dormitories");
            Console.WriteLine("4. Search");
            Console.WriteLine("5. Exit");
            Console.WriteLine("Enter your choice:");

            int choice;
            if (int.TryParse(Console.ReadLine(), out choice))
            {
                switch (choice)
                {
                    case 1:
                        ManageStudents(deanery);
                        break;
                    case 2:
                        ManageGroups(deanery);
                        break;
                    case 3:
                        ManageDormitories(deanery);
                        break;
                    case 4:
                        Search(deanery);
                        break;
                    case 5:
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please enter a number between 1 and 5.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a number.");
            }

            Console.WriteLine();
        }
    }

    static void ManageStudents(Deanery deanery)
    {
        Console.WriteLine("1. Add Student");
        Console.WriteLine("2. Remove Student");
        Console.WriteLine("3. Edit Student");
        Console.WriteLine("4. View All Students");
        Console.WriteLine("5. View Student Details");
        Console.WriteLine("Enter your choice:");

        int choice;
        if (int.TryParse(Console.ReadLine(), out choice))
        {
            switch (choice)
            {
                case 1:
                    AddStudent(deanery);
                    break;
                case 2:
                    RemoveStudent(deanery);
                    break;
                case 3:
                    EditStudent(deanery);
                    break;
                case 4:
                    ViewAllStudents(deanery);
                    break;
                case 5:
                    ViewStudentDetails(deanery);
                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter a number.");
        }
    }

    static void AddStudent(Deanery deanery)
    {
        Console.WriteLine("Enter student's first name:");
        string firstName = Console.ReadLine();
        Console.WriteLine("Enter student's last name:");
        string lastName = Console.ReadLine();
        Console.WriteLine("Enter student's group:");
        string group = Console.ReadLine();

        deanery.AddStudent(new Student(firstName, lastName, group));
        Console.WriteLine("Student added successfully.");
    }

    static void RemoveStudent(Deanery deanery)
    {
        Console.WriteLine("Enter student's first name:");
        string firstName = Console.ReadLine();
        Console.WriteLine("Enter student's last name:");
        string lastName = Console.ReadLine();

        Student student = deanery.Students.Find(s => s.FirstName == firstName && s.LastName == lastName);
        if (student != null)
        {
            deanery.RemoveStudent(student);
            Console.WriteLine("Student removed successfully.");
        }
        else
        {
            Console.WriteLine("Student not found.");
        }
    }

    static void EditStudent(Deanery deanery)
    {
        Console.WriteLine("Enter student's first name:");
        string firstName = Console.ReadLine();
        Console.WriteLine("Enter student's last name:");
        string lastName = Console.ReadLine();

        Student student = deanery.Students.Find(s => s.FirstName == firstName && s.LastName == lastName);
        if (student != null)
        {
            Console.WriteLine("Enter new first name:");
            string newFirstName = Console.ReadLine();
            Console.WriteLine("Enter new last name:");
            string newLastName = Console.ReadLine();
            Console.WriteLine("Enter new group:");
            string newGroup = Console.ReadLine();

            deanery.EditStudent(student, newFirstName, newLastName, newGroup);
            Console.WriteLine("Student edited successfully.");
        }
        else
        {
            Console.WriteLine("Student not found.");
        }
    }

    static void ViewAllStudents(Deanery deanery)
    {
        Console.WriteLine("All Students:");
        foreach (var student in deanery.Students)
        {
            Console.WriteLine(student);
        }
    }

    static void ViewStudentDetails(Deanery deanery)
    {
        Console.WriteLine("Enter student's first name:");
        string firstName = Console.ReadLine();
        Console.WriteLine("Enter student's last name:");
        string lastName = Console.ReadLine();

        Student student = deanery.Students.Find(s => s.FirstName == firstName && s.LastName == lastName);
        if (student != null)
        {
            Console.WriteLine($"Student Details: {student}");
        }
        else
        {
            Console.WriteLine("Student not found.");
        }
    }

    static void ManageGroups(Deanery deanery)
    {
        Console.WriteLine("1. Add Group");
        Console.WriteLine("2. Remove Group");
        Console.WriteLine("3. Edit Group");
        Console.WriteLine("4. View All Groups");
        Console.WriteLine("5. View Group Details");
        Console.WriteLine("Enter your choice:");

        int choice;
        if (int.TryParse(Console.ReadLine(), out choice))
        {
            switch (choice)
            {
                case 1:
                    AddGroup(deanery);
                    break;
                case 2:
                    RemoveGroup(deanery);
                    break;
                case 3:
                    EditGroup(deanery);
                    break;
                case 4:
                    ViewAllGroups(deanery);
                    break;
                case 5:
                    ViewGroupDetails(deanery);
                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter a number.");
        }
    }

    static void AddGroup(Deanery deanery)
    {
        Console.WriteLine("Enter group name:");
        string groupName = Console.ReadLine();

        deanery.AddGroup(new Group(groupName));
        Console.WriteLine("Group added successfully.");
    }

    static void RemoveGroup(Deanery deanery)
    {
        Console.WriteLine("Enter group name:");
        string groupName = Console.ReadLine();

        Group group = deanery.Groups.Find(g => g.Name == groupName);
        if (group != null)
        {
            deanery.RemoveGroup(group);
            Console.WriteLine("Group removed successfully.");
        }
        else
        {
            Console.WriteLine("Group not found.");
        }
    }

    static void EditGroup(Deanery deanery)
    {
        Console.WriteLine("Enter group name:");
        string groupName = Console.ReadLine();

        Group group = deanery.Groups.Find(g => g.Name == groupName);
        if (group != null)
        {
            Console.WriteLine("Enter new group name:");
            string newName = Console.ReadLine();

            deanery.EditGroup(group, newName);
            Console.WriteLine("Group edited successfully.");
        }
        else
        {
            Console.WriteLine("Group not found.");
        }
    }

    static void ViewAllGroups(Deanery deanery)
    {
        Console.WriteLine("All Groups:");
        foreach (var group in deanery.Groups)
        {
            Console.WriteLine(group);
        }
    }

    static void ViewGroupDetails(Deanery deanery)
    {
        Console.WriteLine("Enter group name:");
        string groupName = Console.ReadLine();

        Group group = deanery.Groups.Find(g => g.Name == groupName);
        if (group != null)
        {
            Console.WriteLine($"Group Details: {group}");
        }
        else
        {
            Console.WriteLine("Group not found.");
        }
    }

    static void ManageDormitories(Deanery deanery)
    {
        Console.WriteLine("1. Add Dormitory");
        Console.WriteLine("2. Remove Dormitory");
        Console.WriteLine("3. Edit Dormitory");
        Console.WriteLine("4. View All Dormitories");
        Console.WriteLine("5. Add student into dormitory");
        Console.WriteLine("6. Remove student from dormitory");
        Console.WriteLine("Enter your choice:");

        int choice;
        if (int.TryParse(Console.ReadLine(), out choice))
        {
            switch (choice)
            {
                case 1:
                    AddDormitory(deanery);
                    break;
                case 2:
                    RemoveDormitory(deanery);
                    break;
                case 3:
                    EditDormitory(deanery);
                    break;
                case 4:
                    ViewAllDormitories(deanery);
                    break;

                case 5:
                    AddStudentDorm(deanery);
                    break;
                case 6:
                    RemoveStudentDorm(deanery);
                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter a number.");
        }
    }

    static void AddDormitory(Deanery deanery)
    {
        Console.WriteLine("Enter dormitory name:");
        string dormitoryName = Console.ReadLine();
        Console.WriteLine("Enter dormitory capacity:");
        int dormitoryCapacity = Convert.ToInt32(Console.ReadLine());

        deanery.AddDormitory(new Dormitory(dormitoryName, dormitoryCapacity));
        Console.WriteLine("Dormitory added successfully.");
    }

    static void RemoveDormitory(Deanery deanery)
    {
        Console.WriteLine("Enter dormitory name:");
        string dormitoryName = Console.ReadLine();

        Dormitory dormitory = deanery.Dormitories.Find(d => d.Name == dormitoryName);
        if (dormitory != null)
        {
            deanery.RemoveDormitory(dormitory);
            Console.WriteLine("Dormitory removed successfully.");
        }
        else
        {
            Console.WriteLine("Dormitory not found.");
        }
    }

    static void EditDormitory(Deanery deanery)
    {
        Console.WriteLine("Enter dormitory name:");
        string dormitoryName = Console.ReadLine();

        Dormitory dormitory = deanery.Dormitories.Find(d => d.Name == dormitoryName);
        if (dormitory != null)
        {
            Console.WriteLine("Enter new dormitory name:");
            string newName = Console.ReadLine();
            Console.WriteLine("Enter new dormitory capacity:");
            int newDormitoryCapacity = Convert.ToInt32(Console.ReadLine());

            deanery.EditDormitory(dormitory, newName, newDormitoryCapacity);
            Console.WriteLine("Dormitory edited successfully.");
        }
        else
        {
            Console.WriteLine("Dormitory not found.");
        }
    }

    static void ViewAllDormitories(Deanery deanery)
    {
        Console.WriteLine("All Dormitories:");
        foreach (var dormitory in deanery.Dormitories)
        {
            Console.WriteLine(dormitory);
        }
    }

    static void Search(Deanery deanery)
    {
        Console.WriteLine("1. Search Students by Name");
        Console.WriteLine("2. Search Students by Group");
        Console.WriteLine("3. Search Students by Dormitory");
        Console.WriteLine("Enter your choice:");

        int choice;
        if (int.TryParse(Console.ReadLine(), out choice))
        {
            switch (choice)
            {
                case 1:
                    SearchStudentsByName(deanery);
                    break;
                case 2:
                    SearchStudentsByGroup(deanery);
                    break;
                case 3:
                    SearchStudentsByDormitory(deanery);
                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter a number.");
        }
    }

    static void SearchStudentsByName(Deanery deanery)
    {
        Console.WriteLine("Enter student's name:");
        string name = Console.ReadLine();

        List<Student> students = deanery.Students.FindAll(s => s.FirstName.Contains(name) || s.LastName.Contains(name));
        if (students.Count > 0)
        {
            Console.WriteLine("Matching Students:");
            foreach (var student in students)
            {
                Console.WriteLine(student);
            }
        }
        else
        {
            Console.WriteLine("No matching students found.");
        }
    }

    static void SearchStudentsByGroup(Deanery deanery)
    {
        Console.WriteLine("Enter group name:");
        string groupName = Console.ReadLine();

        List<Student> students = deanery.Students.FindAll(s => s.Group == groupName);
        if (students.Count > 0)
        {
            Console.WriteLine("Students in Group:");
            foreach (var student in students)
            {
                Console.WriteLine(student);
            }
        }
        else
        {
            Console.WriteLine("No students found in the group.");
        }
    }

    static void SearchStudentsByDormitory(Deanery deanery)
    {
        Console.WriteLine("Enter dormitory name:");
        string dormitoryName = Console.ReadLine();

        List<Student> students = deanery.Students.FindAll(d => d.Dormitory == dormitoryName);
        if (students.Count > 0)
        {
            Console.WriteLine("Students in Dormitory:");
            foreach (var student in students)
            {
                Console.WriteLine(student);
            }
        }
        else
        {
            Console.WriteLine("No students found in the dormitory.");
        }
    }

    static void AddStudentDorm(Deanery deanery)
    {
        Console.WriteLine("Enter student's first name:");
        string firstName = Console.ReadLine();
        Console.WriteLine("Enter student's last name:");
        string lastName = Console.ReadLine();

        Student student = deanery.Students.Find(s => s.FirstName == firstName && s.LastName == lastName);

        Console.WriteLine("Enter dormitory name:");
        string dormitoryName = Console.ReadLine();

        Dormitory dormitory = deanery.Dormitories.Find(d => d.Name == dormitoryName);

        deanery.AddStudentDorm(student, dormitoryName, dormitory);
    }

    static void RemoveStudentDorm(Deanery deanery)
    {
        Console.WriteLine("Enter student's first name:");
        string firstName = Console.ReadLine();
        Console.WriteLine("Enter student's last name:");
        string lastName = Console.ReadLine();

        Student student = deanery.Students.Find(s => s.FirstName == firstName && s.LastName == lastName);

        Console.WriteLine("Enter dormitory name:");
        string dormitoryName = Console.ReadLine();

        Dormitory dormitory = deanery.Dormitories.Find(d => d.Name == dormitoryName);

        deanery.RemoveStudentDorm(student, dormitory);
    }
}


using System;
using System.Collections.Generic;
using System.Linq;

// Клас для представлення запису у адресній книзі
class AddressRecord
{
    private string fullName;
    private DateTime birthDate;
    private List<string> phoneNumbers;
    private string address;
    private DateTime lastEdited;

    public AddressRecord(string fullName, DateTime birthDate, List<string> phoneNumbers, string address)
    {
        this.fullName = fullName;
        this.birthDate = birthDate;
        this.phoneNumbers = phoneNumbers;
        this.address = address;
        this.lastEdited = DateTime.Now;
    }

    // Геттери для приватних полів

    public string FullName => fullName;
    public DateTime BirthDate => birthDate;
    public DateTime LastEdited => lastEdited;

    public override string ToString()
    {
        return $"Full Name: {fullName}\n" +
               $"Birth Date: {birthDate}\n" +
               $"Phone Numbers: {string.Join(", ", phoneNumbers)}\n" +
               $"Address: {address}\n" +
               $"Last Edited: {lastEdited}\n";
    }
}

// Клас-контейнер для адресних записів (Зв'язний список)
class AddressBook<T> : IEnumerable<T> where T : AddressRecord
{
    private List<T> records;  // Контейнер-зв’язний список для зберігання записів

    public AddressBook()
    {
        records = new List<T>();  // Ініціалізація зв'язного списку
    }

    public void AddRecord(T record)
    {
        records.Add(record);  // Додавання запису до зв'язного списку
    }

    public void Sort(Comparison<T> comparison)
    {
        records.Sort(comparison);  // Сортування зв'язного списку за допомогою делегата порівняння
    }

    public IEnumerator<T> GetEnumerator()
    {
        return records.GetEnumerator();  // Повернення ітератора для зв'язного списку
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
}

// Утилітарний клас для роботи з контейнерами
class AddressBookUtils
{
    public static void PrintRecords<T>(AddressBook<T> addressBook) where T : AddressRecord
    {
        foreach (T record in addressBook)  // Використання параметризованого методу для ітерації по контейнеру
        {
            Console.WriteLine(record);
        }
    }
}

class Program
{
    public static void Main(string[] args)
    {
        AddressBook<AddressRecord> myAddressBook = new AddressBook<AddressRecord>();

        // Додавання записів до адресної книги
        myAddressBook.AddRecord(new AddressRecord("John Doe", DateTime.Now, new List<string> { "1234567890", "0987654321" }, "123 Main St"));
        myAddressBook.AddRecord(new AddressRecord("Jane Smith", DateTime.Now, new List<string> { "1111111111" }, "456 Park Ave"));
        myAddressBook.AddRecord(new AddressRecord("Michael Johnson", DateTime.Now, new List<string> { "2222222222", "3333333333" }, "789 Elm St"));

        // Створення делегата порівняння для сортування
        Comparison<AddressRecord> comparison = (a, b) =>
        {
            int result = a.FullName.CompareTo(b.FullName);
            if (result == 0) result = a.BirthDate.CompareTo(b.BirthDate);
            if (result == 0) result = a.LastEdited.CompareTo(b.LastEdited);
            return result;
        };

        // Сортування адресної книги
        myAddressBook.Sort(comparison);

        // Виведення записів адресної книги
        AddressBookUtils.PrintRecords(myAddressBook);
    }
}

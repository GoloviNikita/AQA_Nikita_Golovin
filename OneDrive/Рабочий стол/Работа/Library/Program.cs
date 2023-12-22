using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Program
{
    static Library library = new Library();

    static void Main()
    {
        InitializeLibrary();

        while (true)
        {
            Console.Clear();
            Console.WriteLine("===== Библиотека =====");
            Console.WriteLine("1. Просмотреть каталог");
            Console.WriteLine("2. Просмотреть отсортированный каталог");
            Console.WriteLine("3. Добавить книгу");
            Console.WriteLine("4. Редактировать информацию о книге");
            Console.WriteLine("5. Удалить книгу");
            Console.WriteLine("6. Поиск по автору");
            Console.WriteLine("7. Поиск по названию");
            Console.WriteLine("8. Выдать книгу");
            Console.WriteLine("9. Вернуть книгу");
            Console.WriteLine("10. Контактная информация");
            Console.WriteLine("11. Выход");
            Console.Write("Выберите действие (1-11): ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    DisplayCatalog();
                    break;
                case "2":
                    DisplaySortedCatalog();
                    break;
                case "3":
                    AddBook();
                    break;
                case "4":
                    EditBook();
                    break;
                case "5":
                    RemoveBook();
                    break;
                case "6":
                    SearchByAuthor();
                    break;
                case "7":
                    SearchByTitle();
                    break;
                case "8":
                    IssueBook();
                    break;
                case "9":
                    ReturnBook();
                    break;
                case "10":
                    DisplayLibraryInformation();
                    break;
                case "11":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Некорректный ввод. Пожалуйста, выберите от 1 до 11.");
                    break;
            }

            Console.WriteLine("\nНажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }
    }

    static void InitializeLibrary()
    {
        // Исходное заполнение каталога
        library.Catalog.Add(new Book("Война и мир", "Лев Толстой", 1869));
        library.Catalog.Add(new Book("Гарри Поттер и философский камень", "Джоан Роулинг", 1997));
        library.Catalog.Add(new Book("Грозовой перевал", "Эмили Бронте", 1847));
        library.Catalog.Add(new Book("Мастер и Маргарита", "Михаил Булгаков", 1967));
        library.Catalog.Add(new Book("О дивный новый мир", "Олдос Хаксли", 1932));
        library.Catalog.Add(new Book("Повелитель мух", "Уильям Голдинг", 1954));
        library.Catalog.Add(new Book("Сто лет одиночества", "Габриэль Гарсиа Маркес", 1967));
        library.Catalog.Add(new Book("Тень ветра", "Карлос Руис Сафон", 2001));
        library.Catalog.Add(new Book("Убийство в Восточном экспрессе", "Агата Кристи", 1934));
        library.Catalog.Add(new Book("1984", "Джордж Оруэлл", 1949));




        library.Name = "Название вашей библиотеки";
        library.Location.Country = "Страна";
        library.Location.City = "Город";
        library.Location.Address = "Адрес";
        library.Location.ZipCode = "Индекс";
        library.ContactNumber = "Контактный номер телефона";
        library.WorkingHours = "График работы библиотеки";
        library.LibrarianName = "Имя и фамилия библиотекаря";
    }

    static void DisplayCatalog()
    {
        Console.Clear();
        Console.WriteLine("===== Каталог книг =====");
        foreach (var book in library.Catalog)
        {
            Console.WriteLine(book);
        }
    }

    static void DisplaySortedCatalog()
    {
        Console.Clear();
        Console.WriteLine("===== Каталог книг (сортировка) =====");
        var sortedCatalog = library.Catalog.OrderBy(book => book.Author).ThenBy(book => book.Title);
        foreach (var book in sortedCatalog)
        {
            Console.WriteLine(book);
        }
    }

    static void AddBook()
    {
        Console.Clear();
        Console.WriteLine("===== Добавление новой книги =====");
        Console.Write("Введите название книги: ");
        string title = Console.ReadLine();
        Console.Write("Введите автора книги: ");
        string author = Console.ReadLine();
        Console.Write("Введите год выпуска: ");
        int year;
        while (!int.TryParse(Console.ReadLine(), out year))
        {
            Console.WriteLine("Некорректный ввод. Введите год еще раз: ");
        }

        library.Catalog.Add(new Book(title, author, year));
        Console.WriteLine("Книга успешно добавлена!");
    }

    static void EditBook()
    {
        Console.Clear();
        Console.WriteLine("===== Редактирование информации о книге =====");
        Console.Write("Введите название книги для редактирования: ");
        string titleToEdit = Console.ReadLine();

        Book bookToEdit = library.Catalog.Find(b => b.Title.Equals(titleToEdit, StringComparison.OrdinalIgnoreCase));

        if (bookToEdit != null)
        {
            Console.Write("Введите новое название книги: ");
            bookToEdit.Title = Console.ReadLine();

            Console.Write("Введите нового автора книги: ");
            bookToEdit.Author = Console.ReadLine();

            Console.Write("Введите новый год выпуска: ");
            if (!int.TryParse(Console.ReadLine(), out int newYear))
            {
                Console.WriteLine("Некорректный ввод. Введите год еще раз.");
                return;
            }
            bookToEdit.Year = newYear;

            Console.WriteLine("Информация о книге успешно отредактирована!");
        }
        else
        {
            Console.WriteLine("Книга не найдена.");
        }
    }

    static void RemoveBook()
    {
        Console.Clear();
        Console.WriteLine("===== Удаление книги =====");
        Console.Write("Введите название книги для удаления: ");
        string titleToRemove = Console.ReadLine();

        Book bookToRemove = library.Catalog.Find(b => b.Title.Equals(titleToRemove, StringComparison.OrdinalIgnoreCase));

        if (bookToRemove != null)
        {
            library.Catalog.Remove(bookToRemove);
            Console.WriteLine("Книга успешно удалена!");
        }
        else
        {
            Console.WriteLine("Книга не найдена.");
        }
    }

    static void SearchByAuthor()
    {
        Console.Clear();
        Console.WriteLine("===== Поиск книги по автору =====");
        Console.Write("Введите имя автора (минимум 2 символа): ");
        string authorQuery = Console.ReadLine();

        var results = library.Catalog
            .Where(book => book.Author.IndexOf(authorQuery, StringComparison.OrdinalIgnoreCase) >= 0)
            .ToList();

        DisplaySearchResults(results);
    }

    static void SearchByTitle()
    {
        Console.Clear();
        Console.WriteLine("===== Поиск книги по названию =====");
        Console.Write("Введите название книги (минимум 2 символа): ");
        string titleQuery = Console.ReadLine();

        var results = library.Catalog
            .Where(book => book.Title.IndexOf(titleQuery, StringComparison.OrdinalIgnoreCase) >= 0)
            .ToList();

        DisplaySearchResults(results);
    }

    static void DisplaySearchResults(List<Book> results)
    {
        if (results.Any())
        {
            Console.WriteLine("Результаты поиска:");
            foreach (var book in results)
            {
                Console.WriteLine(book);
            }
        }
        else
        {
            Console.WriteLine("Книги не найдены.");
        }
    }

    static void IssueBook()
    {
        Console.Clear();
        Console.WriteLine("===== Выдача книги клиенту =====");
        Console.Write("Введите название книги: ");
        string titleToIssue = Console.ReadLine();

        Book bookToIssue = library.Catalog.Find(b => b.Title.Equals(titleToIssue, StringComparison.OrdinalIgnoreCase));

        if (bookToIssue != null)
        {
            if (!bookToIssue.IsAvailable)
            {
                Console.WriteLine("Книга уже выдана.");
                return;
            }

            Console.Write("Введите имя клиента: ");
            string clientFirstName = Console.ReadLine();

            Console.Write("Введите фамилию клиента: ");
            string clientLastName = Console.ReadLine();

            Console.Write("Введите номер мобильного телефона клиента: ");
            string clientPhoneNumber = Console.ReadLine();

            Console.Write("Введите дату выдачи (формат: DD.MM.YYYY): ");
            if (!DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime issueDate))
            {
                Console.WriteLine("Некорректный ввод даты. Операция отменена.");
                return;
            }

            bookToIssue.IssueTo(new Client(clientFirstName, clientLastName, clientPhoneNumber), issueDate);

            Console.WriteLine($"Книга \"{bookToIssue.Title}\" успешно выдана клиенту {clientFirstName} {clientLastName}.");
        }
        else
        {
            Console.WriteLine("Книга не найдена.");
        }
    }

    static void ReturnBook()
    {
        Console.Clear();
        Console.WriteLine("===== Возврат книги клиентом =====");
        Console.Write("Введите название книги: ");
        string titleToReturn = Console.ReadLine();

        Book bookToReturn = library.Catalog.Find(b => b.Title.Equals(titleToReturn, StringComparison.OrdinalIgnoreCase));

        if (bookToReturn != null)
        {
            if (bookToReturn.IsAvailable)
            {
                Console.WriteLine("Книга уже доступна в библиотеке.");
                return;
            }

            Console.Write("Введите дату возврата (формат: DD.MM.YYYY): ");
            if (!DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime returnDate))
            {
                Console.WriteLine("Некорректный ввод даты. Операция отменена.");
                return;
            }

            bookToReturn.ReturnBook(returnDate);

            Console.WriteLine($"Книга \"{bookToReturn.Title}\" успешно возвращена в библиотеку.");
        }
        else
        {
            Console.WriteLine("Книга не найдена.");
        }
    }

    static void DisplayLibraryInformation()
    {
        Console.Clear();
        Console.WriteLine("===== Контактная информация о библиотеке =====");
        Console.WriteLine($"Название: {library.Name}");
        Console.WriteLine($"Адрес: {library.Location.Country}, {library.Location.City}, {library.Location.Address}, {library.Location.ZipCode}");
        Console.WriteLine($"Телефон: {library.ContactNumber}");
        Console.WriteLine($"График работы: {library.WorkingHours}");
        Console.WriteLine($"Библиотекарь: {library.LibrarianName}");
    }
}

class Library
{
    public string Name { get; set; }
    public Location Location { get; set; } = new Location();
    public string ContactNumber { get; set; }
    public string WorkingHours { get; set; }
    public string LibrarianName { get; set; }
    public List<Book> Catalog { get; set; } = new List<Book>();
}

class Location
{
    public string Country { get; set; }
    public string City { get; set; }
    public string Address { get; set; }
    public string ZipCode { get; set; }
}

class Book
{
    public string Title { get; set; }
    public string Author { get; set; }
    public int Year { get; set; }
    public bool IsAvailable { get; private set; } = true;
    public Client IssuedTo { get; private set; }
    public DateTime IssueDate { get; private set; }
    public DateTime ReturnDate { get; private set; }

    public Book(string title, string author, int year)
    {
        Title = title;
        Author = author;
        Year = year;
    }

    public void IssueTo(Client client, DateTime issueDate)
    {
        IsAvailable = false;
        IssuedTo = client;
        IssueDate = issueDate;
    }

    public void ReturnBook(DateTime returnDate)
    {
        IsAvailable = true;
        ReturnDate = returnDate;
        IssuedTo = null;
        IssueDate = DateTime.MinValue;
    }

    public override string ToString()
    {
        return $"{Title} - {Author} ({Year}){(IsAvailable ? " - Доступна" : $" - Выдана клиенту: {IssuedTo.FirstName} {IssuedTo.LastName}, дата выдачи: {IssueDate.ToString("dd.MM.yyyy")}")}";
    }
}

class Client
{
    public string FirstName { get; }
    public string LastName { get; }
    public string PhoneNumber { get; }

    public Client(string firstName, string lastName, string phoneNumber)
    {
        FirstName = firstName;
        LastName = lastName;
        PhoneNumber = phoneNumber;
    }
}


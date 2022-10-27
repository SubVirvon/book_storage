using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace book_storage
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string CommandAddBook = "1";
            const string CommandRemoveBook = "2";
            const string CommandShowAllBooks = "3";
            const string CommandShowBooksByTitle = "4";
            const string CommandShowBooksByAuthor = "5";
            const string CommandShowBooksByReleas = "6";
            const string CommandExit = "7";
            bool isFinish = false;
            Storage storage = new Storage();

            while(isFinish == false)
            {
                Console.Clear();
                Console.Write($"Хранилище книг\n\nВыберите команду\n{CommandAddBook}. Добавить книгу\n{CommandRemoveBook}. Убрать книгу\n{CommandShowAllBooks}. Показать все книги\n{CommandShowBooksByTitle}. Показать кникги по названию\n{CommandShowBooksByAuthor}. Показать книги по автору\n{CommandShowBooksByReleas}. Показать книги по году издания\n{CommandExit}. Завершить\nВведите команду: ");

                switch (Console.ReadLine())
                {
                    case CommandAddBook:
                        ChangeBooksCount(false, storage);
                        break;
                    case CommandRemoveBook:
                        ChangeBooksCount(true, storage);
                        break;
                    case CommandShowAllBooks:
                        storage.ShowAllBooks();
                        break;
                    case CommandShowBooksByTitle:
                        ShowBooksByTitle(storage);
                        break;
                    case CommandShowBooksByAuthor:
                        ShowBooksByAuthor(storage);
                        break;
                    case CommandShowBooksByReleas:
                        ShowBooksByRelease(storage);
                        break;
                    case CommandExit:
                        Console.Clear();
                        Console.WriteLine("Программа завершена");
                        isFinish = true;
                        break;
                    default:
                        Console.WriteLine("Некоректная команда");
                        break;
                }

                Console.ReadKey();
            }
        }

        static void ShowBooksByTitle(Storage storage)
        {
            string title = SetSearchParameter("Найти книги по названию\nВведите название книги: ");
            storage.ShowBooksByTitle(title);
        }

        static void ShowBooksByAuthor(Storage storage)
        {
            string author = SetSearchParameter("Найти книги по автору\nВведите автора книги: ");
            storage.ShowBooksByAuthor(author);
        }

        static void ShowBooksByRelease(Storage storage)
        {
            string release = SetSearchParameter("Найти книги по году издания\nВведите год издания книги: ");
            storage.ShowBooksByRelease(release);
        }

        static string SetSearchParameter(string requestText)
        {
            Console.Clear();
            Console.Write(requestText);

            string serchParameter = Console.ReadLine();

            return serchParameter;
        }

        static void ChangeBooksCount(bool isDelete, Storage storage)
        {
            Console.Clear();

            if(isDelete)
                Console.WriteLine("Удалить книгу");
            else
                Console.WriteLine("Добавить книгу");

            Console.Write("Введите название книги: ");
            string title = Console.ReadLine();
            Console.Write("Введите автора книги: ");
            string author = Console.ReadLine();
            Console.Write("Введите год выпуска книги: ");
            string release = Console.ReadLine();

            if(isDelete)
                storage.RemoveBook(title, author, release);
            else
                storage.AddBook(title, author, release);
        }
    }
    
    class Book
    {
        public string Title { get; private set; }
        public string Author { get; private set; }
        public string Release { get; private set; }

        public Book(string title, string author, string release)
        {
            Title = title;
            Author = author;
            Release = release;
        }
    }

    class Storage
    {
        List<Book> books = new List<Book>();

        public void AddBook(string title, string author, string release)
        {
            books.Add(new Book(title, author, release));
        }

        public void RemoveBook(string title, string author, string release)
        {
            bool isFind = false;

            foreach(var book in books)
            {
                if(book.Title == title && book.Author == author && book.Release == release)
                {
                    books.Remove(book);
                    isFind = true;
                }
            }

            if (isFind == false)
                Console.WriteLine("Книга не найдена");
        }

        public void ShowAllBooks()
        {
            foreach(var book in books)
            {
                WriteBookInfo(book);
            }
        }

        public void ShowBooksByTitle(string title)
        {
            foreach (var book in books)
            {
                if(book.Title == title)
                    WriteBookInfo(book);
            }
        }

        public void ShowBooksByAuthor(string author)
        {
            foreach(var book in books)
            {
                if (book.Author == author)
                    WriteBookInfo(book);
            }
        }

        public void ShowBooksByRelease(string release)
        {
            foreach (var book in books)
            {
                if (book.Release == release)
                    WriteBookInfo(book);
            }
        }

        private void WriteBookInfo(Book book)
        {
            Console.WriteLine($"{book.Title} - {book.Author} ({book.Release})");
        }
    }
}

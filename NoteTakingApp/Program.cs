using System;
using System.Collections.Generic;

public class NoteTakingApp
{
    public static void Main(string[] args)
    {
        var noteManager = new NoteManager();
        Console.Title = "Note Taking Application";
        Console.WriteLine("Welcome to the note-taking app!");
        Console.WriteLine("===============================\n");

        Console.WriteLine("1. Add a note");
        Console.WriteLine("2. Remove a note");
        Console.WriteLine("3. Edit a note");
        Console.WriteLine("4. Display notes");
        Console.WriteLine("5. Exit");
        Console.Write("Choice: ");
        if (int.TryParse(Console.ReadLine(), out int selection))
        {
            if (selection >= 1 && selection <= 5)
            {
                try
                {
                    if (selection == 1)
                    {
                        Console.Clear();
                        Console.WriteLine("Add a new note:");
                        Console.Write("Enter the title for the note: ");
                        string title = Console.ReadLine();
                        Console.Write("Enter the content for the note: ");
                        string content = Console.ReadLine();
                        DateTime creationDate = DateTime.Now;

                        noteManager.AddNote(new Note()
                        {
                            Title = title,
                            Content = content,
                            CreationDate = creationDate,
                        });
                        Console.Clear();
                        Console.WriteLine("====================\nList of notes");
                        noteManager.DisplayNotes();
                    }
                    else if (selection == 2)
                    {
                        Console.Write("Enter the title of the note to remove: ");
                        string noteTitleToRemove = Console.ReadLine();
                        noteManager.RemoveNote(noteTitleToRemove);
                    }
                    else if (selection == 3)
                    {
                        Console.Write("Enter the title of the note to edit: ");
                        string noteTitleToEdit = Console.ReadLine();
                        noteManager.EditNote(noteTitleToEdit);
                    }
                    else if (selection == 4)
                    {
                        Console.Clear();
                        Console.WriteLine("====================\nList of notes");
                        noteManager.DisplayNotes();
                    }
                    else if (selection == 5)
                    {
                        Console.WriteLine("Goodbye!");
                        Environment.Exit(0);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                Console.WriteLine("Please select a number between 1 and 5.");

            }
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter a number between 1 and 5.");
        }    


    }
}

internal class Note
{
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime CreationDate { get; set; }
}

internal class NoteManager
{
    public List<Note> notes = new List<Note>();

    public void AddNote(Note note)
    {
        notes.Add(note);
    }

    public void RemoveNote(string title)
    {
        var noteToRemove = notes.Find(note => note.Title == title);
        if (noteToRemove == null)
        {
            throw new NoteNotFoundException(title);
        }

        notes.Remove(noteToRemove);
    }

    public void EditNote(string title)
    {
        var noteToEdit = notes.Find(note => note.Title == title);
        if (noteToEdit == null)
        {
            throw new NoteNotFoundException(title);
        }

        Console.Write("Enter the new title for the note: ");
        noteToEdit.Title = Console.ReadLine();

        Console.Write("Enter the new content for the note: ");
        noteToEdit.Content = Console.ReadLine();
    }

    public void DisplayNotes()
    {
        foreach (Note note in notes)
        {
            Console.WriteLine($"Title: {note.Title}");
            Console.WriteLine($"Content: {note.Content}");
            Console.WriteLine($"Creation date: {note.CreationDate}");
            Console.WriteLine();
        }
    }
}

internal class NoteNotFoundException : Exception
{
    public NoteNotFoundException(string noteTitle)
        : base($"Note with title '{noteTitle}' was not found.")
    {
    }
}

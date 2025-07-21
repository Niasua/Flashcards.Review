using Flashcards.Niasua.DTOs;
using Flashcards.Niasua.Services;
using Spectre.Console;
using System.Data;

namespace Flashcards.Niasua.UI;

public class FlashcardMenu
{
    public static void Show()
    {
        bool exit = false;

        while (!exit)
        {
            Console.Clear();
            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("[green]Flashcards Menu[/]")
                .AddChoices(new[]
                {
                    "Add",
                    "View",
                    "Edit",
                    "Delete",
                    "Back"
                }));

            switch (choice)
            {
                case "Add":

                    AddFlashcard();

                    break;

                case "View":

                    ViewFlashcards();

                    break;

                case "Edit":

                    EditFlashcards();

                    break;

                case "Delete":

                    DeleteFlashcard();

                    break;

                case "Back":

                    exit = true;

                    break;
            }
        }
    }

    public static void AddFlashcard()
    {
        while (true)
        {
            Console.Clear();
            AnsiConsole.MarkupLine("[red]Create Flashcard:[/]\n");

            AnsiConsole.MarkupLine("Type the [green]name[/] of the Flashcard Stack (type 'zzz' to return to the menu):");
            var stackName = Console.ReadLine();
            if (stackName?.ToLower() == "zzz") break;

            AnsiConsole.MarkupLine("\nType the [green]question[/] (type 'zzz' to return to the menu):");
            var question = Console.ReadLine();
            if (question?.ToLower() == "zzz") break;

            AnsiConsole.MarkupLine("\nType the [green]answer[/] (type 'zzz' to return to the menu):");
            var answer = Console.ReadLine();
            if (answer?.ToLower() == "zzz") break;

            var creation = FlashcardService.CreateFlashcard(stackName, question, answer);

            if (creation)
            {
                AnsiConsole.MarkupLine("\n[green]Flashcard successfully added![/]:");
            }
            else
            {
                AnsiConsole.MarkupLine("\n[red]The stack was not found[/]:");
            }

            AnsiConsole.MarkupLine("\nPress any key to add another flashcard or type [red]'zzz'[/] to return.");

            var input = Console.ReadLine();
            if (input?.ToLower() == "zzz") break;
        }
    }
    private static void ViewFlashcards()
    {
        var isStudying = false;

        while (true)
        {
            Console.Clear();
            AnsiConsole.MarkupLine("[red]View Flashcards:[/]\n");

            AnsiConsole.MarkupLine("Type the [green]name[/] of the Flashcard Stack (type 'zzz' to return to the menu):");
            var stackName = Console.ReadLine();
            if (stackName?.ToLower() == "zzz") break;

            var flashcards = FlashcardService.GetFlashcardsByStack(stackName);

            if (flashcards == null)
            {
                AnsiConsole.MarkupLine("\n[red]The stack was not found[/]:");
                break;
            }

            Display.ShowFlashcards(flashcards, isStudying);

            Console.Clear();

            AnsiConsole.MarkupLine("\nPress any key to view Flashcards again or [red]'zzz'[/] to return.");

            var input = Console.ReadLine();
            if (input?.ToLower() == "zzz") break;
        }
    }
    private static void EditFlashcards()
    {
        while (true)
        {
            Console.Clear();
            AnsiConsole.MarkupLine("[red]Edit Flashcards:[/]\n");

            AnsiConsole.MarkupLine("Type the [green]name[/] of the Flashcard Stack (type 'zzz' to return to the menu):");
            var stackName = Console.ReadLine();
            if (stackName?.ToLower() == "zzz") break;

            List<FlashcardDTO> flashcards = FlashcardService.GetFlashcardsByStack(stackName);

            if (flashcards == null || flashcards.Count == 0)
            {
                AnsiConsole.MarkupLine("[red]No flashcards found in that stack.[/]");
                Console.ReadKey();
                continue;
            }

            Display.ShowFlaschardsTable(flashcards);

            AnsiConsole.MarkupLine("\nType the ID of the flashcard you want to update (type 'zzz' to return to the menu):");
            var flashcardId = Console.ReadLine();
            if (flashcardId.ToLower() == "zzz") break;

            if (!int.TryParse(flashcardId, out int displayId) || displayId < 1 || displayId > flashcards.Count)
            {
                AnsiConsole.MarkupLine("\n[red]Invalid ID. Press any key to try again...[/]:\n");
                Console.ReadKey();
                continue;
            }

            Console.Clear();
            AnsiConsole.MarkupLine($"[yellow]Editing flashcard #{displayId}[/]");
            AnsiConsole.MarkupLine("\nType the new [green]question[/] (type 'zzz' to return to the menu):");
            var question = Console.ReadLine();
            if (question?.ToLower() == "zzz") break;

            AnsiConsole.MarkupLine("\nType the new [green]answer[/] (type 'zzz' to return to the menu):");
            var answer = Console.ReadLine();
            if (answer?.ToLower() == "zzz") break;

            var update = FlashcardService.UpdateFlashcard(stackName, displayId, question, answer);

            if (update)
            {
                AnsiConsole.MarkupLine("\n[green]Flashcard successfully updated![/]:");
            }
            else
            {
                AnsiConsole.MarkupLine("\n[red]The stack was not found[/]:");
            }

            AnsiConsole.MarkupLine("\nPress any key to update another flashcard or type [red]'zzz'[/] to return.");

            var input = Console.ReadLine();
            if (input?.ToLower() == "zzz") break;
        }
    }
    private static void DeleteFlashcard()
    {
        while (true)
        {
            Console.Clear();
            AnsiConsole.MarkupLine("[red]Delete Flashcard:[/]\n");

            AnsiConsole.MarkupLine("Type the [green]name[/] of the Flashcard Stack (type 'zzz' to return to the menu):");
            var stackName = Console.ReadLine();
            if (stackName?.ToLower() == "zzz") break;

            List<FlashcardDTO> flashcards = FlashcardService.GetFlashcardsByStack(stackName);

            if (flashcards == null || flashcards.Count == 0)
            {
                AnsiConsole.MarkupLine("[red]No flashcards found in that stack.[/]");
                Console.ReadKey();
                continue;
            }

            Display.ShowFlaschardsTable(flashcards);

            AnsiConsole.MarkupLine("\nType the ID of the flashcard you want to delete (type 'zzz' to return to the menu):");
            var flashcardId = Console.ReadLine();
            if (flashcardId?.ToLower() == "zzz") break;

            if (!int.TryParse(flashcardId, out int displayId) || displayId < 1 || displayId > flashcards.Count)
            {
                AnsiConsole.MarkupLine("\n[red]Invalid ID. Press any key to try again...[/]:\n");
                Console.ReadKey();
                continue;
            }

            Console.Clear();
            AnsiConsole.MarkupLine($"[yellow]Deleting flashcard #{displayId}[/]");
            AnsiConsole.MarkupLine("\nAre you sure you want to delete this Flashcard? (y/n):");
            var confirm = Console.ReadLine();
            if (confirm?.ToLower() != "y") continue;

            var delete = FlashcardService.DeleteFlashcard(stackName, displayId);

            if (delete)
            {
                AnsiConsole.MarkupLine("\n[green]Flashcard successfully deleted![/]:");
            }
            else
            {
                AnsiConsole.MarkupLine("\n[red]The stack was not found[/]:");
            }

            AnsiConsole.MarkupLine("\nPress any key to delete another flashcard or type [red]'zzz'[/] to return.");

            var input = Console.ReadLine();
            if (input?.ToLower() == "zzz") break;
        }
    }
}

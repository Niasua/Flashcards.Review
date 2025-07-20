using Flashcards.Niasua.Services;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Spectre.Console;

namespace Flashcards.Niasua.UI;

public static class Menu
{
    public static void Show()
    {
        bool exit = false;

        while (!exit)
        {
            Console.Clear();
            AnsiConsole.MarkupLine("[blue]--- Flashcards App ---[/]\n");
            AnsiConsole.MarkupLine("Select an option:\n");

            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("Choose a [green]section[/].")
                .AddChoices(new[]
                {
                    "Flashcards",
                    "Stacks",
                    "Study Sessions",
                    "Exit"
                }));

            switch (choice)
            {
                case "Flashcards":

                    FlashcardMenu.Show();

                    break;

                case "Stacks":

                    StackMenu.Show();

                    break;

                case "Study Sessions":

                    StudySessionMenu.Show();

                    break;

                case "Exit":

                    Console.Clear();
                    AnsiConsole.MarkupLine("[grey]Goodbye![/]");
                    Thread.Sleep(1000);
                    exit = true;

                    break;
            }
        }
    }
}

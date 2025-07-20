using Flashcards.Niasua.DTOs;
using Flashcards.Niasua.Services;
using Spectre.Console;
using System.Data;

namespace Flashcards.Niasua.UI;

public class StackMenu
{
    public static void Show()
    {
        bool exit = false;

        while (!exit)
        {
            Console.Clear();
            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("[green]Stacks Menu[/]")
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

                    AddStack();

                    break;

                case "View":

                    ViewStacks();

                    break;

                case "Edit":

                    EditStack();

                    break;

                case "Delete":

                    DeleteStack();

                    break;

                case "Back":

                    exit = true;

                    break;
            }
        }
    }

    private static void AddStack()
    {
        while (true)
        {
            Console.Clear();
            AnsiConsole.MarkupLine("[red]Create Stack:[/]\n");

            AnsiConsole.MarkupLine("Type the [green]name[/] of the Stack (type 'zzz' to return to the menu):");
            var stackName = Console.ReadLine();
            if (stackName?.ToLower() == "zzz") break;

            if (string.IsNullOrWhiteSpace(stackName))
            {
                AnsiConsole.MarkupLine("[red]The stack name cannot be empty.[/]");
                continue;
            }

            var creation = StackService.CreateStack(stackName);

            if (creation)
            {
                AnsiConsole.MarkupLine("\n[green]Stack successfully added![/]:");
            }
            else
            {
                AnsiConsole.MarkupLine("\n[red]A stack with that name already exists[/]:");
            }

            AnsiConsole.MarkupLine("\nPress any key to add another stack or type [red]'zzz'[/] to return.");

            var input = Console.ReadLine();
            if (input?.ToLower() == "zzz") break;
        }
    }
    private static void ViewStacks()
    {
        while (true)
        {
            Console.Clear();
            AnsiConsole.MarkupLine("[red]View Stacks:[/]\n");

            var stacks = StackService.GetAllStacks();

            if (stacks == null || stacks.Count == 0)
            {
                AnsiConsole.MarkupLine("[red]No stacks found.[/]");
                Console.ReadKey();
                continue;
            }

            Display.ShowStacks(stacks);

            AnsiConsole.MarkupLine("\n[grey]Press any key to to return...[/]");
            Console.ReadKey();
            break;
        }
    }
    private static void EditStack()
    {
        while (true)
        {
            Console.Clear();
            AnsiConsole.MarkupLine("[red]Edit Stack:[/]\n");

            var stacks = StackService.GetAllStacks();

            if (stacks == null || stacks.Count == 0)
            {
                AnsiConsole.MarkupLine("[red]No stacks found.[/]");
                Console.ReadKey();
                continue;
            }

            Display.ShowStacks(stacks);

            AnsiConsole.MarkupLine("\nType the ID of the Stack you want to update (type 'zzz' to return to the menu):");
            var stackId = Console.ReadLine();
            if (stackId.ToLower() == "zzz") break;

            if (!int.TryParse(stackId, out int displayId) || displayId < 1 || displayId > stacks.Count)
            {
                AnsiConsole.MarkupLine("\n[red]Invalid ID. Press any key to try again...[/]:\n");
                Console.ReadKey();
                continue;
            }

            var stackToUpdate = stacks[displayId - 1];

            Console.Clear();
            AnsiConsole.MarkupLine($"[yellow]Editing Stack [green]{stackToUpdate.Name}[/][/]");
            AnsiConsole.MarkupLine("\nType the new [green]name[/] of the stack (type 'zzz' to return to the menu):");
            var newName = Console.ReadLine();
            if (newName?.ToLower() == "zzz") break;

            if (string.IsNullOrWhiteSpace(newName))
            {
                AnsiConsole.MarkupLine("\n[red]Stack name cannot be empty.[/]");
                Console.ReadKey();
                continue;
            }

            var update = StackService.UpdateStack(displayId, newName);

            if (update)
            {
                AnsiConsole.MarkupLine("\n[green]Stack successfully updated![/]:");
            }
            else
            {
                AnsiConsole.MarkupLine("\n[red]The stack was not found[/]:");
            }

            AnsiConsole.MarkupLine("\nPress any key to update another stack or type [red]'zzz'[/] to return.");

            var input = Console.ReadLine();
            if (input?.ToLower() == "zzz") break;
        }
    }
    private static void DeleteStack()
    {
        while (true)
        {
            Console.Clear();
            AnsiConsole.MarkupLine("[red]Delete Stack:[/]\n");

            var stacks = StackService.GetAllStacks();

            if (stacks == null || stacks.Count == 0)
            {
                AnsiConsole.MarkupLine("[red]No stacks found.[/]");
                Console.ReadKey();
                continue;
            }

            Display.ShowStacks(stacks);

            AnsiConsole.MarkupLine("\nType the ID of the Stack you want to delete (type 'zzz' to return to the menu):");
            var stackId = Console.ReadLine();
            if (stackId?.ToLower() == "zzz") break;

            if (!int.TryParse(stackId, out int displayId) || displayId < 1 || displayId > stacks.Count)
            {
                AnsiConsole.MarkupLine("\n[red]Invalid ID. Press any key to try again...[/]:\n");
                Console.ReadKey();
                continue;
            }

            var stackToDelete = stacks[displayId - 1];

            Console.Clear();
            AnsiConsole.MarkupLine($"[yellow]Deleting Stack {stackToDelete.Name}[/]");
            AnsiConsole.MarkupLine("\nAre you sure you want to delete this Stack? (y/n):");
            var confirm = Console.ReadLine();
            if (confirm?.ToLower() != "y")
            {
                AnsiConsole.MarkupLine("\n[grey]Deletion cancelled. Press any key to continue...[/]");
                Console.ReadKey();
                continue;
            }

            var delete = StackService.DeleteStack(displayId);

            if (delete)
            {
                AnsiConsole.MarkupLine("\n[green]Stack successfully deleted![/]:");
            }
            else
            {
                AnsiConsole.MarkupLine("\n[red]The stack was not found[/]:");
            }

            AnsiConsole.MarkupLine("\nPress any key to delete another Stack or type [red]'zzz'[/] to return.");

            var input = Console.ReadLine();
            if (input?.ToLower() == "zzz") break;
        }
    }
}

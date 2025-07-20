using Flashcards.Niasua.DTOs;
using Flashcards.Niasua.Models;
using Spectre.Console;
using System.Collections;
using System.Security.Cryptography.X509Certificates;

namespace Flashcards.Niasua.UI;

public class Display
{
    internal static int? ShowFlashcards(List<FlashcardDTO> flashcards, bool isStudying)
    {
        Console.Clear();
        if (flashcards == null || flashcards.Count == 0)
        {
            AnsiConsole.MarkupLine("[red]No flashcards found in this stack.[/]");
            Console.ReadKey();
            return null;
        }

        int index = 0;
        bool showingQuestion = true;

        int score = 0;

        void NextCard()
        {
            index++;
            if (index >= flashcards.Count)
                index = 0;
            showingQuestion = true;
        }

        while (true)
        {
            Console.Clear();
            var card = flashcards[index];

            AnsiConsole.MarkupLine($"[yellow]Flashcard {index + 1}/{flashcards.Count}[/]");
            if (isStudying)
                AnsiConsole.MarkupLine($"[bold yellow]Progress: {score}/{flashcards.Count}[/]");

            if (showingQuestion)
            {
                AnsiConsole.MarkupLine("[bold]Question[/]");
                AnsiConsole.MarkupLine($"{card.Question}");
                AnsiConsole.MarkupLine("\nPress [green]Enter[/] to reveal answer, [red]'n'[/] next card, [red]'q'[/] quit.");
            }
            else
            {
                AnsiConsole.MarkupLine($"[yellow]Flashcard {index + 1}/{flashcards.Count}[/]");
                AnsiConsole.MarkupLine("[bold]Answer:[/]");
                AnsiConsole.MarkupLine($"{card.Answer}");
                if (isStudying)
                    AnsiConsole.MarkupLine("\nPress [green]Enter[/] to show question again, [green]H[/] for Hit, [red]M[/] for Miss, [yellow]'n'[/] next card, [red]'q'[/] quit.");
                else
                    AnsiConsole.MarkupLine("\nPress [green]Enter[/] to show question again, [red]'n'[/] next card, [red]'q'[/] quit.");
            }

            var key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.Q)
                break;

            if(key.Key == ConsoleKey.N)
            {
                NextCard();
            }
            else if (key.Key == ConsoleKey.Enter)
            {
                showingQuestion = !showingQuestion;
            }
            else if (isStudying && key.Key == ConsoleKey.H)
            {
                score++;
                index++;
                AnsiConsole.MarkupLine("[green]Hit![/]");
                Thread.Sleep(500);
                NextCard();
            }
            else if (isStudying && key.Key == ConsoleKey.M)
            {
                score--;
                index++;
                AnsiConsole.MarkupLine("[red]Miss![/]");
                Thread.Sleep(500);
                NextCard();
            }
        }

        if (isStudying)
        {
            AnsiConsole.MarkupLine($"\n[green]Study Session Complete![/]");
            AnsiConsole.MarkupLine($"Final Score: [bold]{score}[/]");
        }

        return score;
    }

    internal static void ShowFlaschardsTable(List<FlashcardDTO> flashcards)
    {
        var table = new Table();
        table.Border(TableBorder.Rounded);
        table.AddColumn("[yellow]ID[/]");
        table.AddColumn("[blue]Question[/]");
        table.AddColumn("[green]Answer[/]");

        foreach (var flashcard in flashcards)
        { 
            table.AddRow(
                flashcard.DisplayId.ToString(),
                flashcard.Question.ToString(),
                flashcard.Answer.ToString()
                );
        }

        AnsiConsole.Write(table);
    }

    internal static void ShowStacks(List<Models.Stack> stacks)
    {
        var table = new Table();
        table.Border(TableBorder.Rounded);
        table.AddColumn("[yellow]ID[/]");
        table.AddColumn("[blue]Name[/]");

        foreach (var stack in stacks)
        {
            table.AddRow(
                stack.Id.ToString(),
                stack.Name.ToString()
                );
        }

        AnsiConsole.Write(table);
    }

    internal static void ShowStudySessions(List<StudySessionDTO> sessions)
    {
        var table = new Table();
        table.Border(TableBorder.Rounded);
        table.AddColumn("[yellow]Stack Name[/]");
        table.AddColumn("[blue]Date[/]");
        table.AddColumn("[green]Score[/]");

        foreach (var session in sessions)
        {
            table.AddRow(
                session.StackName.ToString(),
                session.Date.ToString("dd-MM-yyyy HH:mm"),
                session.Score.ToString()
                );
        }

        AnsiConsole.Write(table);
    }
}

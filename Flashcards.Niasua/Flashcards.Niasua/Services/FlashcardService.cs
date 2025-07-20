using Flashcards.Niasua.Data;
using Flashcards.Niasua.DTOs;
using Flashcards.Niasua.Models;
using Microsoft.EntityFrameworkCore;

namespace Flashcards.Niasua.Services;

public class FlashcardService
{
    public static bool CreateFlashcard(string stackName, string question, string answer)
    {
        using var context = new AppDbContext();

        var stack = context.Stacks
            .FirstOrDefault(s => s.Name == stackName);

        if (stack == null) return false;

        var flashcard = new Flashcard { Question = question, Answer = answer, StackId = stack.Id };
        context.Flashcards.Add(flashcard);
        context.SaveChanges();
        return true;
    }

    public static List<FlashcardDTO> GetFlashcardsByStack(string stackName)
    {
        using var context = new AppDbContext();

        var stack = context.Stacks
            .Include(s => s.Flashcards)
            .FirstOrDefault(s => s.Name == stackName);

        if (stack == null) return null;

        return stack.Flashcards
            .OrderBy(f => f.Id)
            .Select((f, i) => new FlashcardDTO
        {
            DisplayId = i + 1,
            Question = f.Question,
            Answer = f.Answer
        }).ToList();
    } 

    public static bool DeleteFlashcard(string stackName, int displayId)
    {
        using var context = new AppDbContext();

        var stack = context.Stacks
            .Include(s => s.Flashcards.OrderBy(f => f.Id))
            .FirstOrDefault(s => s.Name == stackName);

        if (stack == null)
            return false;

        var flashcards = stack.Flashcards.OrderBy(f => f.Id).ToList();

        if (displayId < 1 || displayId > flashcards.Count)
            return false;

        var flashcardToDelete = flashcards[displayId - 1];

        context.Flashcards.Remove(flashcardToDelete);
        context.SaveChanges();

        return true;
    }

    public static bool UpdateFlashcard(string stackName, int displayId, string newQuestion, string newAnswer)
    {
        using var context = new AppDbContext();

        var stack = context.Stacks
            .Include(s => s.Flashcards.OrderBy(f => f.Id))
            .FirstOrDefault(s => s.Name == stackName);

        if (stack == null)
            return false;

        var flashcards = stack.Flashcards.OrderBy(f => f.Id).ToList();

        if (displayId < 1 || displayId > flashcards.Count)
            return false;

        var flashcardToUpdate = flashcards[displayId - 1];
        flashcardToUpdate.Question = newQuestion;
        flashcardToUpdate.Answer = newAnswer;

        context.SaveChanges();

        return true;
    }
}

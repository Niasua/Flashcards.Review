using Flashcards.Niasua.Data;
using Flashcards.Niasua.Models;
using Microsoft.EntityFrameworkCore;

namespace Flashcards.Niasua.Services;

public class StackService
{
    public static bool CreateStack(string stackName)
    {
        using var context = new AppDbContext();

        if (context.Stacks.Any(s => s.Name == stackName))
        {
            return false;
        }

        var stack = new Stack { Name = stackName };

        context.Stacks.Add(stack);
        context.SaveChanges();

        return true;
    }

    public static List<Stack> GetAllStacks()
    {
        using var context = new AppDbContext();

        return context.Stacks
            .OrderBy(s => s.Id)
            .Include(s => s.Flashcards)
            .Include(s => s.StudySessions)
            .ToList();
    }

    public static bool DeleteStack(int displayId)
    {
        using var context = new AppDbContext();

        var stacks = context.Stacks
            .OrderBy(s => s.Id).ToList();

        if (displayId < 1 || displayId > stacks.Count)
            return false;

        var stackToDelete = stacks[displayId - 1];

        context.Stacks.Remove(stackToDelete);
        context.SaveChanges();

        return true;
    }

    public static bool UpdateStack(int displayId, string stackName)
    {
        using var context = new AppDbContext();

        var stacks = context.Stacks.OrderBy(s => s.Id).ToList();

        if (displayId < 1 || displayId > stacks.Count)
            return false;

        var stackToUpdate = stacks[displayId - 1];

        if (context.Stacks.Any(s => s.Name == stackName))
            return false;

        stackToUpdate.Name = stackName;

        context.SaveChanges();

        return true;
    }
}

using Flashcards.Niasua.Data;
using Flashcards.Niasua.DTOs;
using Flashcards.Niasua.Models;

namespace Flashcards.Niasua.Services;

public class StudySessionService
{
    public static bool CreateStudySession(string stackName, int score)
    {
        using var context = new AppDbContext();

        var stack = context.Stacks.FirstOrDefault(s => s.Name == stackName);

        if (stack == null)
            return false;

        var session = new StudySession
        {
            Date = DateTime.Now,
            Score = score,
            StackId = stack.Id
        };

        context.StudySessions.Add(session);
        context.SaveChanges();

        return true;
    }

    public static List<StudySessionDTO> GetAllSessions()
    {
        using var context = new AppDbContext();

        return context.StudySessions
            .OrderByDescending(s => s.Date)
            .Select(s => new StudySessionDTO
            {
                Date = s.Date,
                Score = s.Score,
                StackName = s.Stack.Name
            })
            .ToList();
    }
}

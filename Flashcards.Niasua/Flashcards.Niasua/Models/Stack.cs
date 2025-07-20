namespace Flashcards.Niasua.Models;

public class Stack
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Flashcard> Flashcards { get; set; } = new List<Flashcard>();
    public List<StudySession> StudySessions { get; set; }
}

﻿namespace Flashcards.Niasua.Models;

public class StudySession
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public int Score { get; set; }
    public int StackId { get; set; }
    public Stack Stack { get; set; }
}

# Flashcards Study App 

This is a C# console application that allows users to create stacks of
flashcards, review them, and track study sessions. It was developed as part
of The C# Academy.

## 🚀 Features

- 📁 Create, view, and delete stacks of flashcards
- 📝 Add, view, and delete flashcards within each stack
- 🔄 Flashcard IDs are renumbered from 1 with no gaps, even after deletions
- 🧠 Start a study session with a chosen stack:
  - Cards are shown one by one, first the question, then the answer
  - Users can mark a card as `Hit (h)` or `Miss (m)`
  - A score is calculated and saved based on the user's answers
- 🗓 View the history of all study sessions with date, stack name, and score

## 🧱 Database Structure

Uses SQL Server and Entity Framework Core with the following entities

- **Stack** stores a flashcard stack with a unique name
- **Flashcard** belongs to a stack and contains question and answer
- **StudySession** logs a study session with date, stack, and score

### Relationships

- One `Stack` has many `Flashcards`
- One `Stack` has many `StudySessions`
- When a stack is deleted, its flashcards and sessions are deleted (cascade)

## 🛠 Technologies

- **Language**: C#
- **Framework**: .NET 8 Console Application
- **Database**: SQL Server
- **ORM**: Entity Framework Core
- **Console UI**: Spectre.Console

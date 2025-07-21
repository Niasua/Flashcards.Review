# Flashcards Study App 📚

This is a C# console application that allows users to create stacks of flashcards, review them, and track study sessions. It was developed as part of The C# Academy.

## 🚀 Features

- 📁 Create, view, and delete **stacks** of flashcards.
- 📝 Add, view, and delete **flashcards** within each stack.
- 🔄 When displaying flashcards, IDs are renumbered starting from 1 with no gaps, even after deletions.
- 🧠 Start a **study session** with a chosen stack:
  - Cards are shown one by one, first the question, then the answer.
  - Users can mark a card as `Hit (h)` if answered correctly or `Miss (m)` if answered incorrectly.
  - At the end, a score is calculated and saved based on the user's responses.
- 🗓 View the history of all study sessions, showing the date, stack name, and score.

## 🧱 Database Structure

Uses **SQL Server** and **Entity Framework Core** with the following entities:

- `Stack`: represents a flashcard stack with a unique name.
- `Flashcard`: belongs to a stack, contains question and answer.
- `StudySession`: logs a study session with date, associated stack, and score.

Relationships:

- One `Stack` has many `Flashcards`.
- One `Stack` has many `StudySessions`.
- When a stack is deleted, all its flashcards and study sessions are also deleted (cascade delete).

## 🛠 Technologies

- Language: **C#**
- Framework: **.NET 8 Console Application**
- Database: **SQL Server**
- ORM: **Entity Framework Core**
- Console UI: **Spectre.Console**

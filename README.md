# 223003057_Dube_GradedTut2

## DSW03A1 - Development Software 3A | Graded Tutorial 2

A console-based Case Management System for the UJ Student Wellness Department, implemented using a **multi-tier architecture** in C#.

## Solution Structure

```
DSW03A1.sln
├── DSW03A1.Presentation       → Entry point, all Console I/O
│   └── Program.cs
├── DSW03A1.BusinessLogic      → Validation, rules, session management
│   ├── AuthService.cs
│   └── CaseService.cs
└── DSW03A1.DataAccess         → File reading and writing only
    └── FileHandler.cs
```

## Layer Dependency

```
Presentation → BusinessLogic → DataAccess
```

## Setup

1. Open `DSW03A1.sln` in Visual Studio
2. Place `users.txt` and `cases.txt` in `DSW03A1.Presentation/bin/Debug/net6.0/`
3. Set `DSW03A1.Presentation` as the startup project
4. Run the application

## Features

- Secure login with session management
- Role-based menus: **Student** and **Administrator**
- Students: view own cases, create new cases
- Admins: view all cases, close cases, delete cases (not if Closed)
- Robust flat-file parsing with graceful handling of malformed records
- All changes persisted back to `cases.txt`

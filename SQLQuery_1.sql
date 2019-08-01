CREATE TABLE Cohort (
    Id INTEGER NOT NULL PRIMARY KEY IDENTITY,
    Title VARCHAR(55) NOT NULL
);

CREATE TABLE Student (
    Id INTEGER NOT NULL PRIMARY KEY IDENTITY,
    FirstName VARCHAR(55) NOT NULL,
    LastName VARCHAR(55) NOT NULL,
    SlackHandle VARCHAR(55) NOT NULL,
    CohortId INTEGER NOT NULL
    CONSTRAINT Student_CohortId FOREIGN KEY(CohortId) REFERENCES Cohort(Id)
);

CREATE TABLE Instructor (
    Id INTEGER NOT NULL PRIMARY KEY IDENTITY,
    FirstName VARCHAR(55) NOT NULL,
    LastName VARCHAR(55) NOT NULL,
    SlackHandle VARCHAR(55) NOT NULL,
    CohortId INTEGER NOT NULL,
    Specialty VARCHAR(55) NOT NULL,
    CONSTRAINT Instructor_CohortId FOREIGN KEY(CohortId) REFERENCES Cohort(Id)
);

CREATE TABLE Exercise (
    Id INTEGER NOT NULL PRIMARY KEY IDENTITY,
    ExerciseName VARCHAR(55) NOT NULL,
    ExerciseLanguage VARCHAR(55) NOT NULL,
)

CREATE TABLE Assignment (
    Id INTEGER NOT NULL PRIMARY KEY IDENTITY,
    InstructorId INTEGER NOT NULL,
    StudentId INTEGER NOT NULL,
    ExerciseId INTEGER NOT NULL,
    CONSTRAINT Assignment_InstructorId FOREIGN KEY(InstructorId) REFERENCES Instructor(Id),
    CONSTRAINT Assignment_StudentId FOREIGN KEY(StudentId) REFERENCES Student(Id),
    CONSTRAINT Assignment_ExerciseId FOREIGN KEY(ExerciseId) REFERENCES Exercise(Id)
)
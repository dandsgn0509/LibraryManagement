/*=========================================================
    LIBRARY MANAGEMENT DATABASE
=========================================================*/

USE master;
GO

IF DB_ID('LibraryManagement') IS NOT NULL
BEGIN
    ALTER DATABASE LibraryManagement SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE LibraryManagement;
END
GO

CREATE DATABASE LibraryManagement;
GO

USE LibraryManagement;
GO

/*=========================================================
    MASTER TABLES
=========================================================*/

CREATE TABLE Library
(
    LibraryId INT IDENTITY(1,1) CONSTRAINT PK_Library PRIMARY KEY,
    LibraryName NVARCHAR(255) NOT NULL,
    Address NVARCHAR(500) NOT NULL,
    Phone VARCHAR(20) NULL,
    Email VARCHAR(255) NULL,
    CreatedDate DATETIME2 NOT NULL CONSTRAINT DF_Library_CreatedDate DEFAULT GETDATE(),
    UpdatedDate DATETIME2 NULL
);

CREATE TABLE Reader
(
    ReaderId INT IDENTITY(1,1) CONSTRAINT PK_Reader PRIMARY KEY,
    FullName NVARCHAR(255) NOT NULL,
    Birthday DATE NULL,
    Gender BIT NOT NULL,
    Phone VARCHAR(20) NULL,
    Email VARCHAR(255) NULL,
    Address NVARCHAR(500) NULL,
    Username VARCHAR(50) NOT NULL CONSTRAINT UQ_Reader_Username UNIQUE,
    Password NVARCHAR(255) NOT NULL,
    CreatedDate DATETIME2 NOT NULL CONSTRAINT DF_Reader_CreatedDate DEFAULT GETDATE(),
    UpdatedDate DATETIME2 NULL
);

CREATE TABLE Book
(
    BookId INT IDENTITY(1,1) CONSTRAINT PK_Book PRIMARY KEY,
    Title NVARCHAR(255) NOT NULL,
    Author NVARCHAR(255) NOT NULL,
    Publisher NVARCHAR(255) NOT NULL,
    PublishYear INT NOT NULL CONSTRAINT CK_Book_PublishYear CHECK (PublishYear > 0),
    Description NVARCHAR(MAX) NULL,
    Status TINYINT NOT NULL CONSTRAINT DF_Book_Status DEFAULT 1 CONSTRAINT CK_Book_Status CHECK (Status IN (0,1)),
    CreatedDate DATETIME2 NOT NULL CONSTRAINT DF_Book_CreatedDate DEFAULT GETDATE(),
    UpdatedDate DATETIME2 NULL
);

CREATE TABLE Newspaper
(
    NewspaperId INT IDENTITY(1,1) CONSTRAINT PK_Newspaper PRIMARY KEY,
    Title NVARCHAR(255) NOT NULL,
    Publisher NVARCHAR(255) NOT NULL,
    PublishDate DATE NOT NULL,
    Description NVARCHAR(MAX) NULL,
    Status TINYINT NOT NULL CONSTRAINT DF_Newspaper_Status DEFAULT 1 CONSTRAINT CK_Newspaper_Status CHECK (Status IN (0,1)),
    CreatedDate DATETIME2 NOT NULL CONSTRAINT DF_Newspaper_CreatedDate DEFAULT GETDATE(),
    UpdatedDate DATETIME2 NULL
);

CREATE TABLE Magazine
(
    MagazineId INT IDENTITY(1,1) CONSTRAINT PK_Magazine PRIMARY KEY,
    Title NVARCHAR(255) NOT NULL,
    IssueNumber INT NOT NULL CONSTRAINT CK_Magazine_IssueNumber CHECK (IssueNumber > 0),
    Publisher NVARCHAR(255) NOT NULL,
    PublishDate DATE NOT NULL,
    Description NVARCHAR(MAX) NULL,
    Status TINYINT NOT NULL CONSTRAINT DF_Magazine_Status DEFAULT 1 CONSTRAINT CK_Magazine_Status CHECK (Status IN (0,1)),
    CreatedDate DATETIME2 NOT NULL CONSTRAINT DF_Magazine_CreatedDate DEFAULT GETDATE(),
    UpdatedDate DATETIME2 NULL
);

/*=========================================================
    RELATION TABLES
=========================================================*/

CREATE TABLE Employee
(
    EmployeeId INT IDENTITY(1,1) CONSTRAINT PK_Employee PRIMARY KEY,
    LibraryId INT NOT NULL,
    FullName NVARCHAR(255) NOT NULL,
    Phone VARCHAR(20) NULL,
    Email VARCHAR(255) NULL,
    Username VARCHAR(50) NOT NULL CONSTRAINT UQ_Employee_Username UNIQUE,
    Password NVARCHAR(255) NOT NULL,
    Role TINYINT NOT NULL CONSTRAINT CK_Employee_Role CHECK (Role IN (0,1)),
    CreatedDate DATETIME2 NOT NULL CONSTRAINT DF_Employee_CreatedDate DEFAULT GETDATE(),
    UpdatedDate DATETIME2 NULL,
    CONSTRAINT FK_Employee_Library FOREIGN KEY (LibraryId) REFERENCES Library(LibraryId)
);

CREATE TABLE LibraryBook
(
    LibraryBookId INT IDENTITY(1,1) CONSTRAINT PK_LibraryBook PRIMARY KEY,
    LibraryId INT NOT NULL,
    BookId INT NOT NULL,
    Quantity INT NOT NULL CONSTRAINT CK_LibraryBook_Quantity CHECK (Quantity >= 0),
    CONSTRAINT UQ_LibraryBook UNIQUE (LibraryId, BookId),
    CONSTRAINT FK_LibraryBook_Library FOREIGN KEY (LibraryId) REFERENCES Library(LibraryId),
    CONSTRAINT FK_LibraryBook_Book FOREIGN KEY (BookId) REFERENCES Book(BookId)
);

CREATE TABLE LibraryNewspaper
(
    LibraryNewspaperId INT IDENTITY(1,1) CONSTRAINT PK_LibraryNewspaper PRIMARY KEY,
    LibraryId INT NOT NULL,
    NewspaperId INT NOT NULL,
    Quantity INT NOT NULL CONSTRAINT CK_LibraryNewspaper_Quantity CHECK (Quantity >= 0),
    CONSTRAINT UQ_LibraryNewspaper UNIQUE (LibraryId, NewspaperId),
    CONSTRAINT FK_LibraryNewspaper_Library FOREIGN KEY (LibraryId) REFERENCES Library(LibraryId),
    CONSTRAINT FK_LibraryNewspaper_Newspaper FOREIGN KEY (NewspaperId) REFERENCES Newspaper(NewspaperId)
);

CREATE TABLE LibraryMagazine
(
    LibraryMagazineId INT IDENTITY(1,1) CONSTRAINT PK_LibraryMagazine PRIMARY KEY,
    LibraryId INT NOT NULL,
    MagazineId INT NOT NULL,
    Quantity INT NOT NULL CONSTRAINT CK_LibraryMagazine_Quantity CHECK (Quantity >= 0),
    CONSTRAINT UQ_LibraryMagazine UNIQUE (LibraryId, MagazineId),
    CONSTRAINT FK_LibraryMagazine_Library FOREIGN KEY (LibraryId) REFERENCES Library(LibraryId),
    CONSTRAINT FK_LibraryMagazine_Magazine FOREIGN KEY (MagazineId) REFERENCES Magazine(MagazineId)
);

/*=========================================================
    BORROW TABLES
=========================================================*/

CREATE TABLE BorrowTicket
(
    BorrowTicketId INT IDENTITY(1,1) CONSTRAINT PK_BorrowTicket PRIMARY KEY,
    LibraryId INT NOT NULL,
    ReaderId INT NOT NULL,
    EmployeeId INT NOT NULL,
    BorrowDate DATETIME2 NOT NULL,
    DueDate DATETIME2 NOT NULL,
    Status TINYINT NOT NULL CONSTRAINT DF_BorrowTicket_Status DEFAULT 0 CONSTRAINT CK_BorrowTicket_Status CHECK (Status IN (0,1,2)),
    CreatedDate DATETIME2 NOT NULL CONSTRAINT DF_BorrowTicket_CreatedDate DEFAULT GETDATE(),
    UpdatedDate DATETIME2 NULL,
    CONSTRAINT FK_BorrowTicket_Library FOREIGN KEY (LibraryId) REFERENCES Library(LibraryId),
    CONSTRAINT FK_BorrowTicket_Reader FOREIGN KEY (ReaderId) REFERENCES Reader(ReaderId),
    CONSTRAINT FK_BorrowTicket_Employee FOREIGN KEY (EmployeeId) REFERENCES Employee(EmployeeId)
);

CREATE TABLE BorrowBook
(
    BorrowBookId INT IDENTITY(1,1) CONSTRAINT PK_BorrowBook PRIMARY KEY,
    BorrowTicketId INT NOT NULL,
    BookId INT NOT NULL,
    Quantity INT NOT NULL CONSTRAINT CK_BorrowBook_Quantity CHECK (Quantity > 0),
    ReturnDate DATETIME2 NULL,
    Status TINYINT NOT NULL CONSTRAINT DF_BorrowBook_Status DEFAULT 0 CONSTRAINT CK_BorrowBook_Status CHECK (Status IN (0,1)),
    CONSTRAINT UQ_BorrowBook UNIQUE (BorrowTicketId, BookId),
    CONSTRAINT FK_BorrowBook_BorrowTicket FOREIGN KEY (BorrowTicketId) REFERENCES BorrowTicket(BorrowTicketId),
    CONSTRAINT FK_BorrowBook_Book FOREIGN KEY (BookId) REFERENCES Book(BookId)
);

CREATE TABLE BorrowNewspaper
(
    BorrowNewspaperId INT IDENTITY(1,1) CONSTRAINT PK_BorrowNewspaper PRIMARY KEY,
    BorrowTicketId INT NOT NULL,
    NewspaperId INT NOT NULL,
    Quantity INT NOT NULL CONSTRAINT CK_BorrowNewspaper_Quantity CHECK (Quantity > 0),
    ReturnDate DATETIME2 NULL,
    Status TINYINT NOT NULL CONSTRAINT DF_BorrowNewspaper_Status DEFAULT 0 CONSTRAINT CK_BorrowNewspaper_Status CHECK (Status IN (0,1)),
    CONSTRAINT UQ_BorrowNewspaper UNIQUE (BorrowTicketId, NewspaperId),
    CONSTRAINT FK_BorrowNewspaper_BorrowTicket FOREIGN KEY (BorrowTicketId) REFERENCES BorrowTicket(BorrowTicketId),
    CONSTRAINT FK_BorrowNewspaper_Newspaper FOREIGN KEY (NewspaperId) REFERENCES Newspaper(NewspaperId)
);

    CREATE TABLE BorrowMagazine
    (
        BorrowMagazineId INT IDENTITY(1,1) CONSTRAINT PK_BorrowMagazine PRIMARY KEY,
        BorrowTicketId INT NOT NULL,
        MagazineId INT NOT NULL,
        Quantity INT NOT NULL CONSTRAINT CK_BorrowMagazine_Quantity CHECK (Quantity > 0),
        ReturnDate DATETIME2 NULL,
        Status TINYINT NOT NULL CONSTRAINT DF_BorrowMagazine_Status DEFAULT 0 CONSTRAINT CK_BorrowMagazine_Status CHECK (Status IN (0,1)),
        CONSTRAINT UQ_BorrowMagazine UNIQUE (BorrowTicketId, MagazineId),
        CONSTRAINT FK_BorrowMagazine_BorrowTicket FOREIGN KEY (BorrowTicketId) REFERENCES BorrowTicket(BorrowTicketId),
        CONSTRAINT FK_BorrowMagazine_Magazine FOREIGN KEY (MagazineId) REFERENCES Magazine(MagazineId)
    );

    /*=========================================================
    SAMPLE DATA
=========================================================*/

INSERT INTO Library (LibraryName, Address, Phone, Email)
VALUES
(N'Thư viện Hà Nội', N'Hà Nội', '0241111111', 'hanoi@library.com'),
(N'Thư viện Hồ Chí Minh', N'Hồ Chí Minh', '0282222222', 'hcm@library.com');

INSERT INTO Reader (FullName, Birthday, Gender, Phone, Email, Address, Username, Password)
VALUES
(N'Nguyễn Văn A', '2002-05-10', 1, '0901111111', 'a@gmail.com', N'Hà Nội', 'reader01', '123456'),
(N'Trần Thị B', '2003-08-20', 0, '0902222222', 'b@gmail.com', N'Hồ Chí Minh', 'reader02', '123456');

INSERT INTO Book (Title, Author, Publisher, PublishYear, Description, Status)
VALUES
(N'Lập trình C#', N'Nguyễn Văn C', N'NXB Giáo Dục', 2022, N'Sách học C#', 1),
(N'ASP.NET Core MVC', N'Trần Văn D', N'NXB Khoa Học', 2023, N'Sách ASP.NET Core', 1);

INSERT INTO Newspaper (Title, Publisher, PublishDate, Description, Status)
VALUES
(N'Tuổi Trẻ', N'Tuổi Trẻ', '2026-06-01', N'Báo Tuổi Trẻ', 1),
(N'Thanh Niên', N'Thanh Niên', '2026-06-02', N'Báo Thanh Niên', 1);

INSERT INTO Magazine (Title, IssueNumber, Publisher, PublishDate, Description, Status)
VALUES
(N'Tạp chí CNTT', 1, N'NXB CNTT', '2026-06-01', N'Số tháng 6', 1),
(N'Tạp chí Khoa học', 5, N'NXB KH', '2026-06-10', N'Số đặc biệt', 1);

INSERT INTO Employee (LibraryId, FullName, Phone, Email, Username, Password, Role)
VALUES
(1, N'Admin Hà Nội', '0903333333', 'adminhn@gmail.com', 'admin', '123456', 0),
(2, N'Thủ thư HCM', '0904444444', 'librarian@gmail.com', 'staff', '123456', 1);

INSERT INTO LibraryBook (LibraryId, BookId, Quantity)
VALUES
(1, 1, 20),
(1, 2, 15),
(2, 1, 10);

INSERT INTO LibraryNewspaper (LibraryId, NewspaperId, Quantity)
VALUES
(1, 1, 30),
(2, 2, 25);

INSERT INTO LibraryMagazine (LibraryId, MagazineId, Quantity)
VALUES
(1, 1, 12),
(2, 2, 8);

INSERT INTO BorrowTicket (LibraryId, ReaderId, EmployeeId, BorrowDate, DueDate, Status)
VALUES
(1, 1, 1, GETDATE(), DATEADD(DAY, 7, GETDATE()), 0);

INSERT INTO BorrowBook (BorrowTicketId, BookId, Quantity, ReturnDate, Status)
VALUES
(1, 1, 1, NULL, 0);

INSERT INTO BorrowNewspaper (BorrowTicketId, NewspaperId, Quantity, ReturnDate, Status)
VALUES
(1, 1, 1, NULL, 0);

INSERT INTO BorrowMagazine (BorrowTicketId, MagazineId, Quantity, ReturnDate, Status)
VALUES
(1, 1, 1, NULL, 0);
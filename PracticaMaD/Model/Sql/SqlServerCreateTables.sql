
USE practicamad

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[SpecificProperty]') 
AND type in ('U')) DROP TABLE [SpecificProperty]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[CommentTag]') 
AND type in ('U')) DROP TABLE [CommentTag]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[OrderLine]') 
AND type in ('U')) DROP TABLE [OrderLine]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[Order]') 
AND type in ('U')) DROP TABLE [Order]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[Comment]') 
AND type in ('U')) DROP TABLE [Comment]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[Tag]') 
AND type in ('U')) DROP TABLE [Tag]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[BankCard]') 
AND type in ('U')) DROP TABLE [BankCard]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[User]') 
AND type in ('U')) DROP TABLE [User]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[Product]') 
AND type in ('U')) DROP TABLE [Product]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[Garage]') 
AND type in ('U')) DROP TABLE [Garage]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[Category]') 
AND type in ('U')) DROP TABLE [Category]
GO

CREATE TABLE Category (
    categoryId BIGINT IDENTITY(1,1) PRIMARY KEY,
    name VARCHAR(50) NOT NULL,
    parentId BIGINT FOREIGN KEY REFERENCES Category(categoryId)
);

CREATE TABLE Product (
    productId BIGINT IDENTITY(1,1) PRIMARY KEY,
    name VARCHAR(50) NOT NULL,
    price FLOAT NOT NULL,
    addingDate DATETIME2 NOT NULL,
    stock INT NOT NULL,
	image VARCHAR(255),
	description varchar(4000),
    categoryId BIGINT FOREIGN KEY REFERENCES Category(categoryId)
);

CREATE TABLE SpecificProperty (
    name VARCHAR(50) NOT NULL,
    value VARCHAR(50) NOT NULL,
    productId BIGINT FOREIGN KEY REFERENCES Product(productId) ON DELETE CASCADE
    PRIMARY KEY(productId, value)
);

CREATE TABLE Garage (
    garageId BIGINT IDENTITY(1,1) PRIMARY KEY,
    name VARCHAR(50) NOT NULL,
);

CREATE TABLE [User] (
    userId BIGINT IDENTITY(1,1) PRIMARY KEY,
    login VARCHAR(50) NOT NULL,
    password VARCHAR(50) NOT NULL,
    name VARCHAR(50) NOT NULL,
    surnames VARCHAR(100) NOT NULL,
    address VARCHAR(100) NOT NULL,
    email VARCHAR(50) NOT NULL,
	language VARCHAR(255),
	country VARCHAR(255),
    isAdmin BIT NOT NULL,
    garageId BIGINT FOREIGN KEY REFERENCES Garage(garageId)
);

CREATE TABLE BankCard (
    cardId BIGINT IDENTITY(1,1) PRIMARY KEY,
    type TINYINT NOT NULL,
    number VARCHAR(16) NOT NULL,
    cvv VARCHAR(3) NOT NULL,
    expirationDate DATETIME2 NOT NULL,
    isDefault BIT NOT NULL,
    isActive BIT NOT NULL,
    userId BIGINT NOT NULL FOREIGN KEY REFERENCES [User](userId) ON DELETE CASCADE
);

CREATE TABLE [Order] (
    orderId BIGINT IDENTITY(1,1) PRIMARY KEY,
    purchasingDate DATETIME2 NOT NULL,
    name VARCHAR(50) NOT NULL,
    address VARCHAR(100) NOT NULL,
    totalPrice DECIMAL(10,2) NOT NULL,
    expressShipping BIT NOT NULL,
    cardId BIGINT NOT NULL FOREIGN KEY REFERENCES BankCard(cardId),
    userId BIGINT NOT NULL FOREIGN KEY REFERENCES [User](userId) ON DELETE CASCADE
);

CREATE NONCLUSTERED INDEX [IX_OrderIndexByPurchasingDate] 
ON [Order] (orderId, purchasingDate);

CREATE TABLE OrderLine (
    units INT NOT NULL,
    price DECIMAL(10,2) NOT NULL,
    orderId BIGINT FOREIGN KEY REFERENCES [Order](orderId) ON DELETE CASCADE,
    productId BIGINT FOREIGN KEY REFERENCES Product(productId),
    PRIMARY KEY(orderId, productId)
);

CREATE TABLE Comment (
  commentId BIGINT IDENTITY(1,1) PRIMARY KEY,
  productId BIGINT NOT NULL,
  userId BIGINT NOT NULL,
  date DATETIME2,
  message VARCHAR(4000),
  FOREIGN KEY (productId) REFERENCES Product(productId) ON DELETE CASCADE,
  FOREIGN KEY (userId) REFERENCES [User] (userId)
);

CREATE NONCLUSTERED INDEX [IX_CommentIndexByDate] 
ON Comment (commentId, date);

CREATE TABLE Tag (
  tagId BIGINT IDENTITY(1,1) PRIMARY KEY,
  name VARCHAR(255)
);

CREATE TABLE CommentTag (
  commentId BIGINT,
  tagId BIGINT,
  FOREIGN KEY (commentId) REFERENCES Comment(commentId),
  FOREIGN KEY (tagId) REFERENCES Tag(tagId),
  PRIMARY KEY (tagId, commentId)
);

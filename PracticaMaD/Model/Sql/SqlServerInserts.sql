USE practicamad;

-- Garage 1
INSERT INTO Garage (name)
VALUES ('Main Garage');

-- Garage 2
INSERT INTO Garage (name)
VALUES ('Second Garage');

-- Garage 3
INSERT INTO Garage (name)
VALUES ('Home Garage');


-- User 1
INSERT INTO [User] (login, password, name, surnames, address, email, language, country, isAdmin, garageId)
VALUES ('userTest1', 'password1', 'John', 'Doe', '123 Main St', 'john.doe@email.com', 'English', 'United States', 1,  1);

-- User 2
INSERT INTO [User] (login, password, name, surnames, address, email, language, country, isAdmin, garageId)
VALUES ('userTest2', 'password2', 'Alice', 'Smith', '456 Elm St', 'alice.smith@email.com', 'English', 'Canada', 0, 2);

-- User 3
INSERT INTO [User] (login, password, name, surnames, address, email, language, country, isAdmin, garageId)
VALUES ('userTest3', 'password3', 'Michael', 'Johnson', '789 Oak St', 'michael.johnson@email.com', 'English', 'Australia', 0, 1);

-- Bank cards
DECLARE @userId1 INT = (SELECT userId FROM [User] WHERE login = 'userTest1');
DECLARE @userId2 INT = (SELECT userId FROM [User] WHERE login = 'userTest2');

INSERT INTO BankCard (type, number, cvv, expirationDate, isDefault, isActive, userId) VALUES 
(1, '1111222233334444', '123', DATEADD(year, 3, GETDATE()), 1, 1, @userId1),
(1, '1111222233335555', '321', DATEADD(year, 3, GETDATE()), 1, 1, @userId1),
(2, '5555666677778888', '456', DATEADD(year, 3, GETDATE()), 1, 1, @userId2);

-- Categories
INSERT INTO Category (name)
VALUES 
('Aceites y lubricantes'),
('Filtros'),
('Baterías'),
('Frenos'),
('Neumáticos'),
('Refrigerantes');

-- Wrench Set
INSERT INTO Product (name, price, addingDate, stock, image, description, categoryId)
VALUES ('Wrench Set', 49.99, '2024-03-24', 20, '/Images/wrench_set.jpg', 'A set of high-quality wrenches for various sizes.', 1);

INSERT INTO SpecificProperty (name, value, productId)
VALUES ('Size', '10-19mm', SCOPE_IDENTITY()),
       ('Material', 'Chrome Vanadium Steel', SCOPE_IDENTITY());

INSERT INTO Comment (productId, userId, date, message)
VALUES (1, 1, '2024-03-20', 'Excellent wrench set! Sturdy and reliable.');

INSERT INTO Tag (name) VALUES ('Wrench'), ('Set');

INSERT INTO CommentTag (commentId, tagId)
VALUES (1, 1);

-- Power Drill
INSERT INTO Product (name, price, addingDate, stock, image, description, categoryId)
VALUES ('Power Drill', 79.99, GETDATE(), 15, '/Images/power_drill.jpg', 'Versatile power drill for various DIY projects.', 1);

INSERT INTO SpecificProperty (name, value, productId)
VALUES ('Voltage', '18V', SCOPE_IDENTITY()),
       ('Corded/Cordless', 'Cordless', SCOPE_IDENTITY());

INSERT INTO Comment (productId, userId, date, message)
VALUES (2, 2, '2024-03-21', 'Great drill! Plenty of power and easy to handle.');

INSERT INTO Tag (name) VALUES ('Drill'), ('Power');

INSERT INTO CommentTag (commentId, tagId)
VALUES (2, 2);

-- Car Wax
INSERT INTO Product (name, price, addingDate, stock, image, description, categoryId)
VALUES ('Car Wax', 14.99, GETDATE(), 50, '/Images/car_wax.jpg', 'Protective wax for a shiny and clean car finish.', 2);

-- Car Wax Ultimate
INSERT INTO Product (name, price, addingDate, stock, image, description, categoryId)
VALUES ('Car Wax Ultimate', 24.99, GETDATE(), 50, '/Images/car_wax_ultimate.jpg', 'Protective wax for a shiny and clean car finish.', 2);

INSERT INTO SpecificProperty (name, value, productId)
VALUES ('Volume', '16 oz', SCOPE_IDENTITY()),
       ('Type', 'Liquid', SCOPE_IDENTITY());

INSERT INTO Comment (productId, userId, date, message)
VALUES (3, 3, '2024-03-22', 'Amazing wax! Gives a deep shine and easy to apply.');

INSERT INTO Tag (name) VALUES ('Car Care'), ('Wax');

INSERT INTO CommentTag (commentId, tagId)
VALUES (3, 3);

-- Floor Mats
INSERT INTO Product (name, price, addingDate, stock, image, description, categoryId)
VALUES ('Floor Mats', 29.99, GETDATE(), 40, '/Images/floor_mats.jpg', 'Durable floor mats to protect your car interior.', 2);

INSERT INTO Product (name, price, addingDate, stock, image, description, categoryId)
VALUES ('Spark Plug', 9.99, GETDATE(), 150, '/Images/Spark_plug_2.jpg', 'High-performance spark plugs for efficient combustion.', 3);

INSERT INTO SpecificProperty (name, value, productId)
VALUES ('Material', 'Rubber', SCOPE_IDENTITY()),
       ('Color', 'Black', SCOPE_IDENTITY());

INSERT INTO Comment (productId, userId, date, message)
VALUES (4, 1, '2024-03-23', 'Love these mats! They fit perfectly and easy to clean.');

INSERT INTO Tag (name) VALUES ('Interior'), ('Protection');

INSERT INTO CommentTag (commentId, tagId)
VALUES (4, 4);

-- Jumper Cables
INSERT INTO Product (name, price, addingDate, stock, image, description, categoryId)
VALUES ('Jumper Cables', 24.99, GETDATE(), 25, '/Images/jumper_cables.jpg', 'Essential for jump-starting your car in emergencies.', 1);

INSERT INTO SpecificProperty (name, value, productId)
VALUES ('Length', '12 feet', SCOPE_IDENTITY()),
       ('Gauge', '8 Gauge', SCOPE_IDENTITY());

INSERT INTO Comment (productId, userId, date, message)
VALUES (5, 2, '2024-03-24', 'Saved me in a pinch! Good quality and easy to use.');

INSERT INTO Tag (name) VALUES ('Emergency'), ('Battery');

INSERT INTO CommentTag (commentId, tagId)
VALUES (5, 5);

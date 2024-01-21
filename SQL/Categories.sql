SELECT CategoryID, CategoryName, Description, Picture FROM Categories;

INSERT INTO Categories (CategoryName, Description, Picture) VALUES ('NewCategory', 'New Description', 0x0102030405);

SELECT CategoryID, CategoryName, Description, Picture FROM Categories Where CategoryID = 10;

UPDATE Categories SET CategoryName = 'UpdatedName', Description = 'Updated Description', Picture = 0x0504030201 WHERE CategoryID = 10;

DELETE FROM Categories WHERE CategoryID = 10;


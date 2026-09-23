/*
    Analytical stored procedures used by the Sales Dashboard.
    Database context: Sales

    These procedures expose the datasets consumed by the ASP.NET Core
    visualization layer.
*/

CREATE OR ALTER PROCEDURE GetTotalSalesPerMonth
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        FORMAT(InvoiceDate, 'yyyy-MM') AS Month,
        SUM(ExtendedPrice) AS TotalSales
    FROM Sales
    GROUP BY FORMAT(InvoiceDate, 'yyyy-MM')
    ORDER BY Month;
END;
GO

CREATE OR ALTER PROCEDURE GetTotalSalesQuantityPerMonth
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        FORMAT(InvoiceDate, 'yyyy-MM') AS Month,
        SUM(Quantity) AS TotalSalesQuantity
    FROM Sales
    GROUP BY FORMAT(InvoiceDate, 'yyyy-MM')
    ORDER BY Month;
END;
GO

CREATE OR ALTER PROCEDURE GetTop5CustomersBySales
AS
BEGIN
    SET NOCOUNT ON;

    SELECT TOP 5
        C.CustomerName,
        SUM(S.ExtendedPrice) AS TotalSales
    FROM Sales AS S
    INNER JOIN Customers AS C ON S.CustomerID = C.CustomerID
    GROUP BY C.CustomerName
    ORDER BY TotalSales DESC;
END;
GO

CREATE OR ALTER PROCEDURE GetTop5ProductsByQuantitySold
AS
BEGIN
    SET NOCOUNT ON;

    SELECT TOP 5
        P.StockItemName,
        SUM(S.Quantity) AS TotalQuantitySold
    FROM Sales AS S
    INNER JOIN StockItems AS P ON S.StockItemID = P.StockItemID
    GROUP BY P.StockItemName
    ORDER BY TotalQuantitySold DESC;
END;
GO

CREATE OR ALTER PROCEDURE GetSalesByCountry
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        CO.CountryName,
        SUM(S.ExtendedPrice) AS TotalSalesRevenue
    FROM Sales AS S
    INNER JOIN Customers AS C ON S.CustomerID = C.CustomerID
    INNER JOIN Cities AS CI ON C.DeliveryCityID = CI.CityID
    INNER JOIN StateProvinces AS SP ON CI.StateProvinceID = SP.StateProvinceID
    INNER JOIN Countries AS CO ON SP.CountryID = CO.CountryID
    GROUP BY CO.CountryName
    ORDER BY TotalSalesRevenue DESC;
END;
GO

CREATE OR ALTER PROCEDURE GetSalesBySalesperson
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        P.FullName AS SalespersonName,
        SUM(S.ExtendedPrice) AS TotalSalesRevenue
    FROM Sales AS S
    INNER JOIN People AS P ON S.SalespersonPersonID = P.PersonID
    GROUP BY P.FullName
    ORDER BY TotalSalesRevenue DESC;
END;
GO

CREATE OR ALTER PROCEDURE GetSalesByProductCategory
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        SG.StockGroupName AS ProductCategory,
        SUM(S.ExtendedPrice) AS TotalSalesRevenue
    FROM Sales AS S
    INNER JOIN StockItemStockGroups AS SISG ON S.StockItemID = SISG.StockItemID
    INNER JOIN StockGroups AS SG ON SISG.StockGroupID = SG.StockGroupID
    GROUP BY SG.StockGroupName
    ORDER BY TotalSalesRevenue DESC;
END;
GO

/* Supporting indexes used by the analytical joins. */

IF NOT EXISTS (
    SELECT 1
    FROM sys.indexes
    WHERE name = 'IX_Sales_CustomerID'
      AND object_id = OBJECT_ID('Sales')
)
BEGIN
    CREATE NONCLUSTERED INDEX IX_Sales_CustomerID
        ON Sales (CustomerID);
END;
GO

IF NOT EXISTS (
    SELECT 1
    FROM sys.indexes
    WHERE name = 'IX_Customers_DeliveryCityID'
      AND object_id = OBJECT_ID('Customers')
)
BEGIN
    CREATE NONCLUSTERED INDEX IX_Customers_DeliveryCityID
        ON Customers (DeliveryCityID);
END;
GO

IF NOT EXISTS (
    SELECT 1
    FROM sys.indexes
    WHERE name = 'IX_Cities_StateProvinceID'
      AND object_id = OBJECT_ID('Cities')
)
BEGIN
    CREATE NONCLUSTERED INDEX IX_Cities_StateProvinceID
        ON Cities (StateProvinceID);
END;
GO

IF NOT EXISTS (
    SELECT 1
    FROM sys.indexes
    WHERE name = 'IX_StateProvinces_CountryID'
      AND object_id = OBJECT_ID('StateProvinces')
)
BEGIN
    CREATE NONCLUSTERED INDEX IX_StateProvinces_CountryID
        ON StateProvinces (CountryID);
END;
GO

IF NOT EXISTS (
    SELECT 1
    FROM sys.indexes
    WHERE name = 'IX_Countries_CountryID'
      AND object_id = OBJECT_ID('Countries')
)
BEGIN
    CREATE NONCLUSTERED INDEX IX_Countries_CountryID
        ON Countries (CountryID);
END;
GO

/* Example execution. Run only after the procedures have been created. */
EXEC GetSalesByCountry;
GO

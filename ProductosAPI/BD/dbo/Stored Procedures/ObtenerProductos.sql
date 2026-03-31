CREATE PROCEDURE ObtenerProductos
	-- Add the parameters for the stored procedure here
AS
BEGIN
	
SELECT [Id]
      ,[IdSubCategoria]
      ,[Nombre]
      ,[Descripcion]
      ,[Precio]
      ,[Stock]
      ,[CodigoBarras]
  FROM [dbo].[Producto]
END
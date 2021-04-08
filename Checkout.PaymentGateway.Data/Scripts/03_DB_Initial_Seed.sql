USE [CheckoutDB]

GO

INSERT INTO [dbo].[Merchant]
           ([MerchantRef]
           ,[Name]
           ,[IsEnabled])
     VALUES
           (NEWID(),'Merchant-01',1),
		   (NEWID(),'Merchant-02',1),
		   (NEWID(),'Merchant-03',1)
GO

INSERT INTO [dbo].[Currency]
           ([Name]
           ,[Code]
           ,[Description]
           ,[IsEnabled]
           ,[Symbol])
     VALUES
           ('British Pound','GBP','',1,'£'),
		   ('Euro','EUR','',1,'€'),
		   ('US Dollar','USD','',1,'$')
GO

INSERT INTO [dbo].[MerchantAPIKey]
           ([MerchantID]
           ,[APIKey]
           ,[IsEnabled])
     VALUES
           (1,'57Dw2tFq9wF6' ,1),
		   (1,'mdAYungKPbmW' ,2),
		   (1,'ArqTZ3SLlcd3' ,3)
GO

INSERT INTO [dbo].[TransactionStatus]
           ([StatusName]
           ,[Description])
     VALUES
           ('Pending', 'Pending Transaction'),
		   ('Completed', 'Completed Transaction'),
		   ('Cancelled', 'Pendong Transaction'),
		   ('Rejected', 'Rejected Transaction'),
		   ('Reversed', 'Reversed Transaction')
GO







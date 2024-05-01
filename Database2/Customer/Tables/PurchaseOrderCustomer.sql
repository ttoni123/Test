CREATE TABLE [Customer].[PurchaseOrderCustomer] (
    [PurchaseOrderCustomerId] INT            IDENTITY (1, 1) NOT NULL,
    [FirstName]               VARCHAR (25)   NULL,
    [LastName]                VARCHAR (25)   NOT NULL,
    [OIB]                     VARCHAR (25)   NOT NULL,
    [Created]                 DATETIME       CONSTRAINT [DF_PurchaseOrderCustomer_Created] DEFAULT (getdate()) NULL,
    [CreatedBy]               NVARCHAR (50)  CONSTRAINT [DF_PurchaseOrderCustomer_CreatedBy] DEFAULT (user_name()) NULL,
    [LastUpd]                 DATETIME       CONSTRAINT [DF_PurchaseOrderCustomer_LastUpd] DEFAULT (getdate()) NULL,
    [LastUpdBy]               NVARCHAR (50)  CONSTRAINT [DF_PurchaseOrderCustomer_LastUpdBy] DEFAULT (user_name()) NULL,
    [LastUpdApp]              NVARCHAR (512) CONSTRAINT [DF_PurchaseOrderCustomer_LastUpdApp] DEFAULT (app_name()) NULL,
    [PurchaseOrderId]         INT            NOT NULL,
    CONSTRAINT [PK_PurchaseOrderCustomerId] PRIMARY KEY CLUSTERED ([PurchaseOrderCustomerId] ASC),
    CONSTRAINT [FK_OrderCustomer] FOREIGN KEY ([PurchaseOrderId]) REFERENCES [Customer].[PurchaseOrder] ([PurchaseOrderId])
);


CREATE TABLE [Customer].[PurchaseOrder] (
    [PurchaseOrderId]              INT            IDENTITY (1, 1) NOT NULL,
    [PurchaseOrderReferenceNumber] INT            NOT NULL,
    [CustomerId]                   INT            NOT NULL,
    [Created]                      DATETIME       CONSTRAINT [DF_PurchaseOrder_Created] DEFAULT (getdate()) NULL,
    [CreatedBy]                    NVARCHAR (50)  CONSTRAINT [DF_PurchaseOrder_CreatedBy] DEFAULT (user_name()) NULL,
    [LastUpd]                      DATETIME       CONSTRAINT [DF_PurchaseOrder_LastUpd] DEFAULT (getdate()) NULL,
    [LastUpdBy]                    NVARCHAR (50)  CONSTRAINT [DF_PurchaseOrder_LastUpdBy] DEFAULT (user_name()) NULL,
    [LastUpdApp]                   NVARCHAR (512) CONSTRAINT [DF_PurchaseOrder_LastUpdApp] DEFAULT (app_name()) NULL,
    [PurchaseOrderDate]            DATETIME       NULL,
    CONSTRAINT [PK_PurchaseOrderId] PRIMARY KEY CLUSTERED ([PurchaseOrderId] ASC)
);


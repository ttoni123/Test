CREATE TABLE [Customer].[PurchaseOrderItem] (
    [PurchaseOrderItemId] INT            IDENTITY (1, 1) NOT NULL,
    [ProductName]         VARCHAR (25)   NULL,
    [Quantity]            INT            NOT NULL,
    [Price]               MONEY          NOT NULL,
    [Created]             DATETIME       CONSTRAINT [DF_PurchaseOrderItem_Created] DEFAULT (getdate()) NULL,
    [CreatedBy]           NVARCHAR (50)  CONSTRAINT [DF_PurchaseOrderItem_CreatedBy] DEFAULT (user_name()) NULL,
    [LastUpd]             DATETIME       CONSTRAINT [DF_PurchaseOrderItem_LastUpd] DEFAULT (getdate()) NULL,
    [LastUpdBy]           NVARCHAR (50)  CONSTRAINT [DF_PurchaseOrderItem_LastUpdBy] DEFAULT (user_name()) NULL,
    [LastUpdApp]          NVARCHAR (512) CONSTRAINT [DF_PurchaseOrderItem_LastUpdApp] DEFAULT (app_name()) NULL,
    [PurchaseOrderId]     INT            NOT NULL,
    CONSTRAINT [PK_PurchaseOrderItemId] PRIMARY KEY CLUSTERED ([PurchaseOrderItemId] ASC)
);


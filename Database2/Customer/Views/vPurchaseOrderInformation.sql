

CREATE VIEW [Customer].[vPurchaseOrderInformation] 
AS

	SELECT 
	PurchaseOrder.PurchaseOrderId,
	PurchaseOrder.PurchaseOrderReferenceNumber,
	PurchaseOrder.PurchaseOrderDate,
	FirstName,
	LastName,
	OIB
FROM 
	Customer.PurchaseOrder
	INNER JOIN
Customer.PurchaseOrderCustomer
	ON
	PurchaseOrderCustomer.PurchaseOrderId = PurchaseOrder.PurchaseOrderId

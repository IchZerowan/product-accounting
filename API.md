# Product Accounting API Reference

## Product
Represents a single product in the database

### JSON Schema

```json
{
    "id": 2,
    "name": "Sugar",
    "description": "A must-have component for any meal",
    "priceRetail": 5.0,
    "priceWholesale": 3.0,
    "count": 10,
    "archived": false
}
```

* GET /products - list all unarchived products
* GET /products/archived - list all archived products
* GET /products/{id} - get product details by id
* POST /products - add new product
* PUT /products/{id} - update existing product
* DELETE /products/{id} - archive product by id

## Supplier
Represents a single delivery source in the database

### JSON Schema

```json
{
    "id": 1,
    "name": "Bakery A ltd.",
    "phone": "+380999999999",
    "archived": false
}
```

* GET /suppliers - list all unarchived suppliers
* GET /suppliers/archived - list all archived suppliers
* GET /suppliers/{id} - get supplier details by id
* POST /suppliers - add new supplier
* PUT /suppliers/{id} - update existing supplier
* DELETE /suppliers/{id} - archive supplier by id

## Delivery
Represents a single delivery with different amounts of different products in the 
database

### JSON Schema

```json
{
    "id": 1,
    "date": "2017-11-15",
    "shippingCost": 2.9,
    "supplier": { ... },
    "products": [
        {
            "product": { ... },
            "count": 20,
            "price": 120.0 // auto-calculated
        }
    ],
    "total": 122.9, // auto-calculated
    "completed": false
}
```

* GET /deliveries - list all deliveries
* GET /deliveries/{id} - get delivery details by id
* POST /deliveries - add new delivery

### JSON Schema
```json
{
    "supplierId": 1,
    "date": "2021-11-15",
    "shippingCost": 12.3
}
```

* PUT /deliveries/{id} - update existing delivery
* PUT /deliveries/{id}/commit - commit the delivery. During this action the 
product count is updated and the Transaction entity is created
* DELETE /deliveries/{id} - delete delivery by id
* POST /deliveries/{deliveryId}/products - add new product item to the delivery

### JSON Schema
```json
{
    "productId": 3,
    "count": 4
}
```

* PUT /deliveries/{deliveryId}/products/{productId} - modifies the product item
in the specified delivery
* DELETE /deliveries/{deliveryId}/products/{productId} - deletes the product 
item in the specified delivery

## Purchase
Represents a single purchase with different amounts of different products in the 
database

### JSON Schema

```json
{
    "id": 1,
    "date": "2017-11-15",
    "products": [
        {
            "product": { ... },
            "count": 20,
            "price": 120.0 // auto-calculated
        }
    ],
    "total": 120.0, // auto-calculated
    "completed": false
}
```

* GET /purchases - list all purchases
* GET /purchases/{id} - get purchase details by id
* POST /purchases - add new purchase

### JSON Schema
```json
{
    "supplierId": 1,
    "date": "2021-11-15",
    "shippingCost": 12.3
}
```

* PUT /purchases/{id} - update existing purchase
* PUT /purchases/{id}/commit - commit the purchase. During this action the 
product count is updated and the Transaction entity is created
* DELETE /purchases/{id} - delete purchase by id
* POST /purchases/{purchaseId}/products - add new product item to the purchase

### JSON Schema
```json
{
    "productId": 3,
    "count": 4
}
```

* PUT /purchases/{purchaseId}/products/{productId} - modifies the product item
in the specified purchase
* DELETE /purchases/{purchaseId}/products/{productId} - deletes the product 
item in the specified purchase

## Transaction

Represents a single committed financial transaction. Used mostly for logging and
statistics

### JSON Schema
```json
{
    "type": "DELIVERY", // or "PURCHASE"
    "relId": 1, // corresponding delivery or purchase id
    "amount": -12.1,
    "date": "2021-12-11"
}
```

* GET /transactions - list all transactions
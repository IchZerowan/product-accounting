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
[
    {
        "id": 1,
        "date": "2017-11-15",
        "supplier": { ... },
        "products": [
            {
                "product": { ... },
                "count": 20
            }
        ]
    }
]
```

* GET /deliveries - list all deliveries
* GET /deliveries/{id} - get delivery details by id
* POST /deliveries - add new delivery

### JSON Schema
```json
{
    "supplierId": 1,
    "date": "2021-11-15"
}
```

* PUT /deliveries/{id} - update existing delivery
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
# Product Accounting API Reference

## Product
Represents a single product in the database

### JSON Schema

```json
{
    "id": 2,
    "name": "Sugar",
    "description": "A must-have component for any meal",
    "price": 5.0,
    "archived": false
}
```

* GET /products - list all unarchived products
* GET /products/archived - list all archived products
* GET /products/{id} - get product details by id
* POST /products - add new product
* PUT /products/{id} - update existing product
* DELETE /products/id - archive product by id
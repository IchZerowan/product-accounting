package com.ichzerowan.accounting.dao;

public class ProductNotFoundException extends RuntimeException {
    public ProductNotFoundException(Long id) {
        super("Couldn't find product with id " + id);
    }
}

package com.ichzerowan.accounting.dao;

public class OutOfStockException extends RuntimeException {

    public OutOfStockException(Long productId, int required, int available) {
        super(String.format("Not enough products (id=%d) in stock to complete transaction. Requested %d, %d in stock",
                productId, required, available));
    }
}

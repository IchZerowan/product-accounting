package com.ichzerowan.accounting.dao;

public class EmptyTransactionException  extends RuntimeException {

    public EmptyTransactionException(Class<?> clazz, Object id) {
        super(String.format("Unable to commit %s with id %s, the products list is empty", clazz.getSimpleName(), id));
    }
}

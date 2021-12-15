package com.ichzerowan.accounting.dao;

public class ObjectArchivedException extends RuntimeException {

    public ObjectArchivedException(Class<?> clazz, Object id) {
        super(String.format("Cannot use %s with id %s, it was archived", clazz.getSimpleName(), id));
    }
}

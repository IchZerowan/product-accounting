package com.ichzerowan.accounting.dao;

public class ObjectNotFoundException extends RuntimeException {

    public ObjectNotFoundException(Class<?> clazz, Object id) {
        super(String.format("Couldn't find %s with id %s", clazz.getSimpleName(), id));
    }
}

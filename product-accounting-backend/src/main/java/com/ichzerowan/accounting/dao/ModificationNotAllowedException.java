package com.ichzerowan.accounting.dao;

public class ModificationNotAllowedException extends RuntimeException {

    public ModificationNotAllowedException(Class<?> clazz, Object id) {
        super(String.format("Modification of the %s with id %s is not allowed", clazz.getSimpleName(), id));
    }
}

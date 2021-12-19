package com.ichzerowan.accounting.controller;

import com.ichzerowan.accounting.dao.*;
import com.ichzerowan.accounting.model.ExceptionInfo;
import org.springframework.http.HttpStatus;
import org.springframework.web.bind.annotation.ControllerAdvice;
import org.springframework.web.bind.annotation.ExceptionHandler;
import org.springframework.web.bind.annotation.ResponseBody;
import org.springframework.web.bind.annotation.ResponseStatus;

import javax.servlet.http.HttpServletRequest;

@ControllerAdvice
public class DefaultExceptionHandler {

    @ResponseStatus(HttpStatus.NOT_FOUND)
    @ExceptionHandler(ObjectNotFoundException.class)
    @ResponseBody
    ExceptionInfo handleNotFound(HttpServletRequest req, Exception ex) {
        return new ExceptionInfo(404, ex);
    }

    @ResponseStatus(HttpStatus.METHOD_NOT_ALLOWED)
    @ExceptionHandler({ModificationNotAllowedException.class, OutOfStockException.class,
            EmptyTransactionException.class, ObjectArchivedException.class,
            OutOfCouponsException.class})
    @ResponseBody
    ExceptionInfo handleNotAllowed(HttpServletRequest req, Exception ex) {
        return new ExceptionInfo(405, ex);
    }

}

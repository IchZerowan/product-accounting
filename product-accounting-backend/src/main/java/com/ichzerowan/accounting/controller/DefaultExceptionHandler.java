package com.ichzerowan.accounting.controller;

import com.ichzerowan.accounting.dao.ProductNotFoundException;
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
    @ExceptionHandler(ProductNotFoundException.class)
    @ResponseBody
    ExceptionInfo handleNotFound(HttpServletRequest req, Exception ex) {
        return new ExceptionInfo(404, ex);
    }
}

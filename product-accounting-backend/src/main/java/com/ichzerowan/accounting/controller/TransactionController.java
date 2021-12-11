package com.ichzerowan.accounting.controller;

import com.ichzerowan.accounting.dao.TransactionRepository;
import com.ichzerowan.accounting.model.Delivery;
import com.ichzerowan.accounting.model.Transaction;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.transaction.annotation.Transactional;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import java.util.List;

@RestController
@RequestMapping("/transactions")
public class TransactionController {

    @Autowired
    private TransactionRepository repository;

    @GetMapping("")
    @Transactional
    List<Transaction> getAll(){
        return repository.findAll();
    }
}

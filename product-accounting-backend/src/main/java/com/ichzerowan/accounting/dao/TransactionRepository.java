package com.ichzerowan.accounting.dao;

import com.ichzerowan.accounting.model.transaction.Transaction;
import org.springframework.data.jpa.repository.JpaRepository;

public interface TransactionRepository extends JpaRepository<Transaction, Long> {

}

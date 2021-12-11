package com.ichzerowan.accounting.dao;

import com.ichzerowan.accounting.model.purchase.Purchase;
import org.springframework.data.jpa.repository.JpaRepository;

public interface PurchaseRepository extends JpaRepository<Purchase, Long> {

}

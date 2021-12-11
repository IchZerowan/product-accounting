package com.ichzerowan.accounting.dao;

import com.ichzerowan.accounting.model.purchase.PurchaseProduct;
import com.ichzerowan.accounting.model.purchase.PurchaseProductId;
import org.springframework.data.jpa.repository.JpaRepository;

public interface PurchaseProductRepository extends JpaRepository<PurchaseProduct, PurchaseProductId> {

}

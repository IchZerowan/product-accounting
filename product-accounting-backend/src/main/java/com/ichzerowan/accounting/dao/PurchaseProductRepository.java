package com.ichzerowan.accounting.dao;

import com.ichzerowan.accounting.model.DeliveryProduct;
import com.ichzerowan.accounting.model.DeliveryProductId;
import com.ichzerowan.accounting.model.PurchaseProduct;
import com.ichzerowan.accounting.model.PurchaseProductId;
import org.springframework.data.jpa.repository.JpaRepository;

public interface PurchaseProductRepository extends JpaRepository<PurchaseProduct, PurchaseProductId> {

}

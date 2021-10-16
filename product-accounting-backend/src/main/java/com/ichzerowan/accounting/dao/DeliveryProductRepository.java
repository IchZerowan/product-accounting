package com.ichzerowan.accounting.dao;

import com.ichzerowan.accounting.model.DeliveryProduct;
import com.ichzerowan.accounting.model.DeliveryProductId;
import org.springframework.data.jpa.repository.JpaRepository;

public interface DeliveryProductRepository extends JpaRepository<DeliveryProduct, DeliveryProductId> {

}
